namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancellationDistinct : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.vw_WMCancelations", "reasonForCancellation");
            //DropColumn("dbo.vw_WMCancelations", "itemId");


            SqlFile(@"Migrations\vw_WMCancellations.sql");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.vw_WMCancelations", "itemId", c => c.String());
            //AddColumn("dbo.vw_WMCancelations", "reasonForCancellation", c => c.String());
            Sql("DROP VIEW vw_WMCancellations");
        }
    }
}
