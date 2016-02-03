using J9Updater.FileTransferSvc.Ver1;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    public class ServiceHost : IDisposable
    {
        private readonly FileTransferServiceConfig config;
        private readonly List<IService> services;
        private Socket tcpSocket;

        public ServiceHost()
        {
            services = new List<IService> { new TcpFileTransmitService() };
            config = new FileTransferServiceConfig()
            {
                LocalPort = 9050,
            };
        }
        /// <summary>
        /// 启动服务，打开TCP监听端口
        /// </summary>
        public void Start()
        {
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // Create a TCP/IP socket.
                tcpSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                IPEndPoint localEndPoint = null;
                //Listen Connection from All IP 
                localEndPoint = new IPEndPoint(IPAddress.Any, config.LocalPort);


                // Bind the socket to the local endpoint and listen for incoming connections.
                tcpSocket.Bind(localEndPoint);
                tcpSocket.Listen(1024);
                // Upload an asynchronous socket to listen for connections.
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
        /// <summary>
        /// 建立来自客户端的请求连接
        /// </summary>
        /// <param name="ar"></param>
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

        /// <summary>
        /// 接受握手请求的回调方法，进行服务路由匹配
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            var state = (Tuple<Socket, byte[]>)ar.AsyncState;

            var conn = state.Item1;
            var buf = state.Item2;
            try
            {

                var bytesRead = conn.EndReceive(ar);
                //TODO 轮询路由，不适合配置，可改进
                //Router
                var serviceMach = false;
                foreach (var service in services)
                {
                    if (service.Handshake(buf, bytesRead, conn))
                    {
                        service.Handle(buf, bytesRead, conn, null);
                        serviceMach = true;
                    }
                }
                //// no service found for this
                if (!serviceMach)
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

        public void Close()
        {
            foreach (var service in services)
            {
                service.Dispose();
            }
            if (this.tcpSocket != null && tcpSocket.Connected)
            {
                tcpSocket.Dispose();
            }
        }
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}
