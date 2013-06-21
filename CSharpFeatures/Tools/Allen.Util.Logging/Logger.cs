using System;
using System.Collections.Generic;
using System.Text;

namespace Allen.Util.Logging
{
    internal class Logger : ILogger
    {
        private log4net.ILog iLog;

        public Logger(log4net.ILog iLog)
        {
            // TODO: Complete member initialization
            this.iLog = iLog;
        }
        /// <summary>
        /// 调试信息-级别0
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            iLog.Debug(message);
        }
        /// <summary>
        /// 日常信息-级别1
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            iLog.Info(message);
        }
        /// <summary>
        /// 业务异常-级别2
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            iLog.Warn(message);
        }
        /// <summary>
        /// 系统异常-级别3
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            iLog.Error(message);
        }
        /// <summary>
        /// 系统异常-级别3
        /// 输出异常信息
        /// </summary>
        /// <param name="ex"></param>
        public void Error(Exception exception)
        {
            if (exception.InnerException != null)
            {
                Error(exception.InnerException);
            }
            var errorMessage = string.Format("[{0}],系统执行过程中发生异常:异常类型{1},异常信息:{2},异常堆栈{3}",
                                             DateTime.Now.ToShortTimeString(),
                                             exception.GetType().ToString(),
                                             exception.Message,
                                             exception.StackTrace
                );
            Console.WriteLine(errorMessage);
            this.Error(errorMessage);
        }
    }
}
