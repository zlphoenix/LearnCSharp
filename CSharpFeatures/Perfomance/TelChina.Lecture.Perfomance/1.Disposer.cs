using System;

namespace TelChina.Lecture.Perfomance
{
    /// <summary>
    /// 
    /// </summary>
    public class Disposer : IDisposable
    {
        /// <summary>
        /// 需要释放的非托管资源
        /// </summary>
        private object _unManagedRes;
        /// <summary>
        /// 需要释放的托管资源
        /// </summary>
        private object _managedRes;
        /// <summary>
        /// 是否已经被释放的标识
        /// </summary>
        private bool _alreadyDisposed;

        /// <summary>
        /// 释放资源的重载虚方法
        /// </summary>
        /// <param name="isDisposing"></param>
        protected virtual void Dispose(bool isDisposing)
        {

            // 不做重复释放
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                // TODO: 释放托管资源.
                _managedRes = null;
            }
            // TODO: 释放非托管资源.
            _unManagedRes = null;

            // 设置资源释放标识
            _alreadyDisposed = true;

        }
        /// <summary>
        /// 调用资源释放方法实现析构方法
        /// </summary>
        ~Disposer()
        {
            Dispose(false);
        }

        /// <summary>
        /// 显示实现IDisposable接口，调用重载虚函数释放资源，并强制内存回收
        /// </summary>
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
