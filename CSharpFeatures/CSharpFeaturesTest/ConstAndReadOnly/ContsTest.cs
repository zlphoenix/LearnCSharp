using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.ConstAndReadOnly
{
    /// <summary>
    /// 从反编译结果看,const类似C中的宏,在声明和使用的双边都会用其值替换,虽然元数据中会出现 const变量,使用过程中是没有的,直接用它的值.
    /// 也就是说,const成员一旦发布,背别的类型引用,如果再做修改必须统统重新编译
    /// static readonly 会在static构造方法中初始化,而且其顺序会与声明的顺序一致
    /// </summary>
    [TestClass]
    public class ConstTest
    {
        static readonly int A = B * 10;
        static readonly int B = 10;
        const int C = D * 10;
        const int D = 10;

        [TestMethod]
        public void ConstAndStaticInitTest()
        {
            Assert.AreEqual(0, A, "A!=0");
            Assert.AreEqual(10, B, "B!=10");

            Assert.AreEqual(100, C, "C!=100");
            Assert.AreEqual(10, D, "D!=10");
        }
    }
}
