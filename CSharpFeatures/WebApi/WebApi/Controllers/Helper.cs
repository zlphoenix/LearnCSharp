using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace WebApi.Controllers
{
    public static class Helper
    {
        public static string Display<T>(this IEnumerable<T> collection, Func<T, string> itemToString = null)
        {

            if (collection == null) return string.Empty;
            if (itemToString == null)
                itemToString = (item) => item == null ? string.Empty : item.ToString();

            return collection.Aggregate(new StringBuilder(), (sb, item) => sb.Append($",{itemToString(item)}")).ToString();
        }

        public static string GetFuncSessionId(this HttpRequestMessage req)
        {
            IEnumerable<string> session;
            req.Headers.TryGetValues("FucSessionId", out session);
            if (session != null)
                return session.FirstOrDefault();
            else
                return string.Empty;
        }
    }
}