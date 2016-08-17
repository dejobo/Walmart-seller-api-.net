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
    public partial class priceType
    {

        private float totalField;

        private WalmartAPI.Classes.Walmart.Orders.priceAndTaxType retailField;

        private WalmartAPI.Classes.Walmart.Orders.priceAndTaxType shippingField;

        /// <remarks/>
        public float total
        {
            get
            {
                return this.totalField;
            }
            set
            {
                this.totalField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.priceAndTaxType retail
        {
            get
            {
                return this.retailField;
            }
            set
            {
                this.retailField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.priceAndTaxType shipping
        {
            get
            {
                return this.shippingField;
            }
            set
            {
                this.shippingField = value;
            }
        }
    }
}
