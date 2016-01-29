using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc
{
    public class ServiceClient
    {
        private bool closed;
        public FileTransferServiceConfig Config { get; set; }
        public int BufferSize { get; set; }
        public ServiceClient()
        {
            this.Config = new FileTransferServiceConfig
            {
                ServerAddress = "127.0.0.1",
                ServerPort = 9050,
                //LocalPort =
            };
            BufferSize = Config.BufferSize;
        }
        public void SendFile(string filePath)
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
            Socket clientSocket = null;
            try
            {
                IPAddress ipAddress;
                bool parsed = IPAddress.TryParse(Config.ServerAddress, out ipAddress);
                if (!parsed)
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Config.ServerAddress);
                    ipAddress = ipHostInfo.AddressList[0];
                }
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                var ipEndPoint = new IPEndPoint(ipAddress, Config.ServerPort);

                clientSocket.BeginConnect(ipEndPoint, SendHandshakeMsg, new Tuple<string, Socket>(filePath, clientSocket));
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                if (clientSocket != null)
                {
                    try
                    {
                        clientSocket.Shutdown(SocketShutdown.Both);
                        clientSocket.Close();
                    }
                    catch (Exception e)
                    {
                        Logging.LogUsefulException(e);
                    }
                }
            }
        }

        private void SendHandshakeMsg(IAsyncResult ar)
        {
            if (closed)
            {
                return;
            }
            var state = (Tuple<string, Socket>)ar.AsyncState;
            var clientSocket = state.Item2;
            try
            {
                clientSocket.EndConnect(ar);
                //Ver1,Opt2,RSV1,MethodType1
                //var handshakeMessage = new MessageHandler();

                var fileInfo = new FileInfo(state.Item1);
                var fileInfoBuffer = Encoding.UTF8.GetBytes($"{fileInfo.Name}|{fileInfo.Length}");
                byte[] header = { 1, 0, 0, 0, 1, 0 };
                byte[] handshakeMsg = new byte[header.Length + fileInfoBuffer.Length];
                header.CopyTo(handshakeMsg, 0);
                fileInfoBuffer.CopyTo(handshakeMsg, header.Length);

                clientSocket.BeginSend(handshakeMsg, 0, handshakeMsg.Length, 0,
                    HandshakeSendCallback, new Tuple<Socket, FileInfo>(clientSocket, fileInfo));
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
            }
        }

        private void HandshakeSendCallback(IAsyncResult ar)
        {
            var state = (Tuple<Socket, FileInfo>)ar.AsyncState;
            var clientSocket = state.Item1;
            SocketError error;
            var sendResult = clientSocket.EndSend(ar, out error);
            Logging.Debug(string.Format("Client:HandShake,Send:{0},Error:{1}", sendResult, error));
            byte[] response = new byte[BufferSize];
            clientSocket.BeginReceive(response, 0, response.Length, SocketFlags.None, ResponseCallBack,
                new Tuple<Socket, byte[], FileInfo>(clientSocket, response, state.Item2));

        }

        private void ResponseCallBack(IAsyncResult ar)
        {
            var state = (Tuple<Socket, byte[], FileInfo>)ar.AsyncState;
            var clientSocket = state.Item1;
            SocketError error;
            var responseBytes = clientSocket.EndReceive(ar, out error);

            if (responseBytes <= 0) throw new Exception("连接失败," + error);

            var response = state.Item2;
            //Validation
            //1.ver
            //2.Status
            if (response[0] != 1) throw new Exception("服务端传输协议版本与客户端不一致");
            if (response[1] != 0x10) throw new Exception("服务端异常" + response[1]);


            var fileInfo = state.Item3;
            byte[] fileBytes = new byte[BufferSize];
            //FileBytes[0] = 1;//Ver
            var readFileBytes = 0;
            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open,
                FileAccess.Read, FileShare.Read, BufferSize,
                FileOptions.Asynchronous);

            //TODO buffer size
            fileStream.BeginRead(fileBytes, 0, fileBytes.Length, AfterReadFileToBuffer,
                new SendFileState()
                {
                    FileStream = fileStream,
                    FileBytes = fileBytes,
                    Connection = clientSocket,
                    TransmitedByteCount = readFileBytes,
                    FileInfo = fileInfo,
                    FileSize = fileInfo.Length,
                });

        }

        private void AfterReadFileToBuffer(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            try
            {

                var readingBytes = state.FileStream.EndRead(ar);
                Logging.Debug(string.Format("Client:ReadBytesFromFile:{0}", readingBytes));
                //TransmitedByteCount
                state.Connection.BeginSend(state.FileBytes, 0,
                    readingBytes, SocketFlags.None, SendFileCallBack, state);
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                state.Close();
            }

        }


        private void SendFileCallBack(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;

            SocketError error;
            var sentBytes = state.Connection.EndSend(ar, out error);
            state.Count++;
            Logging.Debug(
                string.Format("Client:ReadCount:{0},Dealed:{1}，Error:{2}",
                    state.Count, state.TransmitedByteCount, sentBytes));
            if (state.TransmitedByteCount + sentBytes < state.FileInfo.Length)
            {
                state.FileStream.BeginRead(state.FileBytes, 0,
                    state.FileBytes.Length, AfterReadFileToBuffer, state);
                state.TransmitedByteCount += sentBytes;
            }
            else
            {
                state.Connection.BeginReceive(state.FileBytes, 0, state.FileBytes.Length, SocketFlags.None,
                    CloseConnectionCallBack, state);

            }
        }

        private void CloseConnectionCallBack(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            state.Connection.EndReceive(ar);
            if (state.FileBytes[1] != 0x20) throw new Exception("服务端没有正常关闭");
            state.Close();
            Logging.Debug(state.FileName + " Send completed！");
            var sw = Logging.sw;
            sw.Stop();
            Logging.Info(
                $"FileSize:{state.FileSize / 1024 / 1024}M,Spend:{sw.Elapsed} second,speed:{state.FileSize / sw.Elapsed.Seconds / 1024 / 1024} m/s");
        }



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
            return "R:\\temp.txt";
        }
    }

    internal class MessageHandler
    {

        public HandshakeMessage GetHandshakeMessage()
        {
            return null;
        }

    }

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
