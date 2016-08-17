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
    [System.Xml.Serialization.XmlRootAttribute("list", Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
    public partial class ordersListType
    {

        private WalmartAPI.Classes.Walmart.Orders.metaType metaField;

        private WalmartAPI.Classes.Walmart.Orders.Order[] elementsField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.metaType meta
        {
            get
            {
                return this.metaField;
            }
            set
            {
                this.metaField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("order")]
        public WalmartAPI.Classes.Walmart.Orders.Order[] elements
        {
            get
            {
                return this.elementsField;
            }
            set
            {
                this.elementsField = value;
            }
        }
    }
}
