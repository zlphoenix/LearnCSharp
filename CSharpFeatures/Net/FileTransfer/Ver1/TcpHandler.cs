using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class TcpHandler
    {
        private DateTime lastActivity;
        private const string FileServerDir = @"R:\ReceivedFiles\";

        private bool closed;
        //const int RecvSize = 8192;
        //const int RecvReserveSize = IVEncryptor.ONETIMEAUTH_BYTES + IVEncryptor.AUTH_BYTES; // reserve for one-time auth
        public int BufferSize { get; private set; }
        public TcpHandler()
        {
            BufferSize = new FileTransferServiceConfig().BufferSize;
        }

        #region Upload


        public void Upload(Socket connection, byte[] handshakeMsg, int handshakeMsgLength)
        {
            if (closed)
            {
                return;
            }
            lastActivity = DateTime.Now;

            var state = new FileTransmitState
            {
                Connection = connection,
            };
            try
            {
                byte[] response = { 1, 0x10, 0 };//Succeed msg
                var fileInfoStr = Encoding.UTF8.GetString(handshakeMsg, 6, handshakeMsgLength - 6);

                if (string.IsNullOrEmpty(fileInfoStr))
                {
                    Logging.Error("FileInfo Error,response X'40'");
                    response[1] = 0x40; //Client Msg Error
                }
                else
                {
                    var fileInfoArray = fileInfoStr.Split('|');
                    if (fileInfoArray.Length != 2)
                    {
                        Logging.Error("FileInfo Error,response X'40'");
                        response[1] = 0x40; //Client Msg Error
                    }
                    else
                    {
                        state.DealingByteCount = 0;
                        state.Buffer = null;
                        state.FileName = fileInfoArray[0];
                        state.FileSize = Convert.ToInt32(fileInfoArray[1]);
                        if (!Util.CheckDiskSpace(FileServerDir, state.FileSize))
                        {
                            response[1] = 0x51; //Not enough diskspace Msg Error
                        }
                    }
                }
                if (response[1] != 0x10)
                {
                    state.Connection.BeginSend(response, 0, response.Length, 0, UploadFinishingCallBack, state);
                    state.Close();
                    return;
                }
                state.Connection.BeginSend(response, 0, response.Length, 0, UploadHandshakeSendCallback, state);
            }
            catch (Exception e)
            {
                Logging.Debug("握手过程中发生异常");
                Logging.LogUsefulException(e);
                state.Close();
            }
        }

        private void UploadHandshakeSendCallback(IAsyncResult ar)
        {
            if (closed)
            {
                return;
            }
            var state = (FileTransmitState)ar.AsyncState;

            try
            {
                state.Connection.EndSend(ar);
                //握手失败的场景
                if (ar.AsyncState == null)
                {
                    Logging.Debug("握手失败");
                    state.Close();
                    return;
                }

                state.Buffer = new byte[BufferSize];

                state.Connection.BeginReceive(state.Buffer,
                    0, BufferSize, SocketFlags.None,
                    UploadReceiveFileCallback, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                state.Close();
            }
        }
        private void UploadReceiveFileCallback(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                var bytesRead = state.Connection.EndReceive(ar);
                if (bytesRead > 0)
                {
                    var filePath = Path.Combine(FileServerDir, state.FileName);
                    var writer = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write, BufferSize, FileOptions.Asynchronous);
                    state.FileInfo = new FileInfo(filePath);
                    state.FileStream = writer;
                    state.DealingByteCount = bytesRead;
                    writer.BeginWrite(state.Buffer, 0,
                        bytesRead, UploadWriteFileCallBack, state);
                }
                else
                {
                    Close(state);
                }
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                Close(state);
            }
        }
        private void UploadWriteFileCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                state.FileStream.EndWrite(ar);
                state.Count++;
                Logging.Debug(
                    string.Format("Server:WriteCount:{0},Dealed:{1}",
                    state.Count, state.TransmitedByteCount));
                if (state.FileSize > state.FileStream.Position)
                {

                    state.Connection.BeginReceive(state.Buffer,
                        0, BufferSize, SocketFlags.None,
                        UploadCountinueReceiveFileCallback, state);
                    state.TransmitedByteCount += state.DealingByteCount;//写入成功后,累计已写入字节数
                }
                else
                {
                    state.Buffer = new byte[] { 1, 0x20 };
                    state.Connection.BeginSend(state.Buffer, 0, state.Buffer.Length,
                        SocketFlags.None, UploadFinishingCallBack, state);
                }
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                Close(state);
                //throw;
            }

        }
        private void UploadFinishingCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            state.Connection.EndSend(ar);
            state.Close();
            Logging.Debug(state.FileName + " write completed！");
        }
        private void UploadCountinueReceiveFileCallback(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                SocketError error;
                var bytesRead = state.Connection.EndReceive(ar, out error);
                Logging.Debug(
                   string.Format("Server:Count:{0},Receive:{1},SocketError:{2}",
                   state.Count, bytesRead, error));
                if (bytesRead > 0)
                {
                    state.FileStream.BeginWrite(state.Buffer,
                        0, bytesRead, UploadWriteFileCallBack, state);
                }
                else
                {
                    Console.WriteLine("WTF!");
                }
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                Close(state);
            }

        }
        private void Close(FileTransmitState transmitState)
        {
            lock (this)
            {
                if (closed)
                {
                    return;
                }
                closed = true;
            }
            transmitState?.Close();
        }
        #endregion

        #region Download

        /// <summary>
        /// 文件下载服务处理
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="handshakeMsg"></param>
        /// <param name="handshakeMsgLength"></param>
        public void Download(Socket connection, byte[] handshakeMsg, int handshakeMsgLength)
        {
            if (closed)
            {
                return;
            }
            lastActivity = DateTime.Now;

            var state = new FileTransmitState
            {
                Connection = connection,
            };
            try
            {
                List<byte> response = new List<byte> { 1, 0x10, 0 };//Succeed msg
                var fileInfoStr = Encoding.UTF8.GetString(handshakeMsg, 6, handshakeMsgLength - 6);

                if (string.IsNullOrEmpty(fileInfoStr))
                {
                    Logging.Error("FileInfo Error,response X'40'");
                    response[1] = 0x41; //Client Msg Error
                }
                else
                {

                    var path = Path.Combine(FileServerDir, fileInfoStr);
                    if (!File.Exists(path))
                    {
                        response[1] = 0x44; //File Not Found
                    }
                    else
                    {
                        state.FileInfo = new FileInfo(path);
                        state.FileSize = state.FileInfo.Length;
                        state.DealingByteCount = 0;


                        state.FileName = state.FileInfo.Name;
                        //使用“|做分隔符”
                        var fileLengthBytes = Encoding.UTF8.GetBytes(state.FileInfo.Length + "|");
                        response.AddRange(fileLengthBytes);
                        state.Buffer = new byte[BufferSize];
                    }

                    //state.FileSize = Convert.ToInt32(fileInfoArray[1]);

                }
                state.Connection.Send(response.ToArray());
                if (response[1] != 0x10)
                {
                    //Error

                    state.Close();
                }
                else
                {

                    state.Connection.BeginReceive(state.Buffer, 0, state.Buffer.Length, SocketFlags.None,
                        After2ndHandshakeCallback, state);
                }
            }
            catch (Exception e)
            {
                Logging.Debug("握手过程中发生异常");
                Logging.LogUsefulException(e);
                state.Close();
            }
        }

        private void After2ndHandshakeCallback(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                var seconndHandshakeBytes = state.Connection.EndReceive(ar);
                if (seconndHandshakeBytes == 0)
                {
                    throw new Exception("第二次握手连接中断");
                }
                if (state.Buffer[1] != 0x10) throw new Exception("客户端出错:" + state.Buffer[1].ToString("X"));


                state.FileStream = new FileStream(state.FileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.Asynchronous);
                state.TransmitedByteCount = 0;


                state.FileStream.BeginRead(state.Buffer, 0, state.Buffer.Length, AfterReadFileToBuffer, state);

            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close(state);
            }


        }

        /// <summary>
        ///   读取文件到Buffer，并发起文件传送
        /// </summary>
        /// <param name="ar"></param>
        private void AfterReadFileToBuffer(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                var readingBytes = state.FileStream.EndRead(ar);
                Logging.Debug(string.Format("Server:ReadBytesFromFile:{0}", readingBytes));
                //TransmitedByteCount
                state.Connection.BeginSend(state.Buffer, 0,
                   Math.Min(state.Buffer.Length, readingBytes), SocketFlags.None, SendFileCallBack, state);


                if (state.FileStream.Position < state.FileStream.Length)
                {
                    var bytesToSend = state.FileStream.Length - state.FileStream.Position;
                    state.FileStream.BeginRead(state.Buffer, 0, Math.Min((int)bytesToSend, BufferSize),
                        AfterReadFileToBuffer, state);
                }
                else
                {
                    state.Connection.BeginReceive(state.Buffer, 0,
                        readingBytes, SocketFlags.None, DownLoadFinishingCallBack, state);
                }

            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                Close(state);
            }
        }

        private void DownLoadFinishingCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                SocketError error;
                var receivedBytes = state.Connection.EndReceive(ar, out error);
                if (receivedBytes == 0) throw new Exception("客户端没有正确关闭连接");
                if (state.Buffer[1] != 0x20)
                {
                    throw new Exception("文件下载出错:" + state.Buffer[1]);
                }
                Close(state);
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                Close(state);
            }
        }

        private void SendFileCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                SocketError error;
                var sentBytes = state.Connection.EndSend(ar, out error);
                state.Count++;
                Logging.Debug(
                    string.Format("Server:ReadCount:{0},Dealed:{1}，SentBytes{2},Error:{3}",
                        state.Count, state.TransmitedByteCount, sentBytes, error));
                if (state.FileStream.Position >= state.FileStream.Length)
                {
                    Logging.Debug("Server Listen Complete Response。。。");
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

        private void CloseConnectionCallBack(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                state.Connection.EndReceive(ar);
                //执行CallBack
                state.AfterTransmitCallback?.Invoke(state);

                //如果在Callback中没有处理掉异常，需要抛出
                if (state.Buffer[1] != 0x20) throw new Exception("客户端没有正常关闭");
                state.Close();
                Logging.Debug(state.FileName + " DownLoad completed！");
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                state.Close();
            }
        }

        #endregion

    }
}