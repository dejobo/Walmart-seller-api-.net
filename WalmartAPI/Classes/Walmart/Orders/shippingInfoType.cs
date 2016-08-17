using System;
using System.Collections.Generic;
using System.Linq;

namespace WalmartAPI.Classes.Walmart.Orders
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/mp/v3/orders", IsNullable = true)]
    public partial class shippingInfoType
    {

        private string phoneField;

        private System.DateTime estimatedDeliveryDateField;

        private System.DateTime estimatedShipDateField;

        private WalmartAPI.Classes.Walmart.Orders.shippingMethodCodeType methodCodeField;

        private WalmartAPI.Classes.Walmart.Orders.postalAddressType postalAddressField;

        /// <remarks/>
        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }

        /// <remarks/>
        public System.DateTime estimatedDeliveryDate
        {
            get
            {
                return this.estimatedDeliveryDateField;
            }
            set
            {
                this.estimatedDeliveryDateField = value;
            }
        }

        /// <remarks/>
        public System.DateTime estimatedShipDate
        {
            get
            {
                return this.estimatedShipDateField;
            }
            set
            {
                this.estimatedShipDateField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.shippingMethodCodeType methodCode
        {
            get
            {
                return this.methodCodeField;
            }
            set
            {
                this.methodCodeField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.postalAddressType postalAddress
        {
            get
            {
                return this.postalAddressField;
            }
            set
            {
                this.postalAddressField = value;
            }
        }
    }
}
