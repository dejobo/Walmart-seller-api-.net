using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.prices
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class PriceFeed
    {

        private WalmartAPI.Classes.Walmart.prices.feedHeaderType priceHeaderField;

        private WalmartAPI.Classes.Walmart.prices.itemPriceType[] priceField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.feedHeaderType PriceHeader
        {
            get
            {
                return this.priceHeaderField;
            }
            set
            {
                this.priceHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Price")]
        public WalmartAPI.Classes.Walmart.prices.itemPriceType[] Price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }
    }
}
