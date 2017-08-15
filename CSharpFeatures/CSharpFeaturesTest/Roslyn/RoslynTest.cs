using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFeatures.Roslyn;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Roslyn
{
    [TestClass]
    public class RoslynTest
    {
        [TestMethod]
        public async Task ScriptTestAsync()
        {
            var demo = new RoslynDemo();
            await demo.HelloWorld();
        }
        [TestMethod]
        public async Task ScriptTestExpression()
        {
            var demo = new RoslynDemo();
            var result = await demo.GetExpressValue();
            Assert.AreEqual(13, result);
        }
        [TestMethod]
        public void ScriptTestMultiLine()
        {
            var demo = new RoslynDemo();
            var result = demo.ExceuteMultiLine();
            Assert.AreEqual(3, result);
        }
        [TestMethod]
        public void UseGlobalObjTest()
        {
            var demo = new RoslynDemo();
            var result = demo.UseGlobalObj();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void ScriptTest()
        {
            Task.Run(async () =>
            {
                var demo = new RoslynDemo();
                await demo.HelloWorld();
            }).GetAwaiter().GetResult();
        }

    }
}
