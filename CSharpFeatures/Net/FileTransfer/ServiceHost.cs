using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    public class ServiceHost
    {
        private readonly FileTransferServiceConfig config;
        private readonly List<IService> services;

        public ServiceHost()
        {
            services = new List<IService> { new TcpBinding() };


            config = new FileTransferServiceConfig()
            {
                LocalPort = 9050,
            };

        }

        public void Start()
        {
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Create a TCP/IP socket.

                tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                IPEndPoint localEndPoint = null;
                //if (_shareOverLAN)
                //{
                localEndPoint = new IPEndPoint(IPAddress.Any, config.LocalPort); //监听所有IP地址
                //}
                //else
                //{
                //    localEndPoint = new IPEndPoint(IPAddress.Loopback, config.LocalPort);
                //}

                // Bind the socket to the local endpoint and listen for incoming connections.
                tcpSocket.Bind(localEndPoint);
                tcpSocket.Listen(1024);
                // Start an asynchronous socket to listen for connections.
                Logging.Info("Shadowsocks started");
                tcpSocket.BeginAccept(AcceptCallback, tcpSocket);
            }
            catch (SocketException ex)
            {
                Logging.LogUsefulException(ex);
                tcpSocket.Close();
                throw;
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            var listener = (Socket)ar.AsyncState;
            try
            {
                var conn = listener.EndAccept(ar);

                var buf = new byte[4096];
                var state = new Tuple<Socket, byte[]>(conn,
                    buf);


                conn.BeginReceive(buf, 0, buf.Length, 0,
                    ReceiveCallback, state);
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
            }
            finally
            {
                try
                {
                    listener.BeginAccept(AcceptCallback, listener);
                }
                catch (ObjectDisposedException)
                {
                    // do nothing
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                }
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            var state = (Tuple<Socket, byte[]>)ar.AsyncState;

            var conn = state.Item1;
            var buf = state.Item2;
            try
            {
                var bytesRead = conn.EndReceive(ar);

                foreach (var service in services)
                {
                    if (service.Handle(buf, bytesRead, conn, null))
                    {
                        return;
                    }
                }
                // no service found for this
                if (conn.ProtocolType == ProtocolType.Tcp)
                {
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
                conn.Close();
            }
        }
    }
}
