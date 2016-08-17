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
    public partial class trackingInfoType
    {

        private System.DateTime shipDateTimeField;

        private WalmartAPI.Classes.Walmart.Orders.carrierNameType carrierNameField;

        private WalmartAPI.Classes.Walmart.Orders.shippingMethodCodeType methodCodeField;

        private string trackingNumberField;

        private string trackingURLField;

        /// <remarks/>
        public System.DateTime shipDateTime
        {
            get
            {
                return this.shipDateTimeField;
            }
            set
            {
                this.shipDateTimeField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.carrierNameType carrierName
        {
            get
            {
                return this.carrierNameField;
            }
            set
            {
                this.carrierNameField = value;
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
        public string trackingNumber
        {
            get
            {
                return this.trackingNumberField;
            }
            set
            {
                this.trackingNumberField = value;
            }
        }

        /// <remarks/>
        public string trackingURL
        {
            get
            {
                return this.trackingURLField;
            }
            set
            {
                this.trackingURLField = value;
            }
        }
    }
}
