using System;

namespace J9Updater.FileTransferSvc
{
    public class Logging
    {
        public static void LogUsefulException(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        public static void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void Debug(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void Error(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}