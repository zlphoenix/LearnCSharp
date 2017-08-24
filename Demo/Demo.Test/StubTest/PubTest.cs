using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Remoting.Messaging;
using Inspur.Gsp.CSharpIntroduction.Demo.Stub;
using Inspur.Gsp.CSharpIntroduction.DemoLib.StubLib;
using Inspur.Gsp.CSharpIntroduction.DemoLib.StubLib.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Test.StubTest
{
    [TestClass]
    public class PubTest
    {
        /// <summary>
        /// 实例属性，当测试用例启动前，被框架赋值，可以获取测试运行上下文
        /// </summary>
        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void ClassInitialize(TestContext ctx)
        {
            Console.WriteLine($"Class {ctx.TestName} is Initialized");
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Console.WriteLine($"Test {TestContext.TestName} is Initialized");
            Console.WriteLine("TestDir: {0}", TestContext.TestDir);
            Console.WriteLine("TestDeploymentDir: {0}", TestContext.TestDeploymentDir);
            Console.WriteLine("TestLogsDir: {0}", TestContext.TestLogsDir);

            if (!TestContext.Properties.Contains("My Property"))
                TestContext.Properties.Add("My Property", "Hello");
        }

        #region TestRun

        /// <summary>
        /// #.3.4.3 Stub
        /// </summary>
        [TestMethod]
        public void ChargeCustomerCountTest()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = () => new DateTime(2017, 8, 18);
                Console.WriteLine("test is begine...");
                //准备测试桩
                var stubCheckInFee = new StubICheckInFee
                {
                    //需要注入的方法:方法名<参数列表>
                    GetFeeCustomer =
                        customer => 100m
                };

                //用Stub装配Pub实例
                var target = new Pub(stubCheckInFee);

                //准备测试数据
                var customers = new List<Customer>
                {
                    new Customer {Gender = Gender.Male},
                    new Customer {Gender = Gender.Female},
                    new Customer {Gender = Gender.Transgender},
                };


                //act
                var actual1 = target.CheckIn(customers);
                var actual = actual1;

                decimal expected = 1;

                //assert
                Assert.AreEqual(expected, actual);
            }
        }
        /// <summary>
        /// #3.4.4 Shim
        /// </summary>
        [TestMethod]
        public void FridayChargeCustomerCountTest()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = () => new DateTime(2017, 8, 18);
                var actual = DoTest();
                //expected
                decimal expected = 1;

                //assert
                Assert.AreEqual(expected, actual);
                //#.3.4.5 到这里就可以了吗？

                System.Fakes.ShimDateTime.TodayGet = () => new DateTime(2017, 8, 14);

                actual = DoTest();
                expected = 3;
                //assert
                Assert.AreEqual(expected, actual);
            }
        }

        private static int DoTest()
        {
            //准备测试桩
            var stubCheckInFee = new StubICheckInFee
            {
                //需要注入的方法:方法名<参数列表>
                GetFeeCustomer = GetFeeCustomer
            };

            //用Stub装配Pub实例
            var target = new Pub(stubCheckInFee);

            //准备测试数据
            var customers = new List<Customer>
            {
                new Customer {Gender = Gender.Male},
                new Customer {Gender = Gender.Female},
                new Customer {Gender = Gender.Transgender},
            };


            //act
            var actual1 = target.CheckIn(customers);
            var actual = actual1;
            return actual;
        }


        [TestMethod]
        public void UseContext()
        {
            Assert.AreEqual("Hello", TestContext.Properties["My Property"]);
        }

        private static decimal GetFeeCustomer(Customer customer)
        {
            return 100;
        }

        /// <summary>
        /// #.3.5 Data Driven Test
        /// </summary>
        [TestMethod]
        [Priority(1)]
        // 使用数据库表作为测试数据源
        // 使用方法:先执行DDL_dbo.TestDataTable.sql脚本，创建测试数据表，并填充几行用于测试的数据，然后执行此测试用例
        [DataSource("Provider=SQLNCLI11;Server=(local);Database=GSP7UnitTest;Uid=sa;Pwd=Test", "Customer")]
        public void DataSourceDrivenTest()
        {
            Assert.IsNotNull(TestContext.DataConnection);
            DoDataDrivenTest();
        }

        private void DoDataDrivenTest()
        {
            Gender gender;
            if (!Enum.TryParse(TestContext.DataRow[1].ToString(), out gender))
            {
                Assert.Fail("获取测试数据错误");
            }
            //#.3.5.1 使用数据库表可以用列名访问，使用csv只能用数字索引器访问
            var customer = new Customer { Gender = gender, Seq = TestContext.DataRow.Field<int>(0) };
            Console.WriteLine($"{customer.Seq},{customer.Gender}");
            Assert.AreNotEqual(0, customer.Seq);
        }

        /// <summary>
        /// #.3.5.2 使用csv准备数据
        /// </summary>
        /// <remarks>
        /// 第一个参数是csv驱动
        /// 第二个参数是测试数据源csv文件名，不需要路径，作为数据库存在，不能为空
        /// 第三个参数是也是文件名，其中逗号替换成#，数据驱动中使用类似SQL的方式访问csv文件，这个作为表名存在，也不能为空
        /// </remarks>
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "data.csv", "data#csv", DataAccessMethod.Sequential)]
        //意图：以项目目录为相对于Build Output的路径下的指定文件部署到测试运行目录，这句也是必须的
        [DeploymentItem(@"DataDirectory\data.csv")]
        //数据文件需要设置为Copy to Output
        [TestMethod]
        [Priority(2)]
        [TestProperty("DataDriven", "PropertyValue")]
        public void CsvDataDrivenTest()
        {
            DoDataDrivenTest();
            Assert.AreEqual("PropertyValue", TestContext.Properties["DataDriven"]);

        }


        #endregion

        [TestCleanup]
        public void TestCleanup()
        {
            Console.WriteLine($"Test {TestContext.TestName} is Cleanup");
        }
        [ClassCleanup]
        public static void ClassCleanup()
        {
            Console.WriteLine("Class PubTest is Cleanup");
        }
    }
}
