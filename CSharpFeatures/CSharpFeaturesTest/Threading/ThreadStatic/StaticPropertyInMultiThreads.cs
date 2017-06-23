using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.ThreadStatic;
using System.Threading;

namespace CSharpFeaturesTest.ThreadStatic
{
    [TestClass]
    public class StaticPropertyInMultiThreadsTest
    {
        [TestMethod]
        public void PropertyInMultiThreadsHasSameValue()
        {
          
            string startValue = "Foo!";
            string expectedValue = "Bar!";
            SetValue(startValue);

            var thread = new Thread(() => SetValue(expectedValue));
            thread.Start();
            //thread.
            Assert.AreEqual(expectedValue, StaticPropertyInMutliThreads.StaticProperty);
            Assert.AreEqual(startValue, StaticPropertyInMutliThreads.ThreadStaticProperty);
        }

        private void SetValue(string propertyValue)
        {
            StaticPropertyInMutliThreads.StaticProperty = propertyValue;
            StaticPropertyInMutliThreads.ThreadStaticProperty = propertyValue;
        }
    }
}
