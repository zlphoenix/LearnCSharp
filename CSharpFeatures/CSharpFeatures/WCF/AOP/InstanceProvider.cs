using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;

namespace CSharpFeatures.WCF.AOP
{
    public class PolicyInjectionInstanceProvider : IInstanceProvider
    {
        private Type _serviceContractType;
        private string _policyInjectorName;

        public PolicyInjectionInstanceProvider(Type serviceContractType, string policyInjectorName)
        {
            this._serviceContractType = serviceContractType;
            this._policyInjectorName = policyInjectorName;
        }
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            PolicyInjector policyInjector = null;
            if (string.IsNullOrEmpty(this._policyInjectorName))
            {
                policyInjector = new PolicyInjectorFactory().Create();
            }
            else
            {
                policyInjector = new PolicyInjectorFactory().Create(this._policyInjectorName);
            }

            Type serviceType = instanceContext.Host.Description.ServiceType;
            object serviceInstance = Activator.CreateInstance(serviceType);
            if (!this._serviceContractType.IsInterface && !serviceType.IsMarshalByRef && policyInjector is RemotingPolicyInjector)
            {
                return serviceInstance;
            }

            return policyInjector.Wrap(serviceInstance, this._serviceContractType);
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return this.GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }

}
