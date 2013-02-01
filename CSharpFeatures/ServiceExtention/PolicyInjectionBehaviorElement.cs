using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace ServiceExtention
{
    public class PolicyInjectionBehaviorElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("policyInjectorName", IsRequired = false, DefaultValue = "")]
        public string PolicyInjectorName
        {
            get
            {
                return this["policyInjectorName"] as string;
            }
            set
            {
                this["policyInjectorName"] = value;
            }
        }

        public override Type BehaviorType
        {
            get { return typeof(PolicyInjectionBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new PolicyInjectionBehavior(this.PolicyInjectorName);
        }
    }
}
