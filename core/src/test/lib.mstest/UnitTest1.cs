using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dao = new CustomerDAO();
            var dt = dao.GetAllCustomers();
            Assert.IsTrue(dt.Rows.Count > 0);

        }
    }
}
