namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class productIdentifier
    {

        private productIdentifierProductIdType productIdTypeField;

        private string productIdField;

        /// <remarks/>
        public productIdentifierProductIdType productIdType
        {
            get
            {
                return this.productIdTypeField;
            }
            set
            {
                this.productIdTypeField = value;
            }
        }

        /// <remarks/>
        public string productId
        {
            get
            {
                return this.productIdField;
            }
            set
            {
                this.productIdField = value;
            }
        }
    }
}
