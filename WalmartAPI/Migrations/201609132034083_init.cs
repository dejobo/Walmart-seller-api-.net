namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WMSystemOrders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        orderNumber = c.String(maxLength: 255, unicode: false),
                        orderDate = c.DateTime(nullable: false),
                        estimatedShipDate = c.DateTime(nullable: false),
                        estimatedDeliveryDate = c.DateTime(nullable: false),
                        shippingMethod = c.String(),
                        customerName = c.String(),
                        customerAddress1 = c.String(),
                        customerAddress2 = c.String(),
                        customerCity = c.String(),
                        customerState = c.String(),
                        customerPostalCode = c.String(),
                        customerCountry = c.String(),
                        customerAddressType = c.String(),
                        customerPhoneNumber = c.String(),
                        customerEmail = c.String(),
                        lineNumber = c.String(maxLength: 255, unicode: false),
                        sku = c.String(),
                        itemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        itemTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        shippingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        shippingTax = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantity = c.Int(nullable: false),
                        orderLineStatus = c.String(),
                        orderTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isImported = c.Boolean(nullable: false),
                        lineTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => new { t.orderNumber, t.lineNumber }, unique: true, name: "IX_OrderNumber_OrderLine");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WMSystemOrders", "IX_OrderNumber_OrderLine");
            DropTable("dbo.WMSystemOrders");
        }
    }
}
