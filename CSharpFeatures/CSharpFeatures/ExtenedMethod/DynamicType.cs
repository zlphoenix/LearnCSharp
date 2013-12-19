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

    public class InvokeExtMethodWithNullThisParam
    {
        /// <summary>
        /// 对于扩展方法,完全可以由null实例来调用.
        /// 换言之,不能保证传入扩展方法的this参数为非空
        /// </summary>
        /// <returns></returns>
        public static string Do()
        {
            string s = null;
            s.Test();
            return "Succeed!";
        }
    }
}
