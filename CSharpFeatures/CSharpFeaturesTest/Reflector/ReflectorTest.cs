using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allen.Util.TestUtil;
using CSharpFeatures.Reflactor;

namespace CSharpFeaturesTest.Reflector
{
    [TestClass]
    public class ReflectorTest
    {
        [TestMethod]
        public void CreateObjWithReflector()
        {
            CodeTimer.Initialize();

            for (int count = 10000; count < 100000000; count *= 10)
            {
                CodeTimer.Time("直接创建对象", count, () => new Entity());
                CodeTimer.Time("反射创建对象", count, () => Activator.CreateInstance(typeof(Entity)));
            }
        }
        [TestMethod]
        public void InvokeMethod()
        {
            var obj = new ReflectorPerformance();
            var method = obj.GetType().GetMethod("Invoke");
            var param = new object[] { 10, 100 };
            for (int count = 100; count <= 1000000; count *= 10)
            {

                CodeTimer.Time("直接调用", count, () => obj.Invoke(10, 100));
                CodeTimer.Time("反射调用", count, () => method.Invoke(obj, param));
            }
        }
        [TestMethod]
        public void InvokeMethodWithoutLoad()
        {
            var obj = new ReflectorPerformance();
            var method = obj.GetType().GetMethod("InvokeWithoutLoad");
            var param = new object[] { 10, 100 };
            for (int count = 100; count <= 1000000; count *= 10)
            {

                CodeTimer.Time("直接调用", count, obj.InvokeWithoutLoad);
                CodeTimer.Time("反射调用", count, () => method.Invoke(obj, null));
            }
        }
    }
}
