using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc
{
    internal class TcpHandler
    {
        public Socket Connection { get; set; }
        private TcpBinding binding;
        public DateTime lastActivity;
        private byte[] handshakeMsg;
        private int handshakeMsgLength;
        private bool closed;
        //const int RecvSize = 8192;
        //const int RecvReserveSize = IVEncryptor.ONETIMEAUTH_BYTES + IVEncryptor.AUTH_BYTES; // reserve for one-time auth

        public TcpHandler(TcpBinding tcpBinding)
        {
            BufferSize = new FileTransferServiceConfig().BufferSize;
            binding = tcpBinding;
        }

        public void Start(byte[] firstPacket, int length)
        {
            handshakeMsg = firstPacket;
            handshakeMsgLength = length;
            HandshakeReceive();
            lastActivity = DateTime.Now;
        }

        private void HandshakeReceive()
        {
            if (closed)
            {
                return;
            }
            if (handshakeMsgLength <= 1)
            {
                Close();
                return;
            }
            var state = new SendFileState
            {
                Connection = Connection,
            };
            try
            {



                // +-----+-----+-------+------+-------------------+---------------------+
                // | VER | OPT |  RSV  |Type   | UpLoad/Download   |  fileName|fileLength |
                // +-----+-----+-------+------+-------------------+---------------------+
                // |  1  |  2  | X'00' |   1   | 1                 |    Variable          |
                // +-----+-----+-------+------+-------------------+---------------------+

                var ver = handshakeMsg[0];
                if (ver != 1)
                {
                    //TODO Versioning
                    Logging.Error("Service Version Check Failed,Expect ver=1 actual is " + ver);
                }

                byte[] response = { 1, 0x10, 0 };//Succeed msg
                var fileInfoStr = Encoding.UTF8.GetString(handshakeMsg, 6, handshakeMsgLength - 6);

                if (string.IsNullOrEmpty(fileInfoStr))
                {
                    Logging.Error("FileInfo Error,response X'40'");
                    response[2] = 0x40; //Client Msg Error
                }
                else
                {
                    var fileInfoArray = fileInfoStr.Split('|');
                    if (fileInfoArray.Length != 2)
                    {
                        Logging.Error("FileInfo Error,response X'40'");
                        response[2] = 0x40; //Client Msg Error
                    }
                    else
                    {

                        state.DealingByteCount = 0;
                        state.FileBytes = null;
                        state.FileName = fileInfoArray[0];
                        state.FileSize = Convert.ToInt32(fileInfoArray[1]);

                    }
                }
                Connection.BeginSend(response, 0, response.Length, 0, HandshakeSendCallback, state);
            }
            catch (Exception e)
            {
                Logging.Debug("握手过程中发生异常");
                Logging.LogUsefulException(e);
                Close(state);
            }
        }



        private void HandshakeSendCallback(IAsyncResult ar)
        {
            if (closed)
            {
                return;
            }
            try
            {
                Connection.EndSend(ar);
                //握手失败的场景
                if (ar.AsyncState == null)
                {
                    Logging.Debug("握手失败");
                    Close();
                    return;
                }
                var state = (SendFileState)ar.AsyncState;
                state.FileBytes = new byte[BufferSize];

                Connection.BeginReceive(state.FileBytes,
                    0, BufferSize, SocketFlags.None,
                    ReceiveFileCallback, state);


            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
            }
        }

        private void ReceiveFileCallback(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            try
            {
                var bytesRead = Connection.EndReceive(ar);
                if (bytesRead > 0)
                {
                    var filePath = Path.Combine(@"R:\ReceivedFiles\", state.FileName);
                    var writer = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write, BufferSize, FileOptions.Asynchronous);
                    state.FileInfo = new FileInfo(filePath);
                    state.FileStream = writer;
                    state.DealingByteCount = bytesRead;
                    writer.BeginWrite(state.FileBytes, 0,
                        bytesRead, WriteFileCallBack, state);
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



        private void WriteFileCallBack(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            try
            {
                state.FileStream.EndWrite(ar);
                state.Count++;
                Logging.Debug(
                    string.Format("Server:WriteCount:{0},Dealed:{1}",
                    state.Count, state.TransmitedByteCount));
                if (state.FileSize >
                    (state.DealingByteCount + state.TransmitedByteCount))
                {

                    state.Connection.BeginReceive(state.FileBytes,
                        0, BufferSize, SocketFlags.None,
                        CountinueReceiveFileCallback, state);
                    state.TransmitedByteCount += state.DealingByteCount;//写入成功后,累计已写入字节数
                }
                else
                {
                    state.FileBytes = new byte[] { 1, 0x20 };
                    state.Connection.BeginSend(state.FileBytes, 0, state.FileBytes.Length,
                        SocketFlags.None, FinishingCallBack, state);


                }
            }
            catch (Exception ex)
            {
                Logging.LogUsefulException(ex);
                Close(state);
                //throw;
            }

        }

        private void FinishingCallBack(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            state.Connection.EndSend(ar);
            state.Close();
            Logging.Debug(state.FileName + " write completed！");
        }

        private void CountinueReceiveFileCallback(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;
            try
            {
                SocketError error;
                var bytesRead = state.Connection.EndReceive(ar, out error);
                Logging.Debug(
                   string.Format("Server:Count:{0},Receive:{1},SocketError:{2}",
                   state.Count, bytesRead, error));
                if (bytesRead > 0)
                {
                    state.FileStream.BeginWrite(state.FileBytes,
                        0, bytesRead, WriteFileCallBack, state);
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

        private void HandshakeReceive2Callback(IAsyncResult ar)
        {
            var connetionRecvBuffer = (byte[])ar.AsyncState;
            if (closed)
            {
                return;
            }
            try
            {
                int bytesRead = Connection.EndReceive(ar);

                if (bytesRead > 0)
                {

                    var command = connetionRecvBuffer[1];

                    if (command == 1)
                    {
                        byte[] response = { 5, 0, 0, 1, 0, 0, 0, 0, 0, 0 };
                        Connection.BeginSend(response, 0, response.Length, 0, ResponseCallback, null);
                    }
                    //UDP
                    //else if (command == 3)
                    //{
                    //    HandleUDPAssociate();
                    //}
                }
                else
                {
                    Logging.Debug("failed to recv data in HandshakeReceive2Callback()");
                    Close();
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
            }
        }

        private void ResponseCallback(IAsyncResult ar)
        {
            try
            {
                Connection.EndSend(ar);

                StartConnect();
            }

            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
            }
        }
        private void StartConnect()
        {
            //TODO Dispatch
        }


        public int BufferSize { get; private set; }
        private void Close(SendFileState state)
        {
            lock (this)
            {
                if (closed)
                {
                    return;
                }
                closed = true;
            }
            state?.Close();
        }
        public void Close()
        {

            lock (this)
            {
                if (closed)
                {
                    return;
                }
                closed = true;
            }
            if (Connection != null)
            {
                try
                {
                    Logging.Debug("Closing Connection...");
                    Connection.Shutdown(SocketShutdown.Both);
                    Connection.Close();
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                }
            }
            //if (remote != null)
            //{
            //    try
            //    {
            //        remote.Shutdown(SocketShutdown.Both);
            //        remote.Close();
            //    }
            //    catch (Exception e)
            //    {
            //        Logging.LogUsefulException(e);
            //    }
            //}
            //lock (encryptionLock)
            //{
            //    lock (decryptionLock)
            //    {
            //        if (encryptor != null)
            //        {
            //            ((IDisposable)encryptor).Dispose();
            //        }
            //    }
            //}
        }
    }
}