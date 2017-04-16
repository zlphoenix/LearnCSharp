using EFDemo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFDemoTest
{
    [TestClass]
    public class EntityCrudTests
    {
        [TestMethod]
        public void CreateOrderTest()
        {
            using (var db = new OrderDbContext())
            {
                var me = new Customer
                {
                    ID = Guid.NewGuid().ToString(),
                    Code = "Allen",
                    Name = "Allen",
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,

                };
                db.Customers.Add(me);
                var newOrder = CreateOrder(me);
                db.Orders.Add(newOrder);

                var order = (from o in db.Orders
                             where o.OrderCode == newOrder.ID
                             select o).FirstOrDefault();

                //提交到数据库之前的Query是无法访问缓存中的数据的
                Assert.IsNull(order);

                db.SaveChanges();


            }
        }

        private static SalesOrder CreateOrder(Customer me)
        {
            return new SalesOrder
            {
                ID = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                DocStatus = DocStatus.Open,
                OrderCode = "SO001",
                Customer = me,

                DocDetails = new List<DocDetail>()
                    {
                        new DocDetail
                        {
                            ID = Guid.NewGuid().ToString(),
                            Item ="T440P",
                            CreatedOn =DateTime.Now,
                            ModifiedOn = DateTime.Now,
                        }
                    }
            };
        }
    }
}
