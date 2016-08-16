namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class certificationsAndClaim
    {

        private string certificationAndClaimTypeField;

        private string certifyingAgentField;

        /// <remarks/>
        public string certificationAndClaimType
        {
            get
            {
                return this.certificationAndClaimTypeField;
            }
            set
            {
                this.certificationAndClaimTypeField = value;
            }
        }

        /// <remarks/>
        public string certifyingAgent
        {
            get
            {
                return this.certifyingAgentField;
            }
            set
            {
                this.certifyingAgentField = value;
            }
        }
    }
}
