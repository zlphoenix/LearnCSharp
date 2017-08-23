using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspur.Gsp.CSharpIntroduction.Demo.Stub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Stub.Tests
{
    [TestClass()]
    public class PubTest
    {
        [TestMethod()]
        public void GetInComeTest()
        {
            var pub = new Pub(null);
            Assert.AreEqual(0, pub.GetInCome());
        }
    }
}