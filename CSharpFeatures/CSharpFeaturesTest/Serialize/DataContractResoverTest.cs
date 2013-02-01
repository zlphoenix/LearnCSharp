using CSharpFeatures.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.Serialize
{
    [TestClass]
    public class DataContractResoverTest
    {
        [TestMethod]
        public void SerialiazeTest()
        {
            var serializer = new Serializer();
            serializer.serialize(typeof(PreferredVIPCustomer));
            serializer.deserialize(typeof(PreferredVIPCustomer));
        }
    }
}
