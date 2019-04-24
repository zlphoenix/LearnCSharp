using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHDemo.Common;
using NHDemo.Entities;
using System.Transactions;
using NHibernate;
using TelChina.AF.Util.TestUtil;
using NHibernate.Linq;
using System.Linq;
using TelChina.AF.Util.Logging;
using System;

namespace NHDemoTest
{
    [TestClass]
    public class RelationTest
    {
        ILogger _logger = LogManager.GetLogger(typeof(RelationTest).FullName);

        [TestInitialize()]
        public void MyTestInitialize()
        {
            TransInvoke.InvokTransFunction(() =>
                {
                    using (var session = NHibernateHelper.GetCurrentSession())
                    {
                        var query = session.QueryOver<Question>();
                        var qs = query.List();
                        foreach (var question in qs)
                        {
                            if (question.Answers.Count > 0)
                            {
                                foreach (var answer in question.Answers)
                                {
                                    session.Delete(answer);
                                }
                            }
                            session.Delete(question);
                        }

                        var aQuery = session.QueryOver<Answer>().List();
                        if (aQuery != null)
                        {
                            foreach (var answer in aQuery)
                            {
                                session.Delete(answer);
                            }
                        }
                        session.Flush();
                    }
                    return 0;
                });
            _logger.Debug("数据清理完成");
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        [TestMethod]
        public void CreateMainEntityTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            var expected = TransInvoke.InvokTransFunction(() =>
            {
                var q = new Question();
                q.Name = "Q1";
                session.Save(q);
                session.Flush();
                return q;
            });
            var session2 = NHibernateHelper.GetCurrentSession();
            var result = session2.Get<Question>(expected.QuestionID);
            Assert.AreEqual(result, expected);
            NHibernateHelper.CloseSession();
        }
        [TestMethod]
        public void CreateMainEntityWithChildrenTest()
        {
            var session = NHibernateHelper.GetCurrentSession();
            var expected = TransInvoke.InvokTransFunction(() =>
            {
                var q = CreateQAndA(session);

                session.Flush();
                return q;
            });
            var session2 = NHibernateHelper.GetCurrentSession();
            var result = session2.Get<Question>(expected.QuestionID);

            var query = (from item in session2.Query<Answer>()
                         where item.Question.QuestionID == expected.QuestionID
                         select item);

            Assert.AreEqual(query.Count(), 2, "子实体提交失败");
            Assert.AreEqual(result, expected, "主实体提交失败");
            Assert.AreEqual(result.Answers.Count, 2, "子实体提交失败");
            NHibernateHelper.CloseSession();
        }

        private static Question CreateQAndA(ISession session)
        {
            var q = new Question();
            q.Name = "Q1";
            q.Answers.Add(new Answer() { Name = "A1", Question = q });
            q.Answers.Add(new Answer() { Name = "A2", Question = q });

            session.Save(q);
            foreach (var answer in q.Answers)
            {
                session.Save(answer);
            }
            return q;
        }

        [TestMethod]
        public void CreateEntityWithRelation()
        {
            var session = NHibernateHelper.GetCurrentSession();
            //var expected = TransInvoke.InvokTransFunction(() =>
            //    {
            var question = CreateQAndA(session);
            session.Flush();

            //foreach (var answer in question.Answers)
            //{
            //    session.Delete(answer);
            //}
            //question.Answers.Clear();
            //question.QuestionID = Guid.NewGuid();
            //session.Update(question);
            //session.Flush();
            session.Delete(question);
            session.Flush();
            
            //return 0;
            //});
        }
    }
}
