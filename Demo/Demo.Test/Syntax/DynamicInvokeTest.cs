using Inspur.Gsp.CSharpIntroduction.Demo.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.Syntax
{
    [TestClass]
    public class DynamicInvokeTest
    {
        [TestMethod]
        public void InvokeTest()
        {
            var invoker = new DynamicInvoke();
            Assert.AreEqual("Hello", invoker.Invoke());
            Assert.AreEqual("World", invoker.InvokeMyMethod());
        }
    }
}
