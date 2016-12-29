using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.prices
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    public partial class priceDisplayCodes
    {

        private WalmartAPI.Classes.Walmart.prices.submapType submapTypeField;

        private bool submapTypeFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public WalmartAPI.Classes.Walmart.prices.submapType submapType
        {
            get
            {
                return this.submapTypeField;
            }
            set
            {
                this.submapTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool submapTypeSpecified
        {
            get
            {
                return this.submapTypeFieldSpecified;
            }
            set
            {
                this.submapTypeFieldSpecified = value;
            }
        }
    }
}
