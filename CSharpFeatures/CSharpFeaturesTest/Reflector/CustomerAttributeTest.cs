using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Reflector
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class CustomerNonInheritedAttribute : Attribute
    {
        public string Position { get; set; }
    }
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    internal class CustomerInheritedAttribute : Attribute
    {
        public string Position { get; set; }
    }

    internal interface IInterface
    {
        [CustomerInherited(Position = "Interface")]
        string Id { get; set; }
    }

    class Super
    {
        [CustomerInherited(Position = "Base")]
        public virtual string Misc { get; set; }
    }
    internal class Base : Super
    {
        /// <summary>
        /// 父类有而子类没有的属性,子类可以获取到它的Attrib,无论查询是是否追溯父类
        /// </summary>
        [CustomerNonInheritedAttribute(Position = "Base")]
        public string Code { get; set; }

        [CustomerInherited(Position = "Base")]
        [CustomerNonInheritedAttribute(Position = "Base")]
        public virtual string Description { get; set; }

        //[CustomerInherited(Position = "Base")]
        public override string Misc { get; set; }

        private IList<string> _context;
        public string this[int index]
        {
            get { return _context[index]; }
            set { _context[index] = value; }
        }
        public string this[string index]
        {
            get { return ""; }
            set { }
        }
        [CustomerNonInheritedAttribute(Position = "Base")]
        [CustomerInherited(Position = "Base")]
        public virtual string Property { get; set; }

    }

    internal class Derived : Base, IInterface
    {
        /// <summary>
        /// 子类的标签会覆盖父类的标签
        /// </summary>
        [CustomerNonInheritedAttribute(Position = "Derived")]
        public string Name { get; set; }

        /// <summary>
        /// 父类和子类都有的属性,以子类为准
        /// </summary>
        [CustomerNonInheritedAttribute(Position = "Derived")]
        [CustomerInherited(Position = "Derived")]
        public override string Description { get; set; }

        /// <summary>
        /// 子类不打标签,但标签类本身可以被继承,也会被子类查到,查询必须向父类追踪
        /// </summary>
        public override string Misc { get; set; }



        [CustomerInherited(Position = "Derived")]
        public override string Property { get; set; }



        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

    [TestClass]
    public class CustomerAttributeTest
    {
        [TestMethod]
        public void GetCustomerAttrFromDerivedClass()
        {

            var ps = (from p in typeof(Derived).GetProperties()
                      where p.GetCustomAttributes(typeof(CustomerNonInheritedAttribute), false)
                         .Any(o => o is CustomerNonInheritedAttribute)
                      select p).ToList();

            Assert.AreEqual(3, ps.Count());


            ps = (from p in typeof(Derived).GetProperties()
                  where p.GetCustomAttributes(typeof(CustomerInheritedAttribute), false)
                     .Any(o => o is CustomerInheritedAttribute)
                  select p).ToList();

            //不包含没有
            Assert.IsFalse(ps.Any(p => p.Name == "Misc"));

            //
            Assert.AreEqual(1, typeof(Derived).GetProperty("Misc").GetCustomAttributes(true).Count());
            Assert.AreEqual(1, System.Attribute.GetCustomAttributes(typeof(Derived).GetProperty("Misc")).Count());
            var firstOrDefault = typeof(Derived).GetProperties().FirstOrDefault(p => p.Name == "Item");
            if (firstOrDefault != null)
            {
                var attr = firstOrDefault.GetCustomAttributes(true);
            }

            //Assert.AreEqual(1, System.Attribute.GetCustomAttributes(typeof(Derived).GetProperty("Id")).Count());
        }
    }
}
