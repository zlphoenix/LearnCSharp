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

        /// <summary>
        /// 反射方式调用参数为 传引用的方法
        /// 需要注意的是,Invoke参数中的数组才会被作为引用传递,实际参数不会有这个影响,如果需要得到预期的效果,需要在调用完成后,
        /// 将参数数组的值回填
        /// </summary>
        [TestMethod]
        public void InvokeMethodWithRefParamTest()
        {
            var method = this.GetType().GetMethod("Method");//, new Type[] { typeof(int), typeof(int), typeof(int) });
            Assert.IsNotNull(method);
            string i = "0"; int j = 1, k = 1;
            var param = new object[] { i, j, k };
            method.Invoke(this, param);
            Console.WriteLine("After Invoke...");
            Assert.AreEqual("0", i);
            Assert.AreEqual(1, j);
            Assert.AreEqual(1, k);
            Print(i, j, k);
            Assert.AreEqual("1", param[0]);
            Assert.AreEqual(0, param[1]);
            Assert.AreEqual(2, param[2]);

            //Print((string)param[0], (int)param[1], (int)param[2]);
        }

        public void Method(ref string i, out int j, ref int k)
        {
            i = "1";
            j = 0;
            k++;
            Print(i, j, k);
        }

        private static void Print(string i, int j, int k)
        {
            Console.WriteLine(string.Format("i={0}", i));
            Console.WriteLine(string.Format("j={0}", j));
            Console.WriteLine(string.Format("k={0}", k));
        }
    }
}
