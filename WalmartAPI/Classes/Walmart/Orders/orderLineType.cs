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
    public partial class orderLineType
    {

        private string lineNumberField;

        private WalmartAPI.Classes.Walmart.Orders.itemType itemField;

        private WalmartAPI.Classes.Walmart.Orders.chargeType[] chargesField;

        private quantityType orderLineQuantityField;

        private System.DateTime statusDateField;

        private WalmartAPI.Classes.Walmart.Orders.orderLineStatusType[] orderLineStatusesField;

        private WalmartAPI.Classes.Walmart.Orders.refundType refundField;

        /// <remarks/>
        public string lineNumber
        {
            get
            {
                return this.lineNumberField;
            }
            set
            {
                this.lineNumberField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.itemType item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("charge", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.chargeType[] charges
        {
            get
            {
                return this.chargesField;
            }
            set
            {
                this.chargesField = value;
            }
        }

        /// <remarks/>
        public quantityType orderLineQuantity
        {
            get
            {
                return this.orderLineQuantityField;
            }
            set
            {
                this.orderLineQuantityField = value;
            }
        }

        /// <remarks/>
        public System.DateTime statusDate
        {
            get
            {
                return this.statusDateField;
            }
            set
            {
                this.statusDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("orderLineStatus", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.orderLineStatusType[] orderLineStatuses
        {
            get
            {
                return this.orderLineStatusesField;
            }
            set
            {
                this.orderLineStatusesField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.refundType refund
        {
            get
            {
                return this.refundField;
            }
            set
            {
                this.refundField = value;
            }
        }
    }
}
