namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WMSystemOrders", "dateAdded", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WMSystemOrders", "dateAdded");
        }
    }
}
