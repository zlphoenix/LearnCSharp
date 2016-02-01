using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class TcpHandler
    {
        public DateTime lastActivity;

        private bool closed;
        //const int RecvSize = 8192;
        //const int RecvReserveSize = IVEncryptor.ONETIMEAUTH_BYTES + IVEncryptor.AUTH_BYTES; // reserve for one-time auth
        public int BufferSize { get; private set; }
        public TcpHandler()
        {
            BufferSize = new FileTransferServiceConfig().BufferSize;
        }

        public void Start(Socket connection, byte[] firstPacket, int length)
        {
            if (closed)
            {
                return;
            }
            var handshakeMsg = firstPacket;
            var handshakeMsgLength = length;
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
                        state.Buffer = null;
                        state.FileName = fileInfoArray[0];
                        state.FileSize = Convert.ToInt32(fileInfoArray[1]);

                    }
                }
                state.Connection.BeginSend(response, 0, response.Length, 0, HandshakeSendCallback, state);
            }
            catch (Exception e)
            {
                Logging.Debug("握手过程中发生异常");
                Logging.LogUsefulException(e);
                state.Close();
            }
        }
        private void HandshakeSendCallback(IAsyncResult ar)
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
                    ReceiveFileCallback, state);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                state.Close();
            }
        }
        private void ReceiveFileCallback(IAsyncResult ar)
        {
            var state = (FileTransmitState)ar.AsyncState;
            try
            {
                var bytesRead = state.Connection.EndReceive(ar);
                if (bytesRead > 0)
                {
                    var filePath = Path.Combine(@"R:\ReceivedFiles\", state.FileName);
                    var writer = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Write, BufferSize, FileOptions.Asynchronous);
                    state.FileInfo = new FileInfo(filePath);
                    state.FileStream = writer;
                    state.DealingByteCount = bytesRead;
                    writer.BeginWrite(state.Buffer, 0,
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
            var state = (FileTransmitState)ar.AsyncState;
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

                    state.Connection.BeginReceive(state.Buffer,
                        0, BufferSize, SocketFlags.None,
                        CountinueReceiveFileCallback, state);
                    state.TransmitedByteCount += state.DealingByteCount;//写入成功后,累计已写入字节数
                }
                else
                {
                    state.Buffer = new byte[] { 1, 0x20 };
                    state.Connection.BeginSend(state.Buffer, 0, state.Buffer.Length,
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
            var state = (FileTransmitState)ar.AsyncState;
            state.Connection.EndSend(ar);
            state.Close();
            Logging.Debug(state.FileName + " write completed！");
        }
        private void CountinueReceiveFileCallback(IAsyncResult ar)
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
    }
}