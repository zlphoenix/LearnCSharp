using System.Collections.Generic;
using NHDemo.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NHDemo.Common;
using NHDemo.Entities;
using NHibernate.Criterion;
using NHibernate.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Threading;
using TelChina.AF.Util.TestUtil;
using NHibernate.Cfg;

using TelChina.AF.Util.Logging;

namespace NHDemoTest
{


    /// <summary>
    ///This is a test class for CatsBPTest and is intended
    ///to contain all CatsBPTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CatsBPTest
    {
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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ILogger logger = LogManager.GetLogger(typeof(CatsBPTest).FullName);

        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            var session = NHibernateHelper.GetCurrentSession();
            var cats = session.CreateCriteria<Cat>().List<Cat>();
            foreach (var cat in cats)
            {
                session.Delete(cat);
            }
            session.Flush();
        }

        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //

        #endregion


        /// <summary>
        ///A test for NewCat
        ///</summary>
        [TestMethod()]
        public void NewCatTest()
        {
            var target = new CatsBP(); // TODO: Initialize to an appropriate value
            target.NewCat();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod]
        public void CreateCatWithChildrenTest()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            var mam = new Cat
                          {
                              Name = "MamCat",
                              Sex = 'F',
                              Weight = 7.4f,
                              BirthDate = DateTime.Now,
                              Color = "Black"
                          };
            session.Save(mam);
            for (int i = 0; i < 5; i++)
            {
                var name = "Cat_" + i.ToString();
                var kitt = CreateKitt(mam, name);
                session.Save(kitt);
            }

            session.Save(mam);
            tx.Commit();

            var query = session.QueryOver<Cat>();
            query.Where(c => c.Name == "MamCat");
            var result = query.List().FirstOrDefault();
            Assert.IsTrue(result != null && result.Kittens.Count > 0);
            NHibernateHelper.CloseSession();
        }

        [TestMethod]
        public void TwoThreadCreate()
        {
            Parallel.For(0, 2, (int x) => CreateCatWithChildrenTest());
        }

        private static Cat CreateKitt(Cat mam, string name)
        {
            var kitt = new Cat
                           {
                               Name = name,
                               Sex = 'F',
                               Weight = 7.4f,
                               BirthDate = DateTime.Now,
                               Color = "Black"
                           };
            mam.Kittens.Add(kitt);
            return kitt;
        }

        [TestMethod]
        public void InnerTransTest()
        {
            TransInvoke.InvokTransFunction(() =>
                                               {
                                                   ISession session = NHibernateHelper.GetCurrentSession();
                                                   //Transaction.Current.TransactionCompleted +=
                                                   //    (object sender, TransactionEventArgs e) => NHibernateHelper.CloseSession();
                                                   using (ITransaction tx = session.BeginTransaction())
                                                   {
                                                       //Do sth...
                                                       var entity = new Cat
                                                                        {
                                                                            Name = "Princess",
                                                                            Sex = 'F',
                                                                            Weight = 7.4f,
                                                                            BirthDate = DateTime.Now,
                                                                            Color = "Black"
                                                                        };
                                                       session.Save(entity);
                                                       session.Delete(entity);
                                                       //session.Evict();
                                                       tx.Commit();

                                                       //throw new Exception("");
                                                   }
                                                   NHibernateHelper.CloseSession();
                                                   return 0;
                                               });
        }

        [TestMethod]
        public void SessionDisposeTransactionsTest()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var cats = new List<Cat>();

            TransInvoke.InvokTransFunction(() =>
                                               {
                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          var entity = new Cat
                                                                                                           {
                                                                                                               Name =
                                                                                                                   "Princess1",
                                                                                                               Sex = 'F',
                                                                                                               Weight =
                                                                                                                   7.4f,
                                                                                                               BirthDate
                                                                                                                   =
                                                                                                                   DateTime
                                                                                                                   .Now,
                                                                                                               Color =
                                                                                                                   "Black"
                                                                                                           };
                                                                                          session.Save(entity);
                                                                                          session.Dispose();
                                                                                          //cats.Add(entity);
                                                                                          return 0;
                                                                                      });
                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          var entity = new Cat
                                                                                                           {
                                                                                                               Name =
                                                                                                                   "Princess2",
                                                                                                               Sex = 'F',
                                                                                                               Weight =
                                                                                                                   7.4f,
                                                                                                               BirthDate
                                                                                                                   =
                                                                                                                   DateTime
                                                                                                                   .Now,
                                                                                                               Color =
                                                                                                                   "Black"
                                                                                                           };
                                                                                          //cats.Add(entity);
                                                                                          session.Save(entity);
                                                                                          session.Dispose();
                                                                                          return 0;
                                                                                      });
                                                   //foreach (var cat in cats)
                                                   //{
                                                   //    session.Save(cat);
                                                   //}
                                                   //session.Flush();
                                                   return 0;
                                               }, true, true, null, TransactionScopeOption.Required);
            session.Dispose();
        }

        [TestMethod]
        public void SaveWithoutTransTest()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            CreateNewEntityWithoutTrans(session);
        }

        [TestMethod]
        public void SessionWithComplexTransactionsTest()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            var cats = new List<Cat>();

            TransInvoke.InvokTransFunction(() =>
                                               {

                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          var entity = new Cat
                                                                                                           {
                                                                                                               Name =
                                                                                                                   "Princess1",
                                                                                                               Sex = 'F',
                                                                                                               Weight =
                                                                                                                   7.4f,
                                                                                                               BirthDate
                                                                                                                   =
                                                                                                                   DateTime
                                                                                                                   .Now,
                                                                                                               Color =
                                                                                                                   "Black"
                                                                                                           };
                                                                                          session.Save(entity);
                                                                                          //cats.Add(entity);
                                                                                          return 0;
                                                                                      });
                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          var entity = new Cat
                                                                                                           {
                                                                                                               Name =
                                                                                                                   "Princess2",
                                                                                                               Sex = 'F',
                                                                                                               Weight =
                                                                                                                   7.4f,
                                                                                                               BirthDate
                                                                                                                   =
                                                                                                                   DateTime
                                                                                                                   .Now,
                                                                                                               Color =
                                                                                                                   "Black"
                                                                                                           };
                                                                                          //cats.Add(entity);
                                                                                          session.Save(entity);
                                                                                          return 0;
                                                                                      });
                                                   foreach (var cat in cats)
                                                   {
                                                       session.Save(cat);
                                                   }
                                                   session.Flush();
                                                   return 0;
                                               }
                                           , true, true, null, TransactionScopeOption.Required);
        }

        [TestMethod]
        public void SessionWithComplexTransactionsTest2()
        {
            var SessionFactory = new Configuration().Configure()
                //.SetNamingStrategy()
                .BuildSessionFactory();

            ISession session = SessionFactory.OpenSession();

            CreateNewEntity(session);
            //cats.Add(entity);
            CreateNewEntity(session);

            session.Flush();
        }

        private static void CreateNewEntity(ISession session)
        {
            var entity = new Cat
                             {
                                 Name = "Princess1",
                                 Sex = 'F',
                                 Weight = 7.4f,
                                 BirthDate = DateTime.Now,
                                 Color = "Black"
                             };
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(1, 1, 1))
                )
            {
                session.Save(entity);
                scope.Complete();
            }
        }

        private static void CreateNewEntityWithoutTrans(ISession session)
        {
            var entity = new Cat
                             {
                                 Name = "Princess1",
                                 Sex = 'F',
                                 Weight = 7.4f,
                                 BirthDate = DateTime.Now,
                                 Color = "Black"
                             };
            session.Save(entity);
        }

        [TestMethod]
        public void SessionOverTransTest()
        {
            TransInvoke.InvokTransFunction(() =>
                                               {
                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          NHCreateEntity();
                                                                                          return 0;
                                                                                      });
                                                   TransInvoke.InvokTransFunction(() =>
                                                                                      {
                                                                                          NHCreateEntity();
                                                                                          return 0;
                                                                                      });
                                                   return 0;
                                               }, false);
        }

        [TestMethod]
        public void CreateSessionBeforeTransTest()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            TransInvoke.InvokTransFunction(() =>
                                               {
                                                   var princess = new Cat
                                                                      {
                                                                          Name = "Princess",
                                                                          Sex = 'F',
                                                                          Weight = 7.4f,
                                                                          BirthDate = DateTime.Now,
                                                                          Color = "Black"
                                                                      };
                                                   session.Save(princess);
                                                   return 0;
                                               }, false);
            session.Flush();
        }

        private void NHCreateEntity()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            //session.Transaction.
            Console.WriteLine("ITransaction创建前");
            DealTransaction();
            //ITransaction tx = session.BeginTransaction();
            var princess = new Cat
                               {
                                   Name = "Princess",
                                   Sex = 'F',
                                   Weight = 7.4f,
                                   BirthDate = DateTime.Now,
                                   Color = "Black"
                               };
            session.Save(princess);
            Console.WriteLine("ITransaction提交前");
            DealTransaction();
            //tx.Commit();
            Console.WriteLine("Session关闭前");
            DealTransaction();
            //NHibernateHelper.CloseSession();
            Console.WriteLine("TScop提交前");
            DealTransaction();
        }

        private void LogError(Exception exception)
        {
            if (exception.InnerException != null)
            {
                LogError(exception.InnerException);
            }
            var errorMessage = string.Format("[{0}],系统执行过程中发生异常:异常类型{1},异常信息:{2},异常堆栈{3}",
                                             DateTime.Now.ToShortTimeString(),
                                             exception.GetType().ToString(),
                                             exception.Message,
                                             exception.StackTrace
                );
            Console.WriteLine(errorMessage);
        }

        protected void DealTransaction()
        {
            if (Transaction.Current != null)
            {
                Console.Write("[事务处理模式]:");
                LogTransactionInfo(Transaction.Current);
                //if (Transaction.Current.TransactionCompleted != null)
            }
            else
            {
                Console.Write("[当前处于无事务模式]:");
            }
        }

        private static void LogTransactionInfo(Transaction trans)
        {
            Console.WriteLine(string.Format("事务属性:\t事务状态:{0}\t创建时间{1}\t分布式标识{2}\t本地标识{3}",
                                            trans.TransactionInformation.Status,
                                            trans.TransactionInformation.CreationTime,
                                            trans.TransactionInformation.DistributedIdentifier,
                                            trans.TransactionInformation.LocalIdentifier
                                  ));

        }

        /// <summary>
        ///A test for GetCat
        ///</summary>
        [TestMethod()]
        public void GetCatTest()
        {
            var target = new CatsBP(); // TODO: Initialize to an appropriate value
            target.GetCat();
            //Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        [TestMethod()]
        public void QueryTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                var cats = session.QueryOver<Cat>();
                cats.WhereRestrictionOn(c => c.Name).IsLike("xxx", MatchMode.Anywhere);
                //cats.Where(c => c.Name.Contains("xxx"));//这种方式不支持
            }
        }

        [TestMethod()]
        public void QueryProjectionTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                var cats = session.QueryOver<Cat>();
                var querable = session.Query<Cat>();
                cats.Select(c => c.Id, c => c.Name);
                var result = cats.List<object[]>();



                foreach (var record in result)
                {
                    foreach (var field in record)
                    {
                        Console.Write(field + "/t");
                    }
                    Console.WriteLine();
                }

                var t = from c in result
                        select new { ID = c[0], Name = c[1] };
                foreach (var r in t)
                {
                    Console.WriteLine(r.ID + "\t" + r.Name);
                }
                Assert.AreNotEqual(result.Count, 0);
                tx.Commit();
            }
        }

        //[TestMethod()]
        public void QueryProjectionTest2()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                var cats = session.QueryOver<Cat>();

                cats.Select(c => c.BirthDate.YearPart()); //投影列使用C#方法
                var result = cats.List<string>();
                foreach (var record in result)
                {
                    foreach (var field in record)
                    {
                        Console.Write(field + "/t");
                    }
                    Console.WriteLine();
                }

                cats.Select(c => c.Name.Upper()); //投影列使用C#方法,必须使用NH提供的扩展方法？？！！

                result = cats.List<string>();
                foreach (var record in result)
                {
                    foreach (var field in record)
                    {
                        Console.Write(field + "/t");
                    }
                    Console.WriteLine();
                }
                Assert.AreNotEqual(result.Count, 0);
                tx.Commit();
            }
        }

        [TestMethod()]
        public void HQLTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                //Cat s;
                var cats = session.CreateQuery("select c from Cat as c where c.Sex = :sex");
                cats.SetCharacter("sex", 'F');
                var result = cats.List<Cat>();
                foreach (var cat in result)
                {
                    Console.Write(cat.Id + "/t");
                }
            }
        }

        /// <summary>
        /// 注入方言测试
        /// </summary>
        [TestMethod()]
        public void HQLDialectTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                //Cat s;
                //MyHour 方法是在方言中自定义的方法
                var cats = session.CreateQuery("select c from Cat as c where c.Sex = :sex and MyHour(c.BirthDate)>0");
                cats.SetCharacter("sex", 'F');
                var result = cats.List<Cat>();
                foreach (var cat in result)
                {
                    Console.Write(cat.Id + "/t");
                }
            }
        }

        [TestMethod]
        public void CreateDataBase()
        {
            var target = new CatsBP();
            target.CreateSchema();
        }

        [TestMethod]
        public void LinqTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                var result = from c in session.Query<Cat>()
                             where c.Id > 0
                             select c;
                var count = result.Count();
                var rows = result.Take(10).Skip(20).ToList();

                Console.WriteLine(count);
                //session.CreateQuery("")
            }
        }

        [TestMethod]
        public void PagingTest()
        {
            var bp = new CatsBP();
            for (var i = 0; i < 10; i++)
            {
                bp.NewCat();
            }
            var session = NHibernateHelper.GetCurrentSession();
            var q = session.CreateQuery("select c from Cat as c order by c.Id");
            q.SetMaxResults(6);
            q.SetFirstResult(5);
            var result = q.List<Cat>();
            Assert.IsNotNull(result, "没有查询出结果");
            Assert.AreEqual(5, result.Count, string.Format("分页返回记录数不正确,期望值为{0},实际值为{1}", 5, result.Count));


            result = (from c in session.Query<Cat>()
                      orderby c.Id
                      select c).Skip(5).Take(6).ToList();

            Assert.IsNotNull(result, "没有查询出结果");
            Assert.AreEqual(5, result.Count);

        }
    }
}
