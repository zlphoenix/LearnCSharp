using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.Remoting.Messaging;

namespace CallContextText
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            CallContext.LogicalSetData("foo", new Bar(0));
            //var headers = CallContext.GetHeaders();
            //headers.FirstOrDefault();
            CallContext.FreeNamedDataSlot("foo");
        }
    }

    [Serializable]
    public class Bar : IDisposable
    {
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Bar(String str)
        {

        }

        public Bar(int i)
        {
        }
    }


    public class Foo : Bar
    {
        public Foo(string str) : base(str)
        {
        }
    }


}
