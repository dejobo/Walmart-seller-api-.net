using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.prices
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    public partial class itemIdentifierType
    {

        private string itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("sku")]
        public string Item
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
    }
}
