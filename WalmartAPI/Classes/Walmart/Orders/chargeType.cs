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
    public partial class chargeType
    {

        private string chargeType1Field;

        private string chargeNameField;

        private WalmartAPI.Classes.Walmart.Orders.moneyType chargeAmountField;

        private WalmartAPI.Classes.Walmart.Orders.taxType taxField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("chargeType")]
        public string chargeType1
        {
            get
            {
                return this.chargeType1Field;
            }
            set
            {
                this.chargeType1Field = value;
            }
        }

        /// <remarks/>
        public string chargeName
        {
            get
            {
                return this.chargeNameField;
            }
            set
            {
                this.chargeNameField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.moneyType chargeAmount
        {
            get
            {
                return this.chargeAmountField;
            }
            set
            {
                this.chargeAmountField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Orders.taxType tax
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
