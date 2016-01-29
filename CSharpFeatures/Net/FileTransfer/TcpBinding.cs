using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    /// <summary>
    /// ServiceRouter Ver 1
    /// </summary>
    class TcpBinding : IService
    {
        private DateTime lastSweepTime;
        public List<TcpHandler> Handlers { get; set; }
        public TcpBinding()
        {
            lastSweepTime = DateTime.Now;
            this.Handlers = new List<TcpHandler>();
        }

        public bool Handle(byte[] firstPacket, int length, Socket socket, object state)
        {
            if (socket.ProtocolType != ProtocolType.Tcp)
            {
                return false;
            }
            if (length < 2)
            {
                return false;
            }
            socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            var handler = new TcpHandler(this)
            {
                Connection = socket,
            };
            //handler.controller = _controller;
            handler.Start(firstPacket, length);

            //轮询的方式关闭超时的连接 
            IList<TcpHandler> handlersToClose = new List<TcpHandler>();
            lock (Handlers)
            {
                Handlers.Add(handler);
                DateTime now = DateTime.Now;
                if (now - lastSweepTime > TimeSpan.FromSeconds(1))
                {
                    lastSweepTime = now;
                    foreach (var handlerNeedsToBeClosed in Handlers)
                    {
                        if (now - handlerNeedsToBeClosed.lastActivity > TimeSpan.FromSeconds(900))
                        {
                            handlersToClose.Add(handlerNeedsToBeClosed);
                        }
                    }
                }
            }
            foreach (var closingHandler in handlersToClose)
            {
                Logging.Debug("Closing timed out TCP connection.");
                closingHandler.Close();
            }
            return true;
        }


    }
}