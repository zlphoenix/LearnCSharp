using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpFeatures.Roslyn
{
    public class RoslynDemo
    {
        public async Task HelloWorld()
        {
            //var options =
            //    ScriptOptions.Default
            //   .AddReferences("System.Runtime, Version=4.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            //options.AddReferences(
            //    "System.Reflection.Metadata, Version=1.3.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

            await CSharpScript.RunAsync("System.Console.WriteLine(\"hello world\");");
        }

        public async Task<int> GetExpressValue()
        {

            var result = await CSharpScript.EvaluateAsync<int>("3+2*5");
            return result;
        }

        public int ExceuteMultiLine()
        {
            var s0 = CSharpScript.Create("int x = 1;");
            var s1 = s0.ContinueWith("int y = 2;");
            var s2 = s1.ContinueWith<int>("x + y");
            return s2.RunAsync().Result.ReturnValue;
        }
        public bool UseGlobalObj()
        {
            var bar = new Bar() { StaffId = 5686, UnitId = 2, Age = 15 };
            return CSharpScript.RunAsync<bool>(
                "OutputId();" +
                "return (StaffId==5686 && UnitId==2)||( UnitId == 3|| Age >10);",
                ScriptOptions.Default, bar).Result.ReturnValue;
        }
    }

    public class Bar
    {
        public int StaffId { get; set; }
        public int UnitId { get; set; }
        public int Age { get; set; }

        public void OutputId()
        {
            Console.WriteLine(StaffId);
        }
    }
}
