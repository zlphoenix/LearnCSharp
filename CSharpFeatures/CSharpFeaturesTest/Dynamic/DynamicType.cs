using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Dynamic
{
    [TestClass]
    public class DynamicType
    {
        [TestMethod]
        public void ValueTypeInDynamic()
        {
            var i = 10;
            dynamic d = i;
            d++;
            i = d;//动态类型可以为强类型赋值
            Assert.AreEqual(i, d);

            dynamic dObj = new DType();
            dObj.Order = 10;
            dObj.Order++;
            Assert.AreEqual(i, dObj.Order);

        }
    }

    public class DType
    {
        public int Order;
    }
}
