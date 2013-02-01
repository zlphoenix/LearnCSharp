using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFHost
{
      [DataContract]
    public class EntityBase
    {
        [DataMember]
        public string Code { get; set; }
    }

    [DataContract]
    public class BusinessEntity : EntityBase
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }

    }
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICRUDService" in both code and config file together.
    [ServiceContract]
    public interface ICRUDService
    {
        [OperationContract]
        void Add(EntityBase entity);
    }
}
