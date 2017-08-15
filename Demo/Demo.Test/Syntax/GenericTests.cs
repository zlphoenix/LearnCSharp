using Inspur.Gsp.CSharpIntroduction.Demo.Generic;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.Syntax
{
    [TestClass]
    public class GenericTests
    {

        [TestMethod]
        public void StaticPropertyOfGenericType()
        {
            Queue<Cat>.Count++;
            Assert.AreNotEqual(Queue<Cat>.Count, Queue<Animal>.Count);
        }
        [TestMethod]
        public void DoGenericTest()
        {
            var obj = new UseGeneric();
            obj.UseCollection();
        }
    }
}
