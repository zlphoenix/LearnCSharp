using System;

namespace Inspur.Gsp.CSharpIntroduction.DemoLib
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class MyAttribute : Attribute
    {
        public override object TypeId
        {
            get { return "MyMethod"; }
        }
    }
}
