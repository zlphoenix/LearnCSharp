using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Messaging;
using System.ServiceModel;
//using Model;

namespace proxy
{
    public class MyRealProxy<T> : RealProxy
    {
        //测试中文
        private T _target;
        public MyRealProxy(T target)
            : base(typeof(T))
        {
            this._target = target;
        }
        public override IMessage Invoke(IMessage msg)
        {
            //using (OperationContextScope contextScope = new OperationContextScope(this._target as IContextChannel))
            //{ 
            //    //

            ////添加请求 MessageHeader
            //MessageHeader<ApplicationContext> headerForRequest = new MessageHeader<ApplicationContext>(new ApplicationContext() { Uid = "tiger", Pwd = "456" });
            //OperationContext.Current.OutgoingMessageHeaders.Add(headerForRequest.GetUntypedHeader(ApplicationContext.LocalName, ApplicationContext.Namespace));


            IMethodCallMessage callMessage = (IMethodCallMessage)msg;
            object returnValue = callMessage.MethodBase.Invoke(this._target, callMessage.Args);

            return new ReturnMessage(returnValue, new object[0], 0, null, callMessage);
            //}
        }

    }
}
