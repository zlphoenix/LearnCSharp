using System;
using System.Collections.Generic;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.Syntax
{
    /// <summary>
    /// 测试类命名:《被测类名》Test
    /// </summary>
    [TestClass]
    public class AnimalTest
    {
        /// <summary>
        /// 测试用例命名 《被测方法名》Test
        /// </summary>
        [TestMethod]
        public void ShoutTest()
        {
            Animal animal = new Cat();
            //#.3.2 自动判断结果是否符合预期
            Assert.AreEqual("meow", animal.Shout());

        }

        public void A()
        {

        }
        public void A(int i)
        {

        }

        [TestMethod]
        public void CatNameTest()
        {

            var noOne = new Cat("Tom");
            Assert.AreEqual("Tom", noOne.Name);

            var tom = new Cat { Name = "Garfield" };
            Assert.AreEqual("Garfield", tom.Name);

        }

        /// <summary>
        /// # 期待异常
        /// </summary>
        [ExpectedException(typeof(NotSupportedException), "负值异常没抛出")]
        [TestMethod]
        public void ShoutNumTest()
        {
            var cat = new Cat();
            Assert.AreEqual(0, cat.ShoutNum, "初值错误");

            cat.ShoutNum = 5;
            //自动判断结果是否符合预期
            Assert.AreEqual(5, cat.ShoutNum, "赋值无效");

            cat.ShoutNum = 15;
            Assert.AreEqual(10, cat.ShoutNum, "上限值错误");

            cat.ShoutNum = -1;
        }
        /// <summary>
        /// #.3.3 异常抛出和截获过程
        /// </summary>
        [TestMethod]
        public void ExceptionTest()
        {

            var cat = new Cat();
            try
            {
                cat.ShoutNum = -1;
            }
            catch (NotSupportedException e)
            {
                Console.WriteLine(e);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                Console.WriteLine("这里总会走");
            }


        }

        [TestMethod]
        public void CatShoutEventTest()
        {
            var tom = new Cat { Name = "Tom" };
            //#.1.8.3 注册事件并不会立即触发
            //#.1.8.4 事件用来解耦
            tom.CatShout += new CatShoutEventHandler(CatShouted);

            //#.1.8.5 事件多播
            //tom.CatShout += CatShouted;

            tom.CatShout +=
                delegate (Cat sender, string sound)
                {
                    Console.WriteLine("{0} has Shouted. Another Way", sender.Name);
                };


            //Lanmbda 表达式的本质是匿名委托+隐式类型
            tom.CatShout +=
                (sender, sound) =>
                {
                    Console.WriteLine("{0} has Shouted. lambda", sender.Name);
                };

            Console.WriteLine("After regist event");
            tom.Shout();

            Compare(10, 100, (left, right) => left - right);
            Compare(10, 100, (left, right) => right - left);



        }

        public int Compare<T>(T left, T right, Func<T, T, int> order)
        {
            return order(left, right);
        }
        private void CatShouted(Cat sender, string shoutsound)
        {
            Console.WriteLine("{0} has Shouted ", sender.Name);
        }

        [TestMethod]
        public void ChangeTest()
        {
            var changeableAnimal = new List<IChange>
            {
                new MachineCat("哆啦A梦"),
                new MarvellousMonkey
                {
                   Name = "孙悟空"
                }
            };
            foreach (var animal in changeableAnimal)
            {
                Console.WriteLine(animal.Change("金箍棒"));
            }

            IChange newMonkey = new MarvellousMonkey()
            {
                Name = "六耳猕猴"
            };
            newMonkey.Change("孙悟空");
        }
        [TestMethod]
        public void HidenTest()
        {
            Cat cat = new Cat();
            Animal animal = new Cat();

            Assert.AreEqual("I'm eating... I'm full", cat.Eat());
            Assert.AreEqual("I'm eating...", animal.Eat());
        }
    }
}
