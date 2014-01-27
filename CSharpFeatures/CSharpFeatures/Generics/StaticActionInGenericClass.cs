using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.Generics
{
    public class StaticActionInGenericClass<T>
    {
        public static string TypeName;
        public static void DoWithGenericParam()
        {
            TypeName = typeof(T).Name;
        }
    }

    public class TestClass
    {
        public static string DoStr()
        {
            StaticActionInGenericClass<string>.DoWithGenericParam();
            return StaticActionInGenericClass<string>.TypeName;
        }
        public static void DoBool()
        {
            Trace.WriteLine(StaticActionInGenericClass<string>.TypeName);
            StaticActionInGenericClass<bool>.DoWithGenericParam();
            Trace.WriteLine(StaticActionInGenericClass<bool>.TypeName);
        }
    }
}
