using Inspur.Gsp.CSharpIntroduction.Demo.ExtendMethod;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.Syntax
{
    [TestClass]
    public class StringTests
    {

        [TestMethod]
        public void EqualTest()
        {
            var str = "Allen";
            Assert.AreEqual("Mr. Allen", str.Fill("Mr. "));
        }

    }
}
