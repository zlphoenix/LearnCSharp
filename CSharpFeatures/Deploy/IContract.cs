using System.Collections.Generic;
//using System.Linq;
using System.Text;
//using System.ServiceModel;

namespace Deploy
{
    //[ServiceContract]
    public interface IContract
    {
        //[OperationContract]
        object Do();

        ParamDTO Param { get; set; }
    }
}
