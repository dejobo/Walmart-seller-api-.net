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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
    public partial class orderRefund
    {

        private string purchaseOrderIdField;

        private WalmartAPI.Classes.Walmart.Orders.refundLineType[] orderLinesField;

        /// <remarks/>
        public string purchaseOrderId
        {
            get
            {
                return this.purchaseOrderIdField;
            }
            set
            {
                this.purchaseOrderIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("orderLine", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.refundLineType[] orderLines
        {
            get
            {
                return this.orderLinesField;
            }
            set
            {
                this.orderLinesField = value;
            }
        }
    }
}
