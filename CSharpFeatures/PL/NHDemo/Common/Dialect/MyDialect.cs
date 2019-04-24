using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate;

namespace NHDemo.Common.Dialect
{
    public class MySQLLocaDialect : MsSql2008Dialect
    {
        public MySQLLocaDialect()
            : base()
        {
            RegisterFunction("MyHour", new SQLFunctionTemplate(NHibernateUtil.String, "datepart(hour, ?1)"));
        }
    }
}
