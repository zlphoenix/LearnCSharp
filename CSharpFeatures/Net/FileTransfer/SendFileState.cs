using System.IO;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{
    public class SendFileState
    {
        public Stream fileStream;
        public byte[] fileBytes;
        public Socket clientSocket;
        public int readFileByteCount;
        public FileInfo fileInfo;
        public long filseSize;
        public int actualByteCount;//实际读写的字节数
    }
}