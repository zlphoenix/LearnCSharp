using System;

namespace J9Updater.FileTransferSvc
{
    class FileTransferService : IFileTransferService
    {
        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 服务初始化
        /// </summary>
        public void Init(FileTransferServiceConfig cfg)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Open()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}