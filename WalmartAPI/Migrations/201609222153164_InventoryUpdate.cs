namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InventoryUpdate : DbMigration
    {
        public override void Up()
        {

            //custom create view
            Sql("CREATE VIEW vw_WMSystemInventory AS SELECT Item sku ,Quantity ,[leadtime-to-ship] fulfillmentLagTime FROM ItemList WHERE Discontinued=0");


            //CreateTable(
            //    "dbo.vw_WMSystemInventory",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            sku = c.String(),
            //            quantity = c.Int(nullable: false),
            //            fulfillmentLagTime = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.id);

        }

        public override void Down()
        {
            Sql("IF OBJECT_ID('vw_WMSystemInventory','V') IS NOT NULL DROP VIEW vw_WMSystemInventory");
            //DropTable("dbo.vw_WMSystemInventory");
        }
    }
}
