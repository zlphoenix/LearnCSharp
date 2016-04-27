using System;
using System.Collections.Generic;
using System.Text;

namespace Inspur.GSP.Bom.Builder
{
    public static class Util
    {
        public static StringBuilder OutputList<T>(this List<T> collection, string title, Func<T, string> print = null)
        {
            if (print == null)
                print = arg => arg.ToString() + ",\n";
            if (collection == null) return null;
            var refs = new StringBuilder(title).Append("Count:").Append(collection.Count).Append("\n");
            collection.ForEach(item => refs.Append(print(item)));
            return refs;
        }

        public static void Log(string msg, LogLevel level = LogLevel.Info)
        {
            var color = Console.ForegroundColor;
            if (level == LogLevel.Error)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = color;

        }

        public static void Log(StringBuilder stringBuilder, LogLevel level = LogLevel.Info)
        {
            Log(stringBuilder.ToString(), level);
        }
    }
    public enum LogLevel
    {
        Info,
        Debug,
        Error,
        Warning
    }
}