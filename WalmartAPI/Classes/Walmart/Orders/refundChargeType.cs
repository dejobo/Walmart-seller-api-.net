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
    public partial class refundChargeType
    {

        private WalmartAPI.Classes.Walmart.Orders.reasonCodesType refundReasonField;

        private WalmartAPI.Classes.Walmart.Orders.chargeType chargeField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.reasonCodesType refundReason
        {
            get
            {
                return this.refundReasonField;
            }
            set
            {
                this.refundReasonField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.chargeType charge
        {
            get
            {
                return this.chargeField;
            }
            set
            {
                this.chargeField = value;
            }
        }
    }
}
