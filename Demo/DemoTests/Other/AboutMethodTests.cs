using Inspur.Gsp.CSharpIntroduction.Demo.Other;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoTests.Other
{
    [TestClass()]
    public class AboutMethodTests
    {
        [TestMethod()]
        public void RefParamTest()
        {
            var invoker = new AboutMethod();
            var str = string.Empty;
            invoker.RefParam(ref str);
        }

        [TestMethod()]
        public void VariableParamsTest()
        {
            var invoker = new AboutMethod();
            invoker.VariableParams("", "", "");
            invoker.VariableParams("", "");
        }

        [TestMethod()]
        public void ParamDefaultValueTest()
        {
            var invoker = new AboutMethod();
            invoker.ParamDefaultValue();
            invoker.ParamDefaultValue("");
        }
    }
}