using Inspur.Gsp.CSharpIntroduction.Demo.OO;
using Inspur.Gsp.CSharpIntroduction.Demo.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoTests.Other
{
    [TestClass()]
    public class ObjectCloneTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            var cloner = new ObjectClone()
            {
                Pet = new Cat("Tom")
            };
            var newOne = cloner.Clone() as ObjectClone;
            Assert.IsNotNull(newOne);
            Assert.AreNotSame(cloner, newOne);
            Assert.AreNotSame(cloner.Pet, newOne.Pet);
            Assert.AreEqual(cloner.Pet.Name, newOne.Pet.Name);

        }
    }
}