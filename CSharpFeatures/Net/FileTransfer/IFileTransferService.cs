using System;

namespace J9Updater.FileTransferSvc
{
    /// <summary>
    /// 文件传输服务接口定义
    /// </summary>
    public interface IFileTransferService : IDisposable
    {
        /// <summary>
        /// 服务初始化
        /// </summary>
        void Init(FileTransferServiceConfig cfg);
        /// <summary>
        /// 启动服务
        /// </summary>
        void Open();
        /// <summary>
        /// 关闭服务
        /// </summary>
        void Close();
    }
}
