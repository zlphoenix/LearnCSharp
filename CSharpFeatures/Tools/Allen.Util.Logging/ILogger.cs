using System;
using System.Collections.Generic;
using System.Text;

namespace Allen.Util.Logging
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
        void Error(Exception ex);
    }
}
