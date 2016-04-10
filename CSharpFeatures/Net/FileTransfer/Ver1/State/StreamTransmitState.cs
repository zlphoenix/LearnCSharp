using System;
using System.Diagnostics;
using System.IO;

namespace J9Updater.FileTransferSvc.Ver1
{
    [DebuggerDisplay("Size={Size},TransmitedByteCount={TransmitedByteCount},DealingByteCount={DealingByteCount}")]
    internal class StreamTransmitState
    {
        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public Stream Stream { get; set; }
        /// <summary>
        /// 文件读写过程中的数据缓存
        /// </summary>
        public byte[] Buffer { get; set; }
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
        public int TotalCount => (int)(Size / Buffer.Length);

        /// <summary>
        /// 剩余需要传输的块计数器
        /// </summary>
        public int RemainCount => TotalCount - Count;

        /// <summary>
        /// 传输完成状态
        /// </summary>
        public int TransmitState { get; set; }

        /// <summary>
        /// 传输结束后的回调
        /// </summary>
        public Action<StreamTransmitState> AfterTransmitCallback { get; set; }
    }
}