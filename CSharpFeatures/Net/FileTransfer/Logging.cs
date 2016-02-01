using System;
using System.Diagnostics;
using System.Threading;

namespace J9Updater.FileTransferSvc
{
    public class Logging
    {
        public static Stopwatch sw = new Stopwatch();
        private const string Format = "T:{0},TH:{1},M:{2}";
        public static void LogUsefulException(Exception ex)
        {
            Write(ex.Message);
        }

        public static void Info(string msg)
        {
            Write(msg);
        }

        public static void Debug(string msg)
        {
            Write(msg);
        }

        public static void Error(string msg)
        {
            Write(msg);
        }

        private static void Write(string msg)
        {
            Console.WriteLine(Format, DateTime.Now, Thread.CurrentThread.ManagedThreadId, msg);
        }
    }
}