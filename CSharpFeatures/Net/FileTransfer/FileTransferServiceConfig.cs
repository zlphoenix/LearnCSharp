namespace J9Updater.FileTransferSvc
{
    /// <summary>
    /// 文件传输服务配置，用于初始化文件传输服务的listener
    /// </summary>
    public class FileTransferServiceConfig
    {
        /// <summary>
        /// 本地端口
        /// </summary>
        public int LocalPort { get; set; }

        /// <summary>
        /// 服务端口号
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 服务端地址
        /// </summary>
        public string ServerAddress { get; set; }

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public long ConnectionTimeout { get; set; }

        public int BufferSize { get; set; }

        public FileTransferServiceConfig()
        {
            BufferSize = 8192;
        }
    }
}