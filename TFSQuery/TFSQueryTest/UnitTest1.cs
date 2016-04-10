using Microsoft.VisualStudio.TestTools.UnitTesting;
using TFSQuery;

namespace TFSQueryTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var query = new QueryForChanges();
            query.GetTopChangedFiles();
        }
    }
}
