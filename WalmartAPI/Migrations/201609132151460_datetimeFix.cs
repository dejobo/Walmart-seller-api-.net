namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WMSystemOrders", "orderDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.WMSystemOrders", "estimatedShipDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.WMSystemOrders", "estimatedDeliveryDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WMSystemOrders", "estimatedDeliveryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WMSystemOrders", "estimatedShipDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.WMSystemOrders", "orderDate", c => c.DateTime(nullable: false));
        }
    }
}
