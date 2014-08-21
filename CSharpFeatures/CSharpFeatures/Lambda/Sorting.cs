using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSharpFeatures.Lambda
{
    /// <summary>
    /// 需要排序的类型
    /// </summary>
    public class A
    {
        public string S;
        public B ToData()
        {
            return new B() { S2 = this.S };
        }
        public void FromData(B b)
        {

        }
    }

    public class B
    {
        public string S2;
    }

    public class MainEntity
    {
        public IList<A> As;
    }

    public class MainEntityDTO
    {
        public IList<B> As;
    }

    public static class Sorting
    {

        public static void Test()
        {
            var repo = new List<A>();
            var mylist = new List<A>();
            
            mylist.ForEach(item=>repo.Add(item));


            mylist.Add(new A() { S = "A" });
            mylist.Add(new A() { S = "B" });
            //按照S字段升序
            var result = mylist.OrderBy(a => a.S);
            mylist.OrderBy(a => { return a.S; });
            Print(result);
            //按照S字段逆序
            result = mylist.OrderByDescending(a => a.S);
            Print(result);
            var bList = result.Select(a => new B() { S2 = a.S });
            
            var dic = mylist.ToDictionary(a => a.S);
         
        }

        private static void Print(IOrderedEnumerable<A> result)
        {
            var output = result.Select(a =>
                                           {
                                               Console.WriteLine(a.S);
                                               return a.S;
                                           });

        }
    }
}
