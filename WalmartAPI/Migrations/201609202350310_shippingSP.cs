namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shippingSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.WMSystemShipment_Insert",
                p => new
                    {
                        orderNumber = p.String(),
                        TrackingNumber = p.String(),
                        Method = p.String(),
                        Carrier = p.String(),
                        ShippedVia = p.String(),
                        CreatedTime = p.DateTime(),
                        updated = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[vw_WMShipments]([orderNumber], [TrackingNumber], [Method], [Carrier], [ShippedVia], [CreatedTime], [updated])
                      VALUES (@orderNumber, @TrackingNumber, @Method, @Carrier, @ShippedVia, @CreatedTime, @updated)
                      
                      DECLARE @id int
                      SELECT @id = [id]
                      FROM [dbo].[vw_WMShipments]
                      WHERE @@ROWCOUNT > 0 AND [id] = scope_identity()
                      
                      SELECT t0.[id]
                      FROM [dbo].[vw_WMShipments] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[id] = @id"
            );
            
            CreateStoredProcedure(
                "dbo.WMSystemShipment_Update",
                p => new
                    {
                        id = p.Int(),
                        orderNumber = p.String(),
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
            
            CreateStoredProcedure(
                "dbo.WMSystemShipment_Delete",
                p => new
                    {
                        id = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[vw_WMShipments]
                      WHERE ([id] = @id)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.WMSystemShipment_Delete");
            DropStoredProcedure("dbo.WMSystemShipment_Update");
            DropStoredProcedure("dbo.WMSystemShipment_Insert");
        }
    }
}
