namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class FeedAcknowledgement
    {

        private string feedIdField;

        private IngestionError errorField;

        /// <remarks/>
        public string feedId
        {
            get
            {
                return this.feedIdField;
            }
            set
            {
                this.feedIdField = value;
            }
        }

        /// <remarks/>
        public IngestionError error
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
