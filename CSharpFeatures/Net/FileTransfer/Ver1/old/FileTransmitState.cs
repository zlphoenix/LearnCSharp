using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace J9Updater.FileTransferSvc.Ver1
{

    [DebuggerDisplay("File={FileInfo.Name},FileSize={FileSize},TransmitedByteCount={TransmitedByteCount},DealingByteCount={DealingByteCount}")]
    public class FileTransmitState
    {

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public FileInfo FileInfo { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; } = -1;
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream FileStream { get; set; }
        /// <summary>
        /// 文件读写过程中的数据缓存
        /// </summary>
        public byte[] Buffer { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public Socket Connection { get; set; }
        /// <summary>
        /// 已经执行的传输次数计数器
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 已经传输的字节数
        /// </summary>
        public int TransmitedByteCount { get; set; }

        /// <summary>
        /// 实际已经传输的字节数
        /// </summary>
        public int DealingByteCount { get; set; }
        /// <summary>
        /// 实际已经执行传输的块计数器
        /// </summary>
        public int TotalCount => (int)FileSize / Buffer.Length;

        /// <summary>
        /// 剩余需要传输的块计数器
        /// </summary>
        public int RemainCount
        {
            get
            {
                return TotalCount - Count;
            }
        }

        /// <summary>
        /// 传输完成状态
        /// </summary>
        public int TransmitState { get; set; }

        /// <summary>
        /// 传输结束后的回调
        /// </summary>
        public Action<FileTransmitState> AfterTransmitCallback { get; set; }


        /// <summary>
        /// 关闭相关资源
        /// </summary>
        public void Close()
        {
            Logging.Debug("Closing State...");
            try
            {
                FileStream?.Close();
                if (Connection == null) return;
                if (Connection.Connected)
                {
                    Connection.Shutdown(SocketShutdown.Both);
                }
                Connection.Close();
            }
            catch (Exception e)
            {
                Logging.LogUsefulException(e);
            }

        }
    }
}