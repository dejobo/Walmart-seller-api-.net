namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shippingOrderId : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.vw_WMShipments", "orderId", c => c.Int(nullable: false));
            AlterStoredProcedure(
                "dbo.WMSystemShipment_Insert",
                p => new
                    {
                        orderNumber = p.String(),
                        orderId = p.Int(),
                        TrackingNumber = p.String(),
                        Method = p.String(),
                        Carrier = p.String(),
                        ShippedVia = p.String(),
                        CreatedTime = p.DateTime(),
                        updated = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[vw_WMShipments]([orderNumber], [orderId], [TrackingNumber], [Method], [Carrier], [ShippedVia], [CreatedTime], [updated])
                      VALUES (@orderNumber, @orderId, @TrackingNumber, @Method, @Carrier, @ShippedVia, @CreatedTime, @updated)
                      
                      DECLARE @id int
                      SELECT @id = [id]
                      FROM [dbo].[vw_WMShipments]
                      WHERE @@ROWCOUNT > 0 AND [id] = scope_identity()
                      
                      SELECT t0.[id]
                      FROM [dbo].[vw_WMShipments] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[id] = @id"
            );
            
            AlterStoredProcedure(
                "dbo.WMSystemShipment_Update",
                p => new
                    {
                        id = p.Int(),
                        orderNumber = p.String(),
                        orderId = p.Int(),
                        TrackingNumber = p.String(),
                        Method = p.String(),
                        Carrier = p.String(),
                        ShippedVia = p.String(),
                        CreatedTime = p.DateTime(),
                        updated = p.Boolean(),
                    },
                body:
                    @"UPDATE b
                      SET b.ShippingUploadedAmazon = @updated
                      FROM 
	                    vw_WMShipments A
	                    JOIN ShippingDetails b on A.orderId = b.[Package Reference No 1]
                      WHERE (a.[id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            //DropColumn("dbo.vw_WMShipments", "orderId");
            //throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
