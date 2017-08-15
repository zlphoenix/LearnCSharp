using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.Delegate;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.Syntax
{
    [TestClass]
    public class DelegateTest
    {
        [TestMethod]
        public void ClosureTest()
        {
            var obj = new UsingDelegate();
            Assert.AreEqual(2, obj.Closure());
        }
     

        [TestMethod]
        public void EventTest()
        {
            var tom = new Cat("Tom");
            var jerry = new Mouse() { Name = "Jerry" };

            //tom.OnShout = jerry.Run;
            tom.CatShout += jerry.Run;
            tom.CatShout += Do;


            tom.Shout();
            var list = new List<Animal>
            {
                new Cat("Tom"),
                new Mouse() { Name = "Jerry" },
            };

            var anAnimalNamedTom =
                list.Where(
                    item => item.Name.Equals("Tom")
                    );

            var strList = new List<string>();

            var result = strList.Aggregate(new StringBuilder(), (sb, s) =>
                 sb.Append(s));

            result.ToString();

            anAnimalNamedTom.Count();

            anAnimalNamedTom.ToList();
            //animals.ToList();
            //animals.Any();
            var animals = from a in list
                          where a.Name.Equals("Tom")
                          select a;
        }

        public static void Do(Cat cat, string sound)
        {
            Console.WriteLine("{0} is Shouting: {1}", cat.Name, sound);
        }
    }
}
