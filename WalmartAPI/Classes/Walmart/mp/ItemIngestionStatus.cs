namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class ItemIngestionStatus
    {

        private string productIdField;

        private string abstractProductIdField;

        private int martIdField;

        private bool martIdFieldSpecified;

        private string skuField;

        private string legacyItemIdField;

        private int indexField;

        private bool indexFieldSpecified;

        private string offerIdField;

        private ItemStatus ingestionStatusField;

        private IngestionError[] ingestionErrorsField;

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

        /// <remarks/>
        public string abstractProductId
        {
            get
            {
                return this.abstractProductIdField;
            }
            set
            {
                this.abstractProductIdField = value;
            }
        }

        /// <remarks/>
        public int martId
        {
            get
            {
                return this.martIdField;
            }
            set
            {
                this.martIdField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool martIdSpecified
        {
            get
            {
                return this.martIdFieldSpecified;
            }
            set
            {
                this.martIdFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string sku
        {
            get
            {
                return this.skuField;
            }
            set
            {
                this.skuField = value;
            }
        }

        /// <remarks/>
        public string legacyItemId
        {
            get
            {
                return this.legacyItemIdField;
            }
            set
            {
                this.legacyItemIdField = value;
            }
        }

        /// <remarks/>
        public int index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool indexSpecified
        {
            get
            {
                return this.indexFieldSpecified;
            }
            set
            {
                this.indexFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string offerId
        {
            get
            {
                return this.offerIdField;
            }
            set
            {
                this.offerIdField = value;
            }
        }

        /// <remarks/>
        public ItemStatus ingestionStatus
        {
            get
            {
                return this.ingestionStatusField;
            }
            set
            {
                this.ingestionStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("ingestionError", IsNullable = false)]
        public IngestionError[] ingestionErrors
        {
            get
            {
                return this.ingestionErrorsField;
            }
            set
            {
                this.ingestionErrorsField = value;
            }
        }
    }
}
