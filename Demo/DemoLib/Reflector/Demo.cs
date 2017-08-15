using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inspur.Gsp.CSharpIntroduction.DemoLib
{

    public class Demo
    {
        public string Foo()
        {
            return "Hello";
        }
        [My]
        public string Bar()
        {
            return "World";
        }
        public void Test(string param)
        {

        }

    }

    [TableAttribte]
    public class Cat
    {
        [ColumnAttribute]
        public string Name { get; set; }

        [ColumnAttribute]
        public int Age { get; set; }

        [ColumnAttribute(ColumnName = "W")]
        public int Weight { get; set; }
    }


    class TableAttribte : Attribute
    {

    }

    class ColumnAttribute : Attribute
    {
        public string ColumnName { get; set; }
    }
}
