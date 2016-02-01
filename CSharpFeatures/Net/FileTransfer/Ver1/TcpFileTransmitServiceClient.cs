using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc.Ver1
{
    public class TcpFileTransmitServiceClient
    {
        private bool closed;
        public FileTransferServiceConfig Config { get; set; }
        public int BufferSize { get; set; }
        public TcpFileTransmitServiceClient()
        {
            this.Config = new FileTransferServiceConfig
            {
                ServerAddress = "127.0.0.1",
                ServerPort = 9050,
            };
            BufferSize = Config.BufferSize;
        }
        public void Upload(string filePath, Action<FileTransmitState> callback)
        {
            if (string.IsNullOrEmpty(filePath)) filePath = GetFileName();
            //var FileInfo = new FileInfo(filePath);
            //var fName = FileInfo.Name;
            //byte[] preBuffer;
            //using (var memoryStream = new MemoryStream())
            //{
            //    using (BinaryWriter writer = new BinaryWriter(memoryStream))
            //    {

            //        writer.Write(fName);

            //        //writer.Write(md5Hash);
            //    }
            //    preBuffer = memoryStream.ToArray();
            //}

            FileTransmitState state = null;
            try
            {
                IPAddress ipAddress;
                bool parsed = IPAddress.TryParse(Config.ServerAddress, out ipAddress);
                if (!parsed)
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Config.ServerAddress);
                    ipAddress = ipHostInfo.AddressList[0];
                }
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ipEndPoint = new IPEndPoint(ipAddress, Config.ServerPort);
                var fileInfo = new FileInfo(filePath);
                state = new FileTransmitState
                {
                    Connection = clientSocket,
                    FileInfo = fileInfo,
                    FileSize = fileInfo.Length,
                    FileName = fileInfo.Name,
                    AfterTransmitCallback = callback
                };
                //clientSocket.BeginConnect(ipEndPoint, SendHandshakeMsg, new Tuple<string, Socket>(filePath, clientSocket));
                state.Connection.BeginConnect(ipEndPoint, SendHandshakeMsg, state);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                if (state != null)
                {
                    try
                    {
                        state.Connection.Shutdown(SocketShutdown.Both);
                        state.Connection.Close();
                    }
                    catch (Exception e)
                    {
                        Logging.LogUsefulException(e);
                    }
                }
            }
        }
        /// <summary>
        /// 建立连接后，发送握手消息
        /// </summary>
        /// <param name="ar"></param>
        private void SendHandshakeMsg(IAsyncResult ar)
        {
            if (closed)
            {
                return;
            }
            var state = (FileTransmitState)ar.AsyncState;
            var clientSocket = state.Connection;
            try
            {
                clientSocket.EndConnect(ar);
                //Ver1,Opt2,RSV1,MethodType1
                //var handshakeMessage = new MessageHandler();

                var fileInfo = state.FileInfo;
                //协议中消息体部分使用{目标文件路径|文件长度}
                var fileInfoBuffer = Encoding.UTF8.GetBytes($"{fileInfo.Name}|{fileInfo.Length}");
                byte[] header = { 1, 0, 0, 0, 1, 0 };
                byte[] handshakeMsg = new byte[header.Length + fileInfoBuffer.Length];
                header.CopyTo(handshakeMsg, 0);
                fileInfoBuffer.CopyTo(handshakeMsg, header.Length);

                clientSocket.BeginSend(handshakeMsg, 0, handshakeMsg.Length, 0,
                    HandshakeSendCallback, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
            }
        }
        /// <summary>
        /// 发送消息成功后，接受来自服务端的响应
        /// </summary>
        /// <param name="ar"></param>
        private void HandshakeSendCallback(IAsyncResult ar)
        {
            try
            {
                var state = (FileTransmitState)ar.AsyncState;
                var clientSocket = state.Connection;
                SocketError error;
                var sendResult = clientSocket.EndSend(ar, out error);
                Logging.Debug(string.Format("Client:HandShake,SendByte:{0},Error:{1}", sendResult, error));
                byte[] response = new byte[BufferSize];
                state.Buffer = response;
                clientSocket.BeginReceive(response, 0, response.Length, SocketFlags.None, ResponseCallBack,
                    //new Tuple<Socket, byte[], FileInfo>(clientSocket, response, state.Item2));
                    state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
            }


        }
        /// <summary>
        /// 接受响应并启动文件传送
        /// </summary>
        /// <param name="ar"></param>
        private void ResponseCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                SocketError error;
                var responseBytes = state.Connection.EndReceive(ar, out error);

                if (responseBytes <= 0) throw new Exception("连接失败," + error);

                //Validation
                //1.ver
                //2.Status
                if (state.Buffer[0] != 1) throw new Exception("服务端传输协议版本与客户端不一致");
                if (state.Buffer[1] != 0x10) throw new Exception("服务端异常" + state.Buffer[1]);


                BeginTransmit(state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
            }

        }
        /// <summary>
        /// 启动异步读取文件
        /// </summary>
        /// <param name="state"></param>
        private void BeginTransmit(FileTransmitState state)
        {
            try
            {
                var readFileBytes = 0;
                var fileStream = new FileStream(state.FileInfo.FullName, FileMode.Open,
                    FileAccess.Read, FileShare.Read, BufferSize,
                    FileOptions.Asynchronous);
                state.TransmitedByteCount = readFileBytes;
                state.FileStream = fileStream;
                //TODO buffer size
                fileStream.BeginRead(new byte[BufferSize], 0, new byte[BufferSize].Length, AfterReadFileToBuffer,
                    //new FileTransmitState()
                    //{
                    //    FileStream = fileStream,
                    //    Buffer = new byte[BufferSize],
                    //    Connection = state.Connection,
                    //    TransmitedByteCount = readFileBytes,
                    //    FileInfo = state.FileInfo,
                    //    FileSize = state.FileInfo.Length,
                    //});
                    state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
            }

        }
        /// <summary>
        /// 读取文件到Buffer，并发起文件传送
        /// </summary>
        /// <param name="ar"></param>
        private void AfterReadFileToBuffer(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {

                var readingBytes = state.FileStream.EndRead(ar);
                Logging.Debug(string.Format("Client:ReadBytesFromFile:{0}", readingBytes));
                //TransmitedByteCount
                state.Connection.BeginSend(state.Buffer, 0,
                    readingBytes, SocketFlags.None, SendFileCallBack, state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                state.Close();
            }

        }
        /// <summary>
        /// 当前块传输完成后处理后续块或者接受服务端响应
        /// </summary>
        /// <param name="ar"></param>
        private void SendFileCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {


                SocketError error;
                var sentBytes = state.Connection.EndSend(ar, out error);
                state.Count++;
                Logging.Debug(
                    string.Format("Client:ReadCount:{0},Dealed:{1}，SentBytes{2},Error:{3}",
                        state.Count, state.TransmitedByteCount, sentBytes, error));
                if (state.TransmitedByteCount + sentBytes < state.FileInfo.Length)
                {
                    state.FileStream.BeginRead(state.Buffer, 0,
                        state.Buffer.Length, AfterReadFileToBuffer, state);
                    state.TransmitedByteCount += sentBytes;
                }
                else
                {
                    state.Connection.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None,
                        CloseConnectionCallBack, state);

                }
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                state.Close();
            }

        }
        /// <summary>
        /// 结束传输并执行回调
        /// </summary>
        /// <param name="ar"></param>
        private void CloseConnectionCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {


                state.Connection.EndReceive(ar);
                //执行CallBack
                state.AfterTransmitCallback?.Invoke(state);

                //如果在Callback中没有处理掉异常，需要抛出
                if (state.Buffer[1] != 0x20) throw new Exception("服务端没有正常关闭");
                state.Close();
                Logging.Debug(state.FileName + " Send completed！");
                var sw = Logging.sw;
                sw.Stop();
                Logging.Info(
                    $"FileSize:{state.FileSize / 1024 }K,Spend:{sw.Elapsed} second,speed:{(decimal)state.FileSize / sw.Elapsed.Milliseconds / 1024 / 1000} k/s");
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                state.Close();
            }

        }


        /// <summary>
        /// 执行失败的重传处理
        /// </summary>
        private void RetryConnect()
        {
            throw new NotImplementedException();
        }

        private void Close()
        {
            throw new NotImplementedException();
        }

        private string GetFileName()
        {
            return "R:\\J9Updater.FileTransferSvc.dll";
        }
    }

    /// <summary>
    /// 消息处理器
    /// </summary>
    internal class MessageHandler
    {

        public HandshakeMessage GetHandshakeMessage()
        {
            return null;
        }

    }
    /// <summary>
    /// 握手消息处理器
    /// </summary>
    internal class HandshakeMessage : TransmitMessage
    {

    }

    internal class TransmitMessage
    {

        public byte[] GetMsg()
        {
            return null;
        }
    }

}
