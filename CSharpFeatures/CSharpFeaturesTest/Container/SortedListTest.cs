using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Container
{
    /// <summary>
    /// Summary description for SortedListTest
    /// </summary>
    [TestClass]
    public class SortedListTest
    {
        public SortedListTest()
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
        public void TestAdd()
        {

            var item2 = new SortableItem() { Name = "2", OrderIndex = 2 };
            var item1 = new SortableItem() { Name = "1", OrderIndex = 1 };
            var collection = new SortedCollection<int, SortableItem>();
            collection.Add(item2);
            collection.Add(item1);
            var result = collection.Select(item => item.Value).ToList();
            Assert.AreEqual(item1, result[0]);
            Assert.AreEqual(item2, result[1]);

            item1.OrderIndex = 3;

        }
    }

    public class SortableItem : ISortable<int>
    {

        public int OrderIndex
        {
            get
                ;
            set
                    ;
        }

        public string Name { get; set; }

    }

    public interface ISortable<T>
    {
        T OrderIndex { get; set; }
    }

    /// <summary>
    /// 可排序集合基类
    /// </summary>
    /// <typeparam name="TKey">用于排序的实体主键属性的数据类型</typeparam>
    /// <typeparam name="TValue">集合成员实体类型</typeparam>
    public class SortedCollection<TKey, TValue> :
        SortedList<TKey, TValue>
        where TValue : ISortable<TKey>, new()
    {

        public void Add(TValue item)
        {
            this.Add(item.OrderIndex, item);
        }

    }


}
