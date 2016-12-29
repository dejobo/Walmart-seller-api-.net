using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.prices
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute("Price", Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class itemPriceType
    {

        private WalmartAPI.Classes.Walmart.prices.itemIdentifierType itemIdentifierField;

        private WalmartAPI.Classes.Walmart.prices.pricingType[] pricingListField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.itemIdentifierType itemIdentifier
        {
            get
            {
                return this.itemIdentifierField;
            }
            set
            {
                this.itemIdentifierField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("pricing", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.prices.pricingType[] pricingList
        {
            get
            {
                return this.pricingListField;
            }
            set
            {
                this.pricingListField = value;
            }
        }
    }
}
