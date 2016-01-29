using System;
using System.ComponentModel;
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
        const int RecvSize = 8192;
        //const int RecvReserveSize = IVEncryptor.ONETIMEAUTH_BYTES + IVEncryptor.AUTH_BYTES; // reserve for one-time auth

        public TcpHandler(TcpBinding tcpBinding)
        {
            BufferSize = RecvSize + RecvSize + 32;
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
            try
            {
                var bytesRead = handshakeMsgLength;

                if (bytesRead > 1)
                {

                    // +-----+-----+-------+------+-------------------+---------------------+
                    // | VER | OPT |  RSV  |Type   | UpLoad/Download   |  fileName|fileLength |
                    // +-----+-----+-------+------+-------------------+---------------------+
                    // |  1  |  2  | X'00' |   1   | 1                 |    Variable          |
                    // +-----+-----+-------+------+-------------------+---------------------+

                    var ver = handshakeMsg[0];
                    if (ver != 1)
                    {
                        Logging.Error("Service Version Check Failed,Expect ver=1 actual is " + ver);
                    }

                    byte[] response = { 1, 0x10, 0 };
                    var convert = new ByteConverter();
                    var fileInfoStr = Encoding.UTF8.GetString(handshakeMsg, 6, handshakeMsgLength - 6);
                    Object state = null;
                    if (string.IsNullOrEmpty(fileInfoStr))
                        response[2] = 0x40;//Client Msg Error
                    else
                    {
                        var fileInfoArray = fileInfoStr.Split('|');
                        if (fileInfoArray.Length != 2) response[2] = 0x40;//Client Msg Error
                        else
                        {
                            var fileInfo = new Tuple<Socket, string, int>(Connection, fileInfoArray[0], Convert.ToInt32(fileInfoArray[1]));
                            state = fileInfo;
                        }
                    }


                    Connection.BeginSend(response, 0, response.Length, 0, HandshakeSendCallback, state);
                }
                else
                {
                    Close();
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
            }
        }



        private void HandshakeSendCallback(IAsyncResult ar)
        {
            var state = (Tuple<Socket, string, int>)ar.AsyncState;
            if (closed)
            {
                return;
            }
            try
            {
                Connection.EndSend(ar);

                byte[] connetionRecvBuffer = new byte[BufferSize];
                Connection.BeginReceive(connetionRecvBuffer,
                    0, BufferSize, SocketFlags.None,
                    ReceiveFileCallback, new object[]
                    {
                        state.Item2, state.Item3, connetionRecvBuffer
                    });


            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
            }
        }

        private void ReceiveFileCallback(IAsyncResult ar)
        {
            var state = (object[])ar.AsyncState;
            var fileName = state[0] as string;
            var fileBytes = state[2] as byte[];
            try
            {
                int bytesRead = Connection.EndReceive(ar);
                if (bytesRead > 0)
                {
                    var filePath = Path.Combine(@"R:\ReceivedFiles\", fileName);
                    var writer = File.Open(filePath,
                        FileMode.Create);
                    writer.BeginWrite(fileBytes, 0,
                        bytesRead, WriteFileCallBack, new SendFileState
                        {
                            clientSocket = Connection,
                            fileBytes = fileBytes,
                            fileInfo = new FileInfo(filePath),
                            fileStream = writer,
                            readFileByteCount = 0,
                            filseSize = (int)state[1],
                            actualByteCount = bytesRead
                        });
                }
            }
            catch (Exception e)
            {

                Logging.LogUsefulException(e);
                Close();
            }
        }

        private void WriteFileCallBack(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;

            state.fileStream.EndWrite(ar);
            if (state.filseSize >
                (state.actualByteCount + state.readFileByteCount))
            {
                state.clientSocket.BeginReceive(state.fileBytes,
                    state.readFileByteCount, BufferSize, SocketFlags.None,
                    CountinueReceiveFileCallback, state);
                state.readFileByteCount += state.actualByteCount;//写入成功后,累计已写入字节数
            }
            else
            {
                state.fileStream.Close();
                state.clientSocket.Close();
                Console.WriteLine(state.fileInfo.Name + " write completed！");
            }
        }

        private void CountinueReceiveFileCallback(IAsyncResult ar)
        {
            var state = (SendFileState)ar.AsyncState;

            var bytesRead = state.clientSocket.EndReceive(ar);
            if (bytesRead > 0)
            {
                state.fileStream.BeginWrite(state.fileBytes,
                    state.readFileByteCount, bytesRead, WriteFileCallBack, state);
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

        public void Close()
        {
            lock (binding.Handlers)
            {
                binding.Handlers.Remove(this);
            }
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