using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.Generics
{
    public class GenericsNotTemplates
    {
        public static void DoIt<T>(T t)
        {
            ReallyDoIt(t);
        }
        private static void ReallyDoIt(string s)
        {
            System.Console.WriteLine("string");
        }
        private static void ReallyDoIt<T>(T t)
        {
            System.Console.WriteLine("everything else");
        }
    }


  
}
