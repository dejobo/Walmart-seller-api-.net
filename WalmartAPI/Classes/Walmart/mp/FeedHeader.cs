namespace WalmartAPI.Classes.Walmart.mp
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute("Header", Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class FeedHeader
    {

        private FeedHeaderVersion versionField;

        private string partnerIdField;

        private string sellerIdField;

        private FeedHeaderTenant tenantField;

        private string localeField;

        private System.DateTime feedDateField;

        private bool feedDateFieldSpecified;

        private FeedHeaderFeedType feedTypeField;

        private string batchIdField;

        private string transactionIdField;

        private string fileNameField;

        private string dataSourceField;

        private string requestSourceField;

        private string responseCallbackUrlField;

        public FeedHeader()
        {
            this.versionField = FeedHeaderVersion.Item21;
            this.localeField = "en_US";
        }

        /// <remarks/>
        public FeedHeaderVersion version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string partnerId
        {
            get
            {
                return this.partnerIdField;
            }
            set
            {
                this.partnerIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string sellerId
        {
            get
            {
                return this.sellerIdField;
            }
            set
            {
                this.sellerIdField = value;
            }
        }

        /// <remarks/>
        public FeedHeaderTenant tenant
        {
            get
            {
                return this.tenantField;
            }
            set
            {
                this.tenantField = value;
            }
        }

        /// <remarks/>
        [System.ComponentModel.DefaultValueAttribute("en_US")]
        public string locale
        {
            get
            {
                return this.localeField;
            }
            set
            {
                this.localeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime feedDate
        {
            get
            {
                return this.feedDateField;
            }
            set
            {
                this.feedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool feedDateSpecified
        {
            get
            {
                return this.feedDateFieldSpecified;
            }
            set
            {
                this.feedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public FeedHeaderFeedType feedType
        {
            get
            {
                return this.feedTypeField;
            }
            set
            {
                this.feedTypeField = value;
            }
        }

        /// <remarks/>
        public string batchId
        {
            get
            {
                return this.batchIdField;
            }
            set
            {
                this.batchIdField = value;
            }
        }

        /// <remarks/>
        public string transactionId
        {
            get
            {
                return this.transactionIdField;
            }
            set
            {
                this.transactionIdField = value;
            }
        }

        /// <remarks/>
        public string fileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {
                this.fileNameField = value;
            }
        }

        /// <remarks/>
        public string dataSource
        {
            get
            {
                return this.dataSourceField;
            }
            set
            {
                this.dataSourceField = value;
            }
        }

        /// <remarks/>
        public string requestSource
        {
            get
            {
                return this.requestSourceField;
            }
            set
            {
                this.requestSourceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string responseCallbackUrl
        {
            get
            {
                return this.responseCallbackUrlField;
            }
            set
            {
                this.responseCallbackUrlField = value;
            }
        }
    }
}
