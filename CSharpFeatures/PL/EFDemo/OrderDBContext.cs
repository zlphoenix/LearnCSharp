using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo
{
    public class OrderDbContext : DbContext
    {
        public DbSet<SalesOrder> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
     
    }
}
