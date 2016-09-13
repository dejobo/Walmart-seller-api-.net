namespace WalmartAPI
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class errors
    {

        private WalmartAPI.Classes.Walmart.Responses.error[] errorField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("error")]
        public WalmartAPI.Classes.Walmart.Responses.error[] error
        {
            get
            {
                return this.errorField;
            }
            set
            {
                this.errorField = value;
            }
        }
    }
}
