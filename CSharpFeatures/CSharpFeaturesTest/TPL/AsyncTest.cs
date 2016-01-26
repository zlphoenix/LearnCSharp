using CSharpFeatures.TPL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFeaturesTest.TPL
{
    [TestClass]
    public class AsyncTest
    {
        [TestMethod]
        public void AsyncInvokeTestWrongWay()
        {
            Do();
        }


        [TestMethod]
        public async Task AsyncInvokeTest()
        {
            await Do();
        }



        public async Task Do(int index = -1)
        {

            Console.WriteLine("Thread:{0},index{1} woking", Thread.CurrentThread.ManagedThreadId, index);
            var asyncObj = new AsyncFeatrue();
            var length = await asyncObj.AccessTheWebAsync();

            Assert.IsTrue(length != 0);
        }
    }
}
