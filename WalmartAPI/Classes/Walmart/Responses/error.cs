using System;

namespace WalmartAPI.Classes.Walmart.Responses
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class error
    {

        private string codeField;

        private string fieldField;

        private string descriptionField;

        private string infoField;

        private WalmartAPI.Classes.Walmart.Responses.errorSeverity severityField;

        private WalmartAPI.Classes.Walmart.Responses.errorCategory categoryField;

        private WalmartAPI.Classes.Walmart.Responses.cause[] causesField;

        private object errorIdentifiersField;

        /// <remarks/>
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string field
        {
            get
            {
                return this.fieldField;
            }
            set
            {
                this.fieldField = value;
            }
        }

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string info
        {
            get
            {
                return this.infoField;
            }
            set
            {
                this.infoField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Responses.errorSeverity severity
        {
            get
            {
                return this.severityField;
            }
            set
            {
                this.severityField = value;
            }
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Responses.errorCategory category
        {
            get
            {
                return this.categoryField;
            }
            set
            {
                this.categoryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Responses.cause[] causes
        {
            get
            {
                return this.causesField;
            }
            set
            {
                this.causesField = value;
            }
        }

        /// <remarks/>
        public object errorIdentifiers
        {
            get
            {
                return this.errorIdentifiersField;
            }
            set
            {
                this.errorIdentifiersField = value;
            }
        }
    }
}
