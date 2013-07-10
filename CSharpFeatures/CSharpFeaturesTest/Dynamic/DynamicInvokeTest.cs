using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allen.Util.TestUtil;
using CSharpFeatures.Dynamic;
using System.Linq.Expressions;

namespace CSharpFeaturesTest.Dynamic
{
    /// <summary>
    /// Summary description for DynamicInvokeTest
    /// </summary>
    [TestClass]
    public class DynamicInvokeTest
    {
        public DynamicInvokeTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
            InvokeTimes = 100;
            CreateTimes = 100000;
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

        public void DirectInvokeTest()
        {
            CodeTimer.Time("DirectInvokeTest", 10000, DirectInvoke);
        }
        public void InterfaceInvokeTest()
        {
            CodeTimer.Time("InterfaceInvokeTest", 10000, InterfaceInvoke);
        }
        public void DelegateInvokeTest()
        {
            CodeTimer.Time("DelegateInvokeTest", 10000, DelegateInvoke);
        }
        public void LambdaInvokeTest()
        {
            CodeTimer.Time("LambdaInvokeTest", 10000, LambdaInvoke);
        }
        public void ReflectionInvokeTest()
        {
            CodeTimer.Time("ReflectionInvoke", 10000, ReflectionInvoke);
        }

        public void ReflectionInvokeTest2()
        {
            var obj = new InvokeMethod();
            MethodInfo m = null;
            CodeTimer.Time("ReflectionInvoke.Create", CreateTimes, () => m = CreateMethodInfo(obj));
            CodeTimer.Time("ReflectionInvoke", 10000, () => ReflectionInvoke(m, obj));
        }
        [TestMethod]
        public void LambdaInvokeTest2()
        {
            var obj = new InvokeMethod();
            Action<InvokeMethod, int> a =
                CodeTimer.Time<int, Action<InvokeMethod, int>>("LambdaInvokeTest.Create",
                    CreateTimes, (int i) => CreateLambda(), 100);

            CodeTimer.Time<Tuple<InvokeMethod, Action<InvokeMethod, int>, int>, object>("LambdaInvokeTest", 10000, (tuple) =>
            {
                tuple.Item2(tuple.Item1, tuple.Item3);
                return null;
            }, new Tuple<InvokeMethod, Action<InvokeMethod, int>, int>(obj, a, InvokeTimes));// Tuple<InvokeMethod, Action<InvokeMethod, int>>());
        }


        public void DelegateInvokeTest2()
        {
            var obj = new InvokeMethod();
            Action<int> a = null;
            CodeTimer.Time("DelegateInvokeTest.Create", CreateTimes, () => a = CreateDelegate(obj));
            CodeTimer.Time("DelegateInvokeTest", 10000, () => a.Invoke(InvokeTimes));
        }
        public void DirectInvokeTest2()
        {
            var obj = new InvokeMethod();
            CodeTimer.Time("DirectInvokeTest.Create", CreateTimes, () => new InvokeMethod());
            CodeTimer.Time("DirectInvokeTest", 10000, () => obj.Do(InvokeTimes));
        }
        [TestCategory("DynamicInvoke"), TestMethod]
        public void AllTest()
        {
            DirectInvokeTest();
            InterfaceInvokeTest();
            DelegateInvokeTest();
            LambdaInvokeTest();
            ReflectionInvokeTest();
        }
        [TestCategory("DynamicInvoke"), TestMethod]
        public void AllTest2()
        {
            DirectInvokeTest2();
            //InterfaceInvokeTest2();
            DelegateInvokeTest2();
            LambdaInvokeTest2();
            ReflectionInvokeTest2();
        }

        #region Excute

        /// <summary>
        /// 迭代执行次数
        /// </summary>
        public static int InvokeTimes { get; set; }
        public static int CreateTimes { get; set; }

        public static void DirectInvoke()
        {
            var obj = new InvokeMethod();
            obj.Do(InvokeTimes);
        }
        public static void InterfaceInvoke()
        {
            IInvokeMethod obj = new InvokeMethod();
            obj.Do(InvokeTimes);
        }

        public static void ReflectionInvoke()
        {
            var obj = new InvokeMethod();
            var m = CreateMethodInfo(obj);
            m.Invoke(obj, new object[] { InvokeTimes });
        }

        public static void DelegateInvoke()
        {
            var obj = new InvokeMethod();
            var action = CreateDelegate(obj);
            action(InvokeTimes);
        }
        public static void LambdaInvoke()
        {
            var obj = new InvokeMethod();
            var action = CreateLambda();
            action.Invoke(obj, InvokeTimes);
        }

        public static void ReflectionInvoke(MethodInfo m, InvokeMethod obj)
        {
            //var obj = new InvokeMethod();
            //var m = CreateMethodInfo(obj);
            m.Invoke(obj, new object[] { InvokeTimes });
        }

        //public static void EmitInfoke()
        //{

        //}
        #endregion

        #region PrepareEnvironment

        private static MethodInfo CreateMethodInfo(InvokeMethod obj)
        {
            var t = typeof(InvokeMethod);
            var m = t.GetMethod("Do");

            return m;
        }
        private static Action<int> CreateDelegate(InvokeMethod obj)
        {
            var action = new Action<int>(obj.Do);
            return action;
        }
        //private static Action<int> CreateLambda(InvokeMethod obj)
        //{
        //    Expression<Action<int>> lambda = (int i) => obj.Do(i);
        //    var action = lambda.Compile();
        //    return action;
        //}

        private static Action<InvokeMethod, int> CreateLambda()
        {
            var param_im = Expression.Parameter(typeof(InvokeMethod), "im");
            var param_i = Expression.Parameter(typeof(int), "i");
            //var createObj = Expression.New(typeof(InvokeMethod));
            //var lambda = Expression.Lambda<Action<int>>(
            //    Expression.Call(typeof(InvokeMethod), "Do", new Type[] { typeof(int) }, new Expression[] { param_i }));
            var lambda = Expression.Lambda<Action<InvokeMethod, int>>(
                Expression.Call(param_im, typeof(InvokeMethod).GetMethod("Do"), param_i), param_im, param_i);

            Expression<Action<InvokeMethod, int>> lambda2 = (im, i) => im.Do(i);

            var action = lambda.Compile();
            return action;
        }

        #endregion


    }
}
