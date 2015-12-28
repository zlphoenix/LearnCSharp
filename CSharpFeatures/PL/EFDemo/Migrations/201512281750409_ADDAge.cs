namespace EFDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDAge : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        Code = c.String(),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SalesOrders",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        OrderCode = c.String(),
                        DocStatus = c.Int(nullable: false),
                        Customer_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.DocDetails",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(nullable: false),
                        LineNum = c.Int(nullable: false),
                        Item = c.String(),
                        Qty = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Order_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SalesOrders", t => t.Order_ID)
                .Index(t => t.Order_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocDetails", "Order_ID", "dbo.SalesOrders");
            DropForeignKey("dbo.SalesOrders", "Customer_ID", "dbo.Customers");
            DropIndex("dbo.DocDetails", new[] { "Order_ID" });
            DropIndex("dbo.SalesOrders", new[] { "Customer_ID" });
            DropTable("dbo.DocDetails");
            DropTable("dbo.SalesOrders");
            DropTable("dbo.Customers");
        }
    }
}
