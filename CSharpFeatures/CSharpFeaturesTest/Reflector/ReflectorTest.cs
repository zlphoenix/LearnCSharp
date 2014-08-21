using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allen.Util.TestUtil;
using CSharpFeatures.Reflactor;
using System.Reflection;

namespace CSharpFeaturesTest.Reflector
{
    [TestClass]
    public class ReflectorTest
    {

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            CodeTimer.Initialize();
        }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
        }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{

        //}
        //
        #endregion


        public ReflectorTest()
        {
            //Console.WriteLine("Ctor with no param");
        }

        public ReflectorTest(string name)
        {
            //Console.WriteLine(string.Format("Ctor with param:{0}", name));
        }
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
            //var method = this.GetType().GetMethod("Method");//, 
            MethodInfo method = this.GetType().GetMethod("Method");
            var methods = this.GetType().GetMethods();
            var ms = (from m in methods
                      where m.Name == "Method"
                      select m).ToList();
            //foreach (var methodInfo in ms)
            //{
            //     methodInfo.GetParameters()
            //}

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

        [TestMethod]
        public void InvokeConstructor()
        {
            var ctor = this.GetType().GetConstructor(new Type[0]);

            Assert.IsNotNull(ctor);
            var product = ctor.Invoke(null);
            Assert.IsNotNull(product);
            Assert.AreEqual(this.GetType(), product.GetType());


            ctor = this.GetType().GetConstructor(new Type[1] { typeof(string) });
            Assert.IsNotNull(ctor);
            product = ctor.Invoke(new object[] { ":)" });
            Assert.IsNotNull(product);
            Assert.AreEqual(this.GetType(), product.GetType());
        }
        [TestMethod]
        public void LoadAssemblyTest()
        {
        
          
            //CodeTimer.Time("加载Assembly测试")
            var assName = this.GetType().Assembly.FullName;
            var count = AppDomain.CurrentDomain.GetAssemblies().Count(a => a.FullName == assName);
            Assert.AreEqual(1, count);
            Assembly.Load(assName);
            count = AppDomain.CurrentDomain.GetAssemblies().Count(a => a.FullName == assName);
            Assert.AreEqual(1, count);

        }

        [TestMethod]
        public void CreateObjPerformenceComparison()
        {
            const int createTimes = 100000;
            CodeTimer.Time("直接创建对象", createTimes, () => new ReflectorTest());
            CodeTimer.Time("Activator(Type)创建对象", createTimes, () => Activator.CreateInstance(typeof(ReflectorTest)));
            CodeTimer.Time("Activator(Str)创建对象", createTimes, () => Activator.CreateInstance("CSharpFeaturesTest", "CSharpFeaturesTest.Reflector.ReflectorTest"));
            CodeTimer.Time("Ctor创建对象", createTimes, () =>
                                                        {
                                                            typeof(ReflectorTest).GetConstructor(new Type[0]).Invoke(null);
                                                        });

        }
        public void Method(ref string i, out int j, ref int k)
        {
            i = "1";
            j = 0;
            k++;
            Print(i, j, k);
        }
        public void Method(string i, int j, int k)
        {

        }


        private static void Print(string i, int j, int k)
        {
            Console.WriteLine(string.Format("i={0}", i));
            Console.WriteLine(string.Format("j={0}", j));
            Console.WriteLine(string.Format("k={0}", k));
        }
    }
}
