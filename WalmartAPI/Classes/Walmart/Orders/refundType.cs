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
    public partial class refundType
    {

        private string refundIdField;

        private string refundCommentsField;

        private WalmartAPI.Classes.Walmart.Orders.refundChargeType[] refundChargesField;

        /// <remarks/>
        public string refundId
        {
            get
            {
                return this.refundIdField;
            }
            set
            {
                this.refundIdField = value;
            }
        }

        /// <remarks/>
        public string refundComments
        {
            get
            {
                return this.refundCommentsField;
            }
            set
            {
                this.refundCommentsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("refundCharge", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.refundChargeType[] refundCharges
        {
            get
            {
                return this.refundChargesField;
            }
            set
            {
                this.refundChargesField = value;
            }
        }
    }
}
