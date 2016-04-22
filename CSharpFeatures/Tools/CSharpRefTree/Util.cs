using System;
using System.Collections.Generic;
using System.Text;

namespace Allen.Util.CSharpRefTree
{
    public static class Util
    {
        public static StringBuilder OutputList<T>(this List<T> collection, string title, Func<T, string> print = null)
        {
            if (print == null)
                print = arg => arg.ToString() + ",";
            if (collection.Count == 0) return null;
            var refs = new StringBuilder(title);

            collection.ForEach(item => refs.Append(print(item)));
            return refs;
        }
    }
}