namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipments2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.vw_WMShipment", "orderNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.vw_WMShipment", "orderNumber", c => c.Int(nullable: false));
        }
    }
}
