using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.Lambda;
using System.Threading.Tasks;
using Allen.Util.TestUtil;
using System.Collections.Generic;

namespace CSharpFeaturesTest.Lambda
{
    [TestClass]
    public class ClosureTest
    {
        private event Action<int> Do;
        [TestMethod]
        public void AccessToModifiedClosure()
        {
            //这里的j不是局部变量，而是匿名类的成员
            var j = 10;
            Do = (i) =>
             {
                 //闭包内部实用外部变量，取决于执行方法时变量的值而不是定义时
                 Assert.AreEqual(i, j++);//闭包内修改变量的值会影响外部。
             };

            j = 100;
            Do(100);
            Assert.AreEqual(101, j);//这里访问的是匿名类型的成员，所以获取的是闭包内部修改了的j的值。
        }

        [TestMethod]
        public void ClosurePerformanceTest()
        {
            var o = new Closure();
            var r = new Random((int)DateTime.Now.ToBinary());
            var iteration = 100;
            o.Sample = new List<int>(iteration);
            for (int i = 0; i < iteration; i++)
            {
                o.Sample.Add(r.Next(0, 1024));
            }

            //准备一个有1k元素的集合，遍历，测试闭包性能
            Parallel.For(0, 1024, i => o.Content.Add(i));
            CodeTimer.Initialize();

            CodeTimer.Time("闭包方式测试", 100000, 100, 10, (samples) =>
            {
                samples.UseClosure();
                return string.Empty;
            }, o);
            //o.UseClosure()

            CodeTimer.Time("迭代方式测试", 100000, 100, 10, (samples) =>
            {
                samples.NoClosure();
                return string.Empty;
            }, o);
        }
    }
}
