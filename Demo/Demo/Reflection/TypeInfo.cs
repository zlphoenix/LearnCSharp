using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Inspur.Gsp.CSharpIntroduction.Demo.OO;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Reflection
{
    public class TypeInfo
    {
        public void GetTypeInfo()
        {
            //获取类型
            var type = typeof(Animal);
            //获取成员
            var members = type.GetMembers();
            var methods = members.OfType<MethodInfo>();
            foreach (var methodInfo in methods)
            {
                Console.WriteLine(methodInfo.Name);
            }
        }
    }
}
