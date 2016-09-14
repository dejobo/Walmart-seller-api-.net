namespace WalmartAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixVarchar : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WMSystemOrders", "customerPurchaseOrder", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "shippingMethod", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerName", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerAddress1", c => c.String(maxLength: 500, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerAddress2", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerCity", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerState", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerPostalCode", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerCountry", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerAddressType", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerPhoneNumber", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "customerEmail", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "sku", c => c.String(maxLength: 255, unicode: false));
            AlterColumn("dbo.WMSystemOrders", "orderLineStatus", c => c.String(maxLength: 255, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WMSystemOrders", "orderLineStatus", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "sku", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerEmail", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerPhoneNumber", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerAddressType", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerCountry", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerPostalCode", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerState", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerCity", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerAddress2", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerAddress1", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerName", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "shippingMethod", c => c.String());
            AlterColumn("dbo.WMSystemOrders", "customerPurchaseOrder", c => c.String());
        }
    }
}
