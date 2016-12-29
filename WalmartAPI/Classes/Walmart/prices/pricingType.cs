using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.prices
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    public partial class pricingType
    {

        private WalmartAPI.Classes.Walmart.prices.price currentPriceField;

        private WalmartAPI.Classes.Walmart.prices.priceType currentPriceTypeField;

        private bool currentPriceTypeFieldSpecified;

        private WalmartAPI.Classes.Walmart.prices.price comparisonPriceField;

        private WalmartAPI.Classes.Walmart.prices.priceDisplayCodes priceDisplayCodesField;

        private System.DateTime effectiveDateField;

        private bool effectiveDateFieldSpecified;

        private System.DateTime expirationDateField;

        private bool expirationDateFieldSpecified;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.price currentPrice
        {
            get
            {
                return this.currentPriceField;
            }
            set
            {
                this.currentPriceField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.priceType currentPriceType
        {
            get
            {
                return this.currentPriceTypeField;
            }
            set
            {
                this.currentPriceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool currentPriceTypeSpecified
        {
            get
            {
                return this.currentPriceTypeFieldSpecified;
            }
            set
            {
                this.currentPriceTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.price comparisonPrice
        {
            get
            {
                return this.comparisonPriceField;
            }
            set
            {
                this.comparisonPriceField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.priceDisplayCodes priceDisplayCodes
        {
            get
            {
                return this.priceDisplayCodesField;
            }
            set
            {
                this.priceDisplayCodesField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime effectiveDate
        {
            get
            {
                return this.effectiveDateField;
            }
            set
            {
                this.effectiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool effectiveDateSpecified
        {
            get
            {
                return this.effectiveDateFieldSpecified;
            }
            set
            {
                this.effectiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime expirationDate
        {
            get
            {
                return this.expirationDateField;
            }
            set
            {
                this.expirationDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool expirationDateSpecified
        {
            get
            {
                return this.expirationDateFieldSpecified;
            }
            set
            {
                this.expirationDateFieldSpecified = value;
            }
        }
    }
}
