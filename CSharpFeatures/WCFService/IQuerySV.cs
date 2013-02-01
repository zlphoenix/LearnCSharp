using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml.Linq;
//using ExpressionSerialization;

namespace WCFService
{
    [ServiceContract]
    public interface IQuerySV
    {
        [OperationContract]
        void Do(XElement param);
    }

    public class QuerySV : IQuerySV
    {
        public void Do(XElement xml)
        {
            var list = new List<string>().AsQueryable();
            //list.DeserializeQuery(xml);
        }
    }
}
