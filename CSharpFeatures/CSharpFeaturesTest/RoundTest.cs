using CSharpFeatures.BCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpFeaturesTest
{


    /// <summary>
    ///This is a test class for RoundTest and is intended
    ///to contain all RoundTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RoundTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IntRound
        ///</summary>
        [TestMethod()]
        public void IntRoundTest()
        {
            var target = new Round();
            var a = 10;
            var b = 10;
            var expected = 1;
            var actual = target.IntRound(a, b);
            Assert.AreEqual(expected, actual);

            a = 10;
            b = 15;
            expected =1;
            actual = target.IntRound(a, b);
            Assert.AreEqual(expected, actual);

            a = 10;
            b = 30;
            expected = 1;
            actual = target.IntRound(a, b);
            Assert.AreEqual(expected, actual);

            a = 15;
            b = 10;
            expected = 2;
            actual = target.IntRound(a, b);
            Assert.AreEqual(expected, actual);
        }
    }
}
