using J9Updater.FileTransferSvc.Ver1.Msgs;
using System.Collections.Generic;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    class ApplicationUpdateServiceDistributer : IServiceDistributer
    {
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            if (sessionServices.Count > 0)
            {

            }
        }

        private readonly Dictionary<string, ApplicationUpdateService> sessionServices = new Dictionary<string, ApplicationUpdateService>();
        /// <summary>
        /// 服务路由匹配
        /// </summary>
        /// <param name="handshakeMsg">消息字节流</param>
        /// <param name="length">消息长度</param>
        /// <param name="connection">接受客户端请求的socket连接</param>
        /// <returns>如果匹配当前服务消息格式，返回true</returns>
        public bool Handshake(byte[] handshakeMsg, int length, Socket connection)
        {
            if (length < 6)
            {
                return false;
            }
            var ver = handshakeMsg[0];
            return (ver & 0xf0) != 0x10;
        }

        public bool Handle(byte[] handshakeMsg, int length, Socket socket, object state)
        {
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);

            var msg = Message.CreateMessage(handshakeMsg) as HandshakeMessage;
            ApplicationUpdateService service;
            if ((msg.TransmitOption & TransmitOption.Session) != TransmitOption.None)
            {
                if (sessionServices.ContainsKey(msg.SessionId))
                {
                    service = sessionServices[msg.SessionId];
                }
                else
                {
                    service = new ApplicationUpdateService(socket);
                    service.OnClose += RemoveFromSession;
                    sessionServices.Add(msg.SessionId, service);
                }
            }
            else
            {
                service = new ApplicationUpdateService(socket);
            }

            if (msg.Direction == Direction.Upload)
                service.Upload(msg);
            else
                service.Download(msg);
            return true;
        }

        private void RemoveFromSession(string sessionId)
        {
            this.sessionServices.Remove(sessionId);
        }
    }
}
