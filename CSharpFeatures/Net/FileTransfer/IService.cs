using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    public interface IService
    {
        bool Handle(byte[] firstPacket, int length, Socket socket, object state);
    }
}