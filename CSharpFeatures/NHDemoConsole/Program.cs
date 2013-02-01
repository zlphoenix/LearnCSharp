using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using System.Transactions;
using NHDemo.Entities;

namespace NHDemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.ReadLine();
                var obj = new Program();
                obj.SessionWithComplexTransactionsTest2();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("Message:{0},Trace:{1},Type", e.Message, e.StackTrace, e.GetType().FullName));
            }
        }
        public void SessionWithComplexTransactionsTest2()
        {
            var SessionFactory = new Configuration().Configure()
                //.SetNamingStrategy()
                   .BuildSessionFactory();

            ISession session = SessionFactory.OpenSession();

            CreateNewEntity(session);
            //cats.Add(entity);
            CreateNewEntity(session);
            Console.WriteLine("Succeed!");
            session.Flush();
        }

        private static void CreateNewEntity(ISession session)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TimeSpan(1, 1, 1)))
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
                scope.Complete();
            }
        }
    }
}
