using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
