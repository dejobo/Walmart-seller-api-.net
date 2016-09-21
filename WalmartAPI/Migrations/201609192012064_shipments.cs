namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipments : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.vw_WMShipments",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            orderNumber = c.String(),
            //            TrackingNumber = c.String(),
            //            Method = c.String(),
            //            Carrier = c.String(),
            //            ShippedVia = c.String(),
            //            CreatedTime = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            //DropTable("dbo.vw_WMShipments");
        }
    }
}
