using System;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    /// <summary>
    /// TCP文件传输服务 Ver 1
    /// </summary>
    internal class TcpFileTransmitServiceDistributer : IServiceDistributer
    {
        private DateTime lastSweepTime;
        //public List<FileTransmitService> Handlers { get; set; }
        public TcpFileTransmitServiceDistributer()
        {
            lastSweepTime = DateTime.Now;
            //this.Handlers = new List<FileTransmitService>();
        }

        /// <summary>
        /// 服务路由匹配
        /// </summary>
        /// <param name="handshakeMsg">消息字节流</param>
        /// <param name="length">消息长度</param>
        /// <param name="connection"></param>
        /// <returns>如果匹配当前服务消息格式，返回true</returns>
        public bool Handshake(byte[] handshakeMsg, int length, Socket connection)
        {
            if (connection.ProtocolType != ProtocolType.Tcp)
            {
                return false;
            }
            if (length < 6)
            {
                return false;
            }
            // +-----+-----+-------+------+-------------------+---------------------+
            // | VER | OPT |  RSV  |Type   | UpLoad/Download   |  fileName|fileLength |
            // +-----+-----+-------+------+-------------------+---------------------+
            // |  1  |  2  | X'00' |   1   | 1                 |    Variable          |
            // +-----+-----+-------+------+-------------------+---------------------+

            var ver = handshakeMsg[0];
            if (ver != 1)
            {
                //Logging.Error("Service Version Check Failed,Expect ver=1 actual is " + ver);
                return false;
            }
            //File Transmit
            if (handshakeMsg[4] != 0x01)
                return false;
            return true;
        }

        public bool Handle(byte[] handshakeMsg, int length, Socket socket, object state)
        {

            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            var handler = new FileTransmitService();
            //handler.controller = _controller;
            //
            if (handshakeMsg[5] == 0x00)
                handler.Upload(socket, handshakeMsg, length);
            else
                handler.Download(socket, handshakeMsg, length);

            //轮询的方式关闭超时的连接 
            //IList<FileTransmitService> handlersToClose = new List<FileTransmitService>();
            //lock (Handlers)
            //{
            //    Handlers.Add(handler);
            //    Logging.Debug("Adding Handler...");
            //    DateTime now = DateTime.Now;
            //    if (now - lastSweepTime > TimeSpan.FromSeconds(1))
            //    {
            //        lastSweepTime = now;
            //        foreach (var handlerNeedsToBeClosed in Handlers)
            //        {
            //            if (now - handlerNeedsToBeClosed.LastActivity > TimeSpan.FromSeconds(900))
            //            {
            //                handlersToClose.Add(handlerNeedsToBeClosed);
            //            }
            //        }
            //    }
            //}
            //foreach (var closingHandler in handlersToClose)
            //{
            //    Logging.Debug("Closing timed out TCP connection.");
            //    closingHandler.Close();
            //}
            return true;
        }


        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {

        }
    }
}