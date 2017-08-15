using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Lambda
{
    /// <summary>
    /// 函数式风格编程测试
    /// </summary>
    [TestClass]
    public class FunctionalTest
    {
        /// <summary>
        /// 把函数当成变量来用，关注于描述问题而不是怎么实现
        /// </summary>
        [TestMethod]
        public void CurryingTest()
        {
            var inc = new Func<int, Func<int, int>>(x => y => x + y);

            var inc2 = inc(2);
            var inc5 = inc(5);
            Assert.AreEqual(8, inc2(inc5(1)));

        }

        [TestMethod]
        public void PipelineFucTest()
        {
            var inc = new Func<int, Func<int, int>>(x => y => x + y);
            var fucs = new[] { inc(2), inc(3), inc(5) };

            var result = fucs.Aggregate(10, ((i, func) => func(i)));
            Assert.AreEqual(20, result);
        }
    }
}
