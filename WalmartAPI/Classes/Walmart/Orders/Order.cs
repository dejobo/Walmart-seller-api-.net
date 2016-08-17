using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes.Walmart.Orders
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute("order", Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
    public partial class Order
    {

        private string purchaseOrderIdField;

        private string customerOrderIdField;

        private string customerEmailIdField;

        private System.DateTime orderDateField;

        private WalmartAPI.Classes.Walmart.Orders.shippingInfoType shippingInfoField;

        private WalmartAPI.Classes.Walmart.Orders.orderLineType[] orderLinesField;

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
        public string customerOrderId
        {
            get
            {
                return this.customerOrderIdField;
            }
            set
            {
                this.customerOrderIdField = value;
            }
        }

        /// <remarks/>
        public string customerEmailId
        {
            get
            {
                return this.customerEmailIdField;
            }
            set
            {
                this.customerEmailIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime orderDate
        {
            get
            {
                return this.orderDateField;
            }
            set
            {
                this.orderDateField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.shippingInfoType shippingInfo
        {
            get
            {
                return this.shippingInfoField;
            }
            set
            {
                this.shippingInfoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("orderLine", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.orderLineType[] orderLines
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
