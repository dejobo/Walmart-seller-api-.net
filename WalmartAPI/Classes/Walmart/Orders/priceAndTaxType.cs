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
    public partial class priceAndTaxType
    {

        private WalmartAPI.Classes.Walmart.Orders.moneyType priceField;

        private WalmartAPI.Classes.Walmart.Orders.moneyType taxField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.moneyType price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.moneyType tax
        {
            get
            {
                return this.taxField;
            }
            set
            {
                this.taxField = value;
            }
        }
    }
}
