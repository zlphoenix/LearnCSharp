using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Collections.Specialized;

namespace ServiceExtention.CustomRule
{
    [ConfigurationElementType(typeof(CustomMatchingRuleData))]
    public class ServiceExtentionMatchRule : IMatchingRule
    {
        NameValueCollection attributes = null;
        public ServiceExtentionMatchRule(NameValueCollection attributes)
        {
            this.attributes = attributes;
        }

        public bool Matches(System.Reflection.MethodBase member)
        {
            bool result = false;
            if (this.attributes["matchname"] != null)
            {
                if (this.attributes["matchname"] == member.Name)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}
