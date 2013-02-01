using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TelChina.Lecture.Perfomance
{
    public class DealWithString
    {

        private static readonly string STR = "0123456789";

        public static string NormalConcat(int count)
        {
            var result = "";
            for (int i = 0; i < count; i++) result += STR;
            return result;
        }

        public static string StringBuilder(int count)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < count; i++) builder.Append(STR);
            return builder.ToString();
        }

        public static string StringConcat(int count)
        {
            var array = new string[count];
            for (int i = 0; i < count; i++) array[i] = STR;
            return String.Concat(array);
        }

        public static void StringFormat(int count)
        {
            var template = "{0},{1},{2}";
            for (int i = 0; i < count; i++) string.Format(template, STR, STR, STR);
        }


        public static void StringBuilderFormat(int count)
        {
            var template = "{0},{1},{2}";
            StringBuilder builder = new StringBuilder(350);
            for (int i = 0; i < count; i++)
            {
                builder.AppendFormat(template, STR, STR, STR);
            }
        }
    }
}

