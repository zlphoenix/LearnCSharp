using System;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    internal class TcpHandler
    {
        public Socket Connection { get; set; }
        private TcpRelay relay;
        public DateTime lastActivity;
        private byte[] _firstPacket;
        private int _firstPacketLength;
        private bool closed;
        const int RecvSize = 8192;
        //const int RecvReserveSize = IVEncryptor.ONETIMEAUTH_BYTES + IVEncryptor.AUTH_BYTES; // reserve for one-time auth

        public TcpHandler(TcpRelay tcpRelay)
        {
            BufferSize = RecvSize + RecvSize + 32;
            relay = tcpRelay;
        }

        public void Start(byte[] firstPacket, int length)
        {
            _firstPacket = firstPacket;
            _firstPacketLength = length;
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
                int bytesRead = _firstPacketLength;

                if (bytesRead > 1)
                {
                    byte[] response = { 5, 0 };
                    if (_firstPacket[0] != 5)
                    {
                        // reject socks 4
                        response = new byte[] { 0, 91 };
                        Logging.Error("socks 5 protocol error");
                    }
                    Connection.BeginSend(response, 0, response.Length, 0, HandshakeSendCallback, null);
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
            if (closed)
            {
                return;
            }
            try
            {
                Connection.EndSend(ar);

                // +-----+-----+-------+------+----------+----------+
                // | VER | OPT |  RSV  |METHOD |  PARAM   |   CRC    |
                // +-----+-----+-------+------+----------+----------+
                // |  1  |  1  | X'00' |   1   | Variable |    2     |
                // +-----+-----+-------+------+----------+----------+

                byte[] connetionRecvBuffer = new byte[BufferSize];
                Connection.BeginReceive(connetionRecvBuffer, 0, 3, 0, HandshakeReceive2Callback, connetionRecvBuffer);
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                Close();
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

                if (bytesRead >= 3)
                {
                    //· CONNECT：X’01’
                    //· BIND：X’02’
                    //· UDP ASSOCIATE：X’03’
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
            lock (relay.Handlers)
            {
                relay.Handlers.Remove(this);
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