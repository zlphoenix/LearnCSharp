using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.ExtenedMethod
{
    public static class StringExtenedUseDynamicType
    {
        public static void Test(this string str)
        {
            Console.WriteLine("Test string:" + str);
        }
    }
}
