using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Expression
{
    /// <summary>
    /// Summary description for ExpressionTest
    /// </summary>
    [TestClass]
    public class ExpressionTest
    {
        public ExpressionTest()
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
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
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

        [TestMethod]
        public void TestMethod1()
        {
            //假设ABC是三个布尔表达式（没有副作用），那么A ? B : C是不是就等价于(A && B) || (!A && C)？
            bool a = false, b = false, c = false;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            a = true;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            b = true;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            a = false;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));

            c = true;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            a = true;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            b = false;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
            a = false;
            Assert.AreEqual(Expression1(a, b, c), Expression2(a, b, c));
        }

        private bool Expression1(bool a, bool b, bool c)
        {
            Console.WriteLine(string.Format("{0},{1},{2}", a, b, c));
            return a ? b : c;
        }

        private bool Expression2(bool a, bool b, bool c)
        {
            return (a && b) || (!a && c);
        }

        [TestMethod]
        public void TestAgregate()
        {
            var dic = new Dictionary<string, List<int>>
                {
                        {"123", new List<int> {1, 2, 3}}, 
                        {"64", new List<int> {6, 4}}, 
                        {"6", new List<int> {6}}
                };
            var result = dic.Values.Sum(values => values.Count);

            Assert.AreEqual(6, result);
        }
    }



}
