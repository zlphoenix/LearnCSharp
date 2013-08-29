
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpFeatures.MEF;
using System.ComponentModel.Composition;

namespace CSharpFeaturesTest.MEF
{
    [TestClass]
    public class MEFTest
    {
        [TestMethod]
        public void ComposeTest()
        {
            var host = new AddinHost();
            var importer = new ViewFactory();
            host.Compose(importer);
        }

        /// <summary>
        /// 可以将方法直接注册进来，可以是静态方法也可以是实例方法
        /// </summary>
        [TestMethod]
        public void MethodComposeTest()
        {
            var import = new MethodToImport();
            var host = new AddinHost();
            host.Compose(import);
            Assert.IsNotNull(import.DoInstance);
            import.DoInstance("hello world!");
            Assert.IsNotNull(import.DoStatic);
            import.DoStatic(-1);

            import.EventWithReturnValue += MethodToExport.EventHandler;
            import.EventWithReturnValue += MethodToExport.OtherEventHandler;
            var str = import.OnEventWithReturnValue();
            Assert.AreEqual(MethodToExport.OtherEventHandler(), str);
        }
    }

    public delegate string EventTest();

    public class MethodToImport
    {
        public event EventTest EventWithReturnValue;
        [Import(typeof(Action<string>))]
        public Action<string> DoInstance;

        [Import(typeof(Action<int>))]
        public Action<int> DoStatic;

        public string OnEventWithReturnValue()
        {
            if (EventWithReturnValue != null)
            {
                var str = EventWithReturnValue();
                return str;
            }
            return "";
        }
    }

    public class MethodToExport
    {
        [Export(typeof(Action<string>))]
        public void Do(string str)
        {
            Assert.AreEqual("hello world!", str);
        }

        [Export(typeof(Action<int>))]
        public static void Do(int i)
        {
            Assert.AreEqual(-1, i);
        }


        public static string EventHandler()
        {
            return "AAA";
        }

        public static string OtherEventHandler()
        {
            return "BBb";
        }
    }
}
