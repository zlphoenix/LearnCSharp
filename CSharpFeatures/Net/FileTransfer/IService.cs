using System;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{

    public interface IService : IDisposable
    {
        /// <summary>
        /// 服务路由匹配
        /// </summary>
        /// <param name="handshakeMsg">消息字节流</param>
        /// <param name="length">消息长度</param>
        /// <param name="connection">接受客户端请求的socket连接</param>
        /// <returns>如果匹配当前服务消息格式，返回true</returns>
        bool Handshake(byte[] handshakeMsg, int length, Socket connection);
        bool Handle(byte[] handshakeMsg, int length, Socket socket, object state);
    }
}