using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionSerialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Linq;

namespace CSharpFeaturesTest.Lambda
{
    [TestClass]
    public class ExpressionSerializationTest
    {

        [TestMethod]
        public void TypeExchangeTest()
        {
            var serializer = GetSerializer();

            Expression<Func<DTO, Boolean>> query = (dto) => dto.Id != Guid.Empty;
            var serializedExpression = serializer.Serialize(query);

            Trans(serializedExpression);

            var deserializedExpression = serializer.Deserialize(serializedExpression) as Expression<Func<Entity, Boolean>>;
            var result = deserializedExpression.Compile();
            Assert.IsTrue(result.Invoke(new Entity() { Id = Guid.NewGuid() }));
            Assert.IsFalse(result.Invoke(new Entity() { Id = Guid.Empty }));
        }
        [TestMethod]
        public void ClosureLambdaTest()
        {
            var serializer = GetSerializer();

            Expression<Func<DTO, Boolean>> query = (dto) => dto.Name == "";
            var serializedExpression = serializer.Serialize(query);
            serializedExpression.Save("ContrantExpression.xml");

            var name = "";
            query = (dto) => dto.Name == name;
            serializedExpression = serializer.Serialize(query);
            serializedExpression.Save("ClosureExpression.xml");

            //query = (dto) => dto.Id == new Guid("66232792-DC47-4A98-A83B-73FE886352B0");
            //serializedExpression = serializer.Serialize(query);
            //serializedExpression.Save("InstanceExpression.xml");

        }

        [TestMethod]
        public void ClosureTransTest()
        {
            var serializer = GetSerializer();
            var id = Guid.NewGuid();
            Expression<Func<DTO, Boolean>> query = (dto) => dto.Id == id;
            var serializedExpression = serializer.Serialize(query);

            Trans(serializedExpression);

            var deserializedExpression = serializer.Deserialize(serializedExpression) as Expression<Func<Entity, Boolean>>;

            if (deserializedExpression != null)
            {
                var result = deserializedExpression.Compile();
                Assert.IsTrue(result.Invoke(new Entity() { Id = id }));
                Assert.IsFalse(result.Invoke(new Entity() { Id = Guid.Empty }));
            }
        }


        private void Trans(XElement serializedExpression)
        {
            serializedExpression.Save("beforeTransit.xml");
            Console.WriteLine("beforeTransit:");
            Console.WriteLine(serializedExpression.ToString());
            Transit(serializedExpression);
            Console.WriteLine("afterTransit:");
            Console.WriteLine(serializedExpression.ToString());
            serializedExpression.Save("afterTransit.xml");
        }

        private static ExpressionSerializer GetSerializer()
        {
            var tr = new TypeResolver(new Assembly[] { Assembly.GetAssembly(typeof(ExpressionSerializationTest)) },
                                      new Type[] { typeof(DTO), typeof(Entity), typeof(BaseClass) });
            var serializer = new ExpressionSerializer(tr, new List<CustomExpressionXmlConverter>());
            return serializer;
        }

        private void Transit(System.Xml.Linq.XElement expression)
        {
            if (expression == null)
            {
                return;
            }

            if (expression.Elements().Any())
            {
                foreach (var element in expression.Elements())
                {
                    Transit(element);
                }
            }
            var attirbs = from attirb in expression.Attributes()
                          where attirb.Value == typeof(DTO).FullName
                          select attirb;
            foreach (var attrib in attirbs)
            {
                attrib.Value = typeof(Entity).FullName;
                //expression.ReplaceAttributes(new XAttribute("Name", typeof(Entity).FullName));
            }
        }
    }
    [Serializable]
    public class XObject : BaseClass
    {

    }
    [Serializable]
    public class BaseClass
    {
        public Guid Id { get; set; }
    }

    [Serializable]
    public class DTO : XObject
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }

    [Serializable]
    public class Entity : BaseClass
    {
        public String Name { get; set; }
    }

}
