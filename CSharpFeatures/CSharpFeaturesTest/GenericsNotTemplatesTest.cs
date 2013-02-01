using CSharpFeatures.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpFeaturesTest
{
    
    
    /// <summary>
    ///This is a test class for GenericsNotTemplatesTest and is intended
    ///to contain all GenericsNotTemplatesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GenericsNotTemplatesTest
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
        ///A test for DoIt
        ///</summary>
        public void DoItTestHelper<T>()
        {
            T t = default(T); // TODO: Initialize to an appropriate value
            GenericsNotTemplates.DoIt<T>(t);
            GenericsNotTemplates.DoIt("string");
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void DoItTest()
        {
            DoItTestHelper<GenericParameterHelper>();
        }
    }
}
