using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using TelChina.AF.Util.Logging;

namespace WCFHost
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRUDService" in code, svc and config file together.
    public class CRUDService : ICRUDService
    {

        private readonly ILogger _logger = LogManager.GetLogger(typeof(CRUDService).FullName);


        public void Add(EntityBase entity)
        {
            _logger.Debug(string.Format("参数类型:{0},参数值:{1}",
              entity.GetType().FullName, entity.ToString()));
        }
    }
}
