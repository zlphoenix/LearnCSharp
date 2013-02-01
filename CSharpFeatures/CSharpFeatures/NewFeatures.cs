using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeatures
{
    public class A
    {
        public string Name;
        public string Code;
    }
    public class Class1
    {
        private string Param = "";
        private void Func()
        {
            //初始化器
            var b = new A { Name = "", Code = "" };
            b.Code = "";
            b.Name = "";
            var a = new { Name = "", Code = "" };
            this.Foo("Name");
            this.Foo(Code: "c", Name: "n");

            // ToDO  加入....新特性
            this.Param.Func();

            //Lambda
            List<int> c = new List<int>();
            c.Select(x => x > 1);
            //from x in collection
            //select new {Name = x.Name, Code = x.Code};
        }

        //private void Foo(string Name)
        //{
        //    Foo(Name, "No1");
        //}

        //参数默认值
        private void Foo(string Name, string Code = "")
        {
        }
    }
    //扩展方法
    public static class StringExtention
    {
        public static void Func(this string src)
        {
            Console.Write(src);
        }
    }
}
