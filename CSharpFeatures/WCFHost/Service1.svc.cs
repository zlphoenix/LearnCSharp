using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TelChina.AF.Util.Logging;


namespace WCFHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class Service1 : IService1
    {
        private static ILogger logger = LogManager.GetLogger("Service1");
        public  Service1()
        {
            logger.Debug("Create a new SV Impl instance");
        }
        private int _count;
        private static int TotalCount;
        public void DoWork()
        {
            logger.Debug(string.Format("Service instance is Called {0} times,total {1} times !", _count++, TotalCount++));
        }
    }
}
