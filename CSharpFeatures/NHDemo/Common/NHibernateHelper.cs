using NHibernate;
using NHibernate.Cfg;
using System.Runtime.Remoting.Messaging;
using System.Collections.Generic;

namespace NHDemo.Common
{
    public sealed class NHibernateHelper
    {
        private const string CurrentSessionKey = "nhibernate.current_session";
        private static readonly ISessionFactory SessionFactory;

        static NHibernateHelper()
        {
            SessionFactory = new Configuration().Configure()
                //.SetNamingStrategy()
                .BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {

            var currentSession = CallContext.GetData(CurrentSessionKey) as ISession;

            //如果已经被释放了,应该将Session清理掉
            if (currentSession != null && !currentSession.IsOpen)
            {
                CallContext.FreeNamedDataSlot(CurrentSessionKey);
                currentSession = null;
            }
            if (currentSession == null)
            {
                currentSession = SessionFactory.OpenSession();

                CallContext.SetData(CurrentSessionKey, currentSession);
            }

            return currentSession;
        }

        public static void CloseSession()
        {
            //HttpContext context = HttpContext.Current;
            var currentSession = CallContext.GetData(CurrentSessionKey) as ISession;

            if (currentSession == null)
            {
                // No current session
                return;
            }

            currentSession.Close();
            CallContext.FreeNamedDataSlot(CurrentSessionKey);
            //context.Items.Remove(CurrentSessionKey);
        }

        public static void CloseSessionFactory()
        {
            if (SessionFactory != null)
            {
                SessionFactory.Close();
            }
        }
    }
}