﻿namespace J9Updater.FileTransferSvc
{
    /// <summary>
    /// 文件传输服务配置，用于初始化文件传输服务的listener
    /// </summary>
    public class FileTransferServiceConfig
    {
        /// <summary>
        /// 服务端口号
        /// </summary>
        public string ServerPort { get; set; }

        /// <summary>
        /// 连接超时时间
        /// </summary>
        public long ConnectionTimeout { get; set; }
    }
}