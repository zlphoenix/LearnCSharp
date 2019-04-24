using System;
using NHDemo.Common;
using NHDemo.Entities;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;
namespace NHDemo.DataAccess
{
    public class CatsBP
    {
        public void CreateSchema()
        {
            var nhConfig = new ConfigurationBuilder().Build();
            var schemaExport = new SchemaExport(nhConfig);
            schemaExport
                .SetOutputFile(@"db.sql")
                .Execute(false, false, false);
        }

        public void NewCat()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            var princess = new Cat
            {
                Name = "Princess",
                Sex = 'F',
                Weight = 7.4f,
                BirthDate = DateTime.Now,
                Color = "Black"
            };
            session.Save(princess);
            tx.Commit();
            NHibernateHelper.CloseSession();
        }
        public void GetCat()
        {
            var session = NHibernateHelper.GetCurrentSession();
            using (var tx = session.BeginTransaction())
            {
                var query = session.CreateQuery("select c from Cat as c where c.Sex = :sex");
                query.SetCharacter("sex", 'F');
                foreach (Cat cat in query.Enumerable())
                {
                    Console.WriteLine("Female Cat: " + cat.Name);
                }
                var criteria = session.QueryOver<Cat>();
                //条件测试
                criteria
                    .Where(c => c.Sex == 'F');
                var result = criteria.List();
                foreach (var cat in result)
                {
                    Console.WriteLine("Female Cat: " + cat.Name);
                }

                //投影测试
                criteria.Select(c => c.Name, c => c.Id);
                var scalarResult = criteria.OrderBy(c => c.Id).Asc
                     .List<Object[]>();
                foreach (var cat in scalarResult)
                {
                    Console.WriteLine(string.Format("Female Cat:{0},ID:{1}", cat[0], cat[1]));
                }
                //复合条件
                criteria = session.QueryOver<Cat>();
                criteria.Where(c => c.Sex == 'F' && c.Id > 0);
                result = criteria.List();
                //criteria.SetProjection()
                tx.Commit();
            }
        }
    }
}
