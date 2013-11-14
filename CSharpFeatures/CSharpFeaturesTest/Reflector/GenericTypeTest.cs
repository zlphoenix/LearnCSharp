using System;
using System.Collections.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSharpFeaturesTest.Reflector
{
    [TestClass]
    public class GenericTypeTest
    {
        [TestMethod]
        public void GetTypeFromTypeName()
        {
            var ps = new List<Persion>();
            var t = ps.GetType();
            Console.WriteLine(t);

            Console.WriteLine(t.FullName);

            var t2 = Type.GetType(t.FullName);
            Assert.AreEqual(t, t2);

            Console.WriteLine(typeof(Dictionary<string, Persion>).FullName);
            var pa1 = new Persion[0];
            var pa2 = new Persion[10];


            var t3 = Type.GetType(pa1.GetType().FullName);
            Console.WriteLine(t3.FullName);
            var t4 = Type.GetType(pa2.GetType().FullName);
            Console.WriteLine(t4.FullName);
        }
    }

    public class Persion
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
