using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionSerialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Lambda
{
    [TestClass]
    public class ExpressionSerializationTest
    {
        [TestMethod]
        public void TypeExchangeTest()
        {
            var serializer = new ExpressionSerializer();
            Expression<Func<DTO, Boolean>> query = (dto) => dto.Id != Guid.Empty;
            var serializedExpression = serializer.Serialize(query);

            var deserializedExpression = serializer.Deserialize(serializedExpression);
        }
    }

    [Serializable]
    public class BaseClass
    {
        public Guid Id { get; set; }
    }

    [Serializable]
    public class DTO : BaseClass
    {
        public String Name { get; set; }
    }

    [Serializable]
    public class Entity : BaseClass
    {
        public String Name { get; set; }
    }

}
