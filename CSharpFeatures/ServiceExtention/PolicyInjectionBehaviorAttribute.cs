using System;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace ServiceExtention
{
    public class PolicyInjectionBehaviorAttribute : Attribute, IContractBehavior
    {
        public string PolicyInjectorName { get; set; }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint,
                                        ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint,
                                          DispatchRuntime dispatchRuntime)
        {
            Type serviceContractType = contractDescription.ContractType;
            dispatchRuntime.InstanceProvider = new PolicyInjectionInstanceProvider(serviceContractType,
                                                                                   this.PolicyInjectorName);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}