namespace EFDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WTF : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesOrders", "CreatedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesOrders", "CreatedOn", c => c.DateTime(nullable: false));
        }
    }
}
