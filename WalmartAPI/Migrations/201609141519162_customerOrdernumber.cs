namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerOrdernumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WMSystemOrders", "customerPurchaseOrder", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WMSystemOrders", "customerPurchaseOrder");
        }
    }
}
