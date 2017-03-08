using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{
    internal class TransmitConfig
    {
        public Socket Connection { get; set; }

        public int BufferSize { get; set; }

        public void Close()
        {
            Connection.Close();
        }
    }
}