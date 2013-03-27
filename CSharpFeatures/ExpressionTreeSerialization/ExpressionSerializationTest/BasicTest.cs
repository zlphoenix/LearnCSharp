using System;
using System.Linq.Expressions;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionSerialization;
using System.Xml.Linq;
using TestHarness;

namespace ExpressionSerializationTest
{
    [TestClass]
    public class BasicTest
    {
        [TestMethod]
        public void PrimitiveTypeTest()
        {
            Expression<Func<int>> addExpr = () => 1 + 1;
            var serializer = new ExpressionSerializer();
            XElement addXml = serializer.Serialize(addExpr);
            Expression<Func<int>> addExpResult = serializer.Deserialize<Func<int>>(addXml);
            Func<int> addExpResultFunc = addExpResult.Compile();
            Assert.AreEqual(2, addExpResultFunc());  // evaluates to 2
        }


        [TestMethod]
        public void ComplexTypeTest()
        {
            Expression<Func<Customer, bool>> addExpr = (customer) => string.IsNullOrEmpty(customer.CustomerID);
            var serializer = new ExpressionSerializer();
            XElement addXml = serializer.Serialize(addExpr);
            var addExpResult = serializer.Deserialize<Func<Customer, bool>>(addXml);
            Func<Customer, bool> addExpResultFunc = addExpResult.Compile();
            Assert.AreEqual(true, addExpResultFunc(new Customer() { CustomerID = "Allen" }));  // evaluates to 2
        }
    }
}
