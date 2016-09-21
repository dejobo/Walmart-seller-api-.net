namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shippingUpdatedFlag : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.vw_WMShipments", "updated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            //DropColumn("dbo.vw_WMShipments", "updated");
        }
    }
}
