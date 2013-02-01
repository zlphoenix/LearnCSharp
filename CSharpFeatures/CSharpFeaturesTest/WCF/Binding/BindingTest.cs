using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.WCF.Binding;
using System.Threading;

namespace CSharpFeaturesTest.WCF.Binding
{
    [TestClass]
    public class BindingTest
    {
        [TestMethod]
        public void BindingTransTest()
        {
            Server sv = new Server();
            Thread t = new Thread(sv.StartSv);
            t.Start();
            Client c = new Client();
            c.ConsumeSv();
        }
    }
}
