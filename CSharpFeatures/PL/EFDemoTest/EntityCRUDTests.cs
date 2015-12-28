using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDemo;
using System.Collections.Generic;

namespace EFDemoTest
{
    [TestClass]
    public class EntityCRUDTests
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
                    CreatedOn =DateTime.Now,
                    ModifiedOn = DateTime.Now,
                
                };
                db.Customers.Add(me);

                db.Orders.Add(new SalesOrder
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
                });

                db.SaveChanges();


            }
        }
    }
}
