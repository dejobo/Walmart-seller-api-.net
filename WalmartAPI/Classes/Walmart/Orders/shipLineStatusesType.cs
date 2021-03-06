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
    public partial class shipLineStatusesType
    {

        private WalmartAPI.Classes.Walmart.Orders.shipLineStatusType[] orderLineStatusField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("orderLineStatus")]
        public WalmartAPI.Classes.Walmart.Orders.shipLineStatusType[] orderLineStatus
        {
            get
            {
                return this.orderLineStatusField;
            }
            set
            {
                this.orderLineStatusField = value;
            }
        }
    }
}
