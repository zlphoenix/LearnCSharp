
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.MEF;

namespace CSharpFeaturesTest.MEF
{
    [TestClass]
    public class MEFTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var host = new AddinHost();
            var importer = new ViewFactory();
            host.Compose(importer);

        }
    }
}
