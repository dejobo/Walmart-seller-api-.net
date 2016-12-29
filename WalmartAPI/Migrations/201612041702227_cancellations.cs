namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cancellations : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.vw_WMCancelations",
            //    c => new
            //        {
            //            id = c.Int(nullable: false, identity: true),
            //            orderNumber = c.String(),
            //            reasonForCancellation = c.String(),
            //            itemId = c.String(),
            //        })
            //    .PrimaryKey(t => t.id);


            SqlFile(@"Migrations\vw_WMCancellations.sql");
            
        }
        
        public override void Down()
        {
            Sql("DROP VIEW vw_WMCancellations");
            //DropTable("dbo.vw_WMCancelations");
        }
    }
}
