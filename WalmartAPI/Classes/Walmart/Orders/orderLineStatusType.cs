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
    public partial class orderLineStatusType
    {

        private orderLineStatusValueType statusField;

        private quantityType statusQuantityField;

        private string cancellationReasonField;

        private WalmartAPI.Classes.Walmart.Orders.trackingInfoType trackingInfoField;

        /// <remarks/>
        public orderLineStatusValueType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public quantityType statusQuantity
        {
            get
            {
                return this.statusQuantityField;
            }
            set
            {
                this.statusQuantityField = value;
            }
        }

        /// <remarks/>
        public string cancellationReason
        {
            get
            {
                return this.cancellationReasonField;
            }
            set
            {
                this.cancellationReasonField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.trackingInfoType trackingInfo
        {
            get
            {
                return this.trackingInfoField;
            }
            set
            {
                this.trackingInfoField = value;
            }
        }
    }
}
