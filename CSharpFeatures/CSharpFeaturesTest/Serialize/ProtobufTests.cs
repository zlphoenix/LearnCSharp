using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFeatures.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Serialize
{
    [TestClass]
    public class ProtobufTests
    {
        [TestMethod]
        public void ProtoListTest()
        {
            var expected = new List<int> { 1, 2, 3, 4, 5 };
            var actual = ProtoBufSerializer.Do(expected);
            Assert.AreEqual(expected.Count, actual.Count);
            //expected.Aggregate(true, (seed, item, result) =>
            //{
            //    return false;
            //});
            Assert.AreEqual(expected.Count, actual.Count);

        }
    }
}
