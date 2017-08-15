using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Inspur.Gsp.CSharpIntroduction.Demo.Reflection
{
    public class DynamicInvoke
    {
        public object Invoke()
        {
            var type = GetInvokeeType();

            var demoInstance = GetInstance(type);
            return type.GetMethod("Foo").Invoke(demoInstance, null);
            //return type.GetMethod("Test").Invoke(demoInstance,new object[] { "Hello world" });


        }

        /// <summary>
        /// 查找并执行 声明了MyAttribute特性的方法
        /// </summary>
        /// <returns></returns>
        public object InvokeMyMethod()
        {
            var type = GetInvokeeType();
            var demoInstance = GetInstance(type);

            var firstOrDefault = type.GetMethods()
                .FirstOrDefault(
                    method => method.GetCustomAttributes(false)
                        .OfType<Attribute>()
                        .Any(attrib => Equals(attrib.TypeId, "MyMethod")));

            if (firstOrDefault != null)
            {
                return firstOrDefault
                    .Invoke(demoInstance, null);
            }
            
            var ms = type.GetMethods();
            MethodInfo targetMethod = null;
            foreach (MethodInfo method in ms)
            {
                if (method.GetCustomAttributes(false).Length > 0)
                {
                    targetMethod = method;
                    break;
                }
            }
            if (targetMethod != null)
                targetMethod.Invoke(demoInstance, null);
      
            return null;
        }



        private static object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        private static Type GetInvokeeType()
        {
            var assembly = Assembly.Load("Inspur.Gsp.CSharpIntroduction.DemoLib");
            var type = assembly.GetType("Inspur.Gsp.CSharpIntroduction.DemoLib.Demo");
            return type;
        }
    }
}
