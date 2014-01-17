using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpFeatures.Generics
{
    public class GenericsNotTemplates
    {
        /// <summary>
        /// 泛型方法和非泛型方法同时存在,即使类型能够匹配也会使用泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string DoIt<T>(T t)
        {
            return ReallyDoIt(t);
        }
        private static string ReallyDoIt(string s)
        {
            return "string";
        }
        private static string ReallyDoIt<T>(T t)
        {
            return "everything else";
        }
    }



}
