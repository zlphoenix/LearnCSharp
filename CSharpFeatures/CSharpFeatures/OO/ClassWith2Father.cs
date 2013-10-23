using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.OO
{
    public abstract class Base : IBase
    {
        public abstract string Name { get; set; }
    }

    public interface IBase
    {
        string Name { get; set; }
    }

    public interface IBase1
    {
        string Name { get; set; }
    }
    public class ClassWith2Father : Base, IBase1
    {
        public override string Name { get; set; }
    }
}
