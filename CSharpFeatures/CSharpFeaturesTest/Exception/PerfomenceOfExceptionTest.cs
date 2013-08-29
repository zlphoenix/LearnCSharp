using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allen.Util.TestUtil;

namespace CSharpFeaturesTest.Exception
{
    public delegate string MyFunc();
    /// <summary>
    /// Summary description for PerfomenceOfExceptionTest
    /// </summary>
    [TestClass]
    public sealed class PerfomenceOfExceptionTest
    {
        public PerfomenceOfExceptionTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

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
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ThrowSystemExceptionTest()
        {
            const int calCount = 1000000;
            const int calMin = 100;
            const int step = 10;


            CodeTimer.Time("Devide non zero with Exception dealing ", calCount, calMin, step, () =>
                                                                                                  {
                                                                                                      int i = 100, j = 1;
                                                                                                      DevideZeroException
                                                                                                          (i, j);
                                                                                                  });
            CodeTimer.Time("Devide non zero with Prejudge", calCount, calMin, step, () =>
                                                                                        {
                                                                                            int i = 100, j = 1;
                                                                                            DevideZeroException(i, j);
                                                                                        });
            CodeTimer.Time("Devide zero with Exception dealing", calCount, calMin, step, () =>
                                                                                             {
                                                                                                 int i = 100, j = 0;
                                                                                                 DevideZeroException(i,
                                                                                                                     j);
                                                                                             });
            CodeTimer.Time("Devide zero with Exception Prejudge", calCount, calMin, step, () =>
                                                                                              {
                                                                                                  const int i = 100;
                                                                                                  const int j = 0;
                                                                                                  DevideZero(i, j);
                                                                                              });
        }

        public bool DevideZeroException(int num1, int num2)
        {
            try
            {
                return num1 / num2 == 0;
            }
            catch (System.Exception)
            {

                return false;
            }

        }

        public bool DevideZero(int num1, int num2)
        {
            if (num2 == 0) return false;
            return num1 / num2 == 0;
        }

        //执行结果:
        //Exception                            
        //    Time Elapsed:	1,991ms      
        //    CPU Cycles:	5,753,584,660
        //    Gen 0: 		13           
        //    Gen 1: 		0            
        //    Gen 2: 		0            
        //WithoutException                     
        //    Time Elapsed:	1ms          
        //    CPU Cycles:	3,115,908    
        //    Gen 0: 		0            
        //    Gen 1: 		0            
        //    Gen 2: 		0        

        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void ThrowExceptionAfterFinally()
        {
            var i = 0;
            System.Exception ex = null;

            try
            {
                i++;
                throw new System.Exception("Test");
            }
            catch (System.Exception e)
            {
                //1.在catch中记录的变量,到finally中可以访问到
                ex = e;

                throw;
                //2.throw 之后才会走到finally中
            }
            finally
            {
                i++;
                Assert.AreEqual(2, i);
                Assert.IsNotNull(ex);
                Assert.AreEqual("Test", ex.Message);
            }
        }


        /// <summary>
        /// 反编译看Try catch 的IL
        /// </summary>
        public void ExceptionFlow()
        {
            System.Exception e = null;
            try
            {
                var i = 0;
            }
            catch (System.Exception ex)
            {
                e = ex;
                throw;
            }
            finally
            {
                e = null;
            }
        }
        public const object I = null;
        [MyAttr]
        public void CodeBlock()
        {

            {
                var j = 0;
            }
        }


        public static string Foo()
        {
            return null;
        }
    }

    public class MyAttr : Attribute
    {
        public MyAttr()
        {
        }

        public MyAttr(Func<string> foo, PerfomenceOfExceptionTest test)
        {

        }
        public MyAttr(Func<string> foo)
        {

        }
        public MyAttr(PerfomenceOfExceptionTest test)
        {

        }
        public MyAttr(List<PerfomenceOfExceptionTest> testLis)
        {

        }
        public MyAttr(object testLis)
        {

        }

        public PerfomenceOfExceptionTest Test { get; set; }
    }
}
