using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Allen.Util.Logging;
using System.Runtime.Serialization;
using System.ServiceModel.Activation;

namespace WCFService.OuterLab
{
    #region Biz


    [DataContract(Name = "EntityBase", Namespace = @"http://schemas.telchina.net/AF/2012/WCFService.OuterLab")]
    [XObj(TypeAssembly = "WCFService", TypeFullName = "WCFService.OuterLab.DTOBase")]
    public class EntityBase
    {
        [DataMember]
        public string Code { get; set; }
    }

    [DataContract(Name = "BusinessEntity", Namespace = @"http://schemas.telchina.net/AF/2012/WCFService.OuterLab")]
    [XObj(TypeAssembly = "WCFService", TypeFullName = "WCFService.OuterLab.BizDTO")]
    public class BusinessEntity : EntityBase
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }

    [DataContract]
    [XObj(TypeAssembly = "WCFService", TypeFullName = "WCFService.OuterLab.BizDTO")]
    public class DTOBase
    {
        [DataMember]
        public string Code { get; set; }
    }
    [DataContract]
    [XObj(TypeAssembly = "WCFService", TypeFullName = "WCFService.OuterLab.BizDTO")]
    public class BizDTO : DTOBase
    {
        [DataMember]
        public string Name
        {
            get;
            set;
        }
    }
    #endregion

    #region Service

    /// <summary>
    /// 操作契约使用泛型参数
    /// </summary>
    [ServiceContract]
    public interface ICommonCRUDService
    {
        [OperationContract]
        void Add(object entity);
    }

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CommonCRUDService
    : ICommonCRUDService
    {
        private readonly ILogger _logger =
            LogManager.GetLogger(typeof(CommonCRUDService).FullName);
        public void Add(object entity)
        {

            _logger.Debug(string.Format("参数类型:{0},参数值:{1}",
                entity.GetType().FullName, entity.ToString()));
        }
    }

    #endregion

    #region infrastracture
    [AttributeUsageAttribute(AttributeTargets.Class)]
    public class XObjAttribute : Attribute
    {
        public string TypeFullName { get; set; }

        public string TypeAssembly { get; set; }
    }


    #endregion
}
