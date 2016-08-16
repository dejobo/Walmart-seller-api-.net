namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class additionalProductAttribute
    {

        private string productAttributeNameField;

        private string productAttributeValueField;

        /// <remarks/>
        public string productAttributeName
        {
            get
            {
                return this.productAttributeNameField;
            }
            set
            {
                this.productAttributeNameField = value;
            }
        }

        /// <remarks/>
        public string productAttributeValue
        {
            get
            {
                return this.productAttributeValueField;
            }
            set
            {
                this.productAttributeValueField = value;
            }
        }
    }
}
