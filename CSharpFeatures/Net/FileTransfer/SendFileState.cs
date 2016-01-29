using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc
{

    [DebuggerDisplay("File={FileInfo.Name},FileSize={FileSize},TransmitedByteCount={TransmitedByteCount},DealingByteCount={DealingByteCount}")]
    public class SendFileState
    {
        public int Count;
        public string FileName;
        public Stream FileStream;
        public byte[] FileBytes;
        public Socket Connection;
        public int TransmitedByteCount;
        public FileInfo FileInfo;
        public long FileSize;
        public int DealingByteCount;//实际读写的字节数

        public int TotalCount
        {
            get { return (int)FileSize / FileBytes.Length; }
        }

        public int RemainCount
        {
            get
            {
                return TotalCount - Count;
            }
        }

        public void Close()
        {
            if (Connection != null)
            {
                try
                {
                    Logging.Debug("Closing Connection...");
                    if (Connection.Connected)
                        Connection.Shutdown(SocketShutdown.Both);
                    Connection.Close();
                }
                catch (Exception e)
                {
                    Logging.LogUsefulException(e);
                }
            }
            FileStream?.Close();
        }
    }
}