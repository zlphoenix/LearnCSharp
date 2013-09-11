using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Reflector
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class CustomerAttribute : Attribute
    {
        public string Position { get; set; }
    }

    internal class Base
    {
        [CustomerAttribute(Position = "Base")]
        public string Code { get; set; }

        [CustomerAttribute(Position = "Base")]
        public virtual string Description { get; set; }

    }

    internal class Derived : Base
    {
        [CustomerAttribute(Position = "Derived")]
        public string Name { get; set; }

        [CustomerAttribute(Position = "Derived")]
        public override string Description { get; set; }
    }

    [TestClass]
    public class CustomerAttributeTest
    {
        [TestMethod]
        public void GetCustomerAttrFromDerivedClass()
        {
            var ps = (from p in typeof(Derived).GetProperties()
                      where p.GetCustomAttributes(typeof(CustomerAttribute), true)
                         .Select(o => o as CustomerAttribute)
                         .Any(ca => ca.Position == "Derived")
                      select p).ToList();
            Assert.AreEqual(2, ps.Count(), "父类中的Attrib没有取到");
           
        }
    }
}
