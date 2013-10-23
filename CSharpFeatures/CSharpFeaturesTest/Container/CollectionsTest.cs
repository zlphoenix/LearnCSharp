using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeaturesTest.Container
{
    [TestClass]
    public class CollectionsTest
    {
        [TestMethod]
        public void ExceptTest()
        {
            var strSrc = new string[] { "A", "B", "C" };
            var strDest = new string[] { "A", "D", "F" };

            var result = strSrc.Except(strDest).ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Length);
            Assert.IsTrue(!result.Contains("A"));
            Assert.IsTrue(result.Contains("B"));
            Assert.IsTrue(result.Contains("C"));
            
        }
    }
}
