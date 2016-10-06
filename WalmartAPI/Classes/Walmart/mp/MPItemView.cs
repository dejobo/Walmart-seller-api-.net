namespace WalmartAPI.Classes.Walmart.mp
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class MPItemView
    {

        private Mart martField;

        private bool martFieldSpecified;

        private string skuField;

        private string wpidField;

        private string upcField;

        private string gtinField;

        private string productNameField;

        private string shelfField;

        private string productTypeField;

        private Money priceField;

        private ItemPublishStatus publishedStatusField;

        private bool publishedStatusFieldSpecified;

        /// <remarks/>
        public Mart mart
        {
            get
            {
                return this.martField;
            }
            set
            {
                this.martField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool martSpecified
        {
            get
            {
                return this.martFieldSpecified;
            }
            set
            {
                this.martFieldSpecified = value;
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
        public string wpid
        {
            get
            {
                return this.wpidField;
            }
            set
            {
                this.wpidField = value;
            }
        }

        /// <remarks/>
        public string upc
        {
            get
            {
                return this.upcField;
            }
            set
            {
                this.upcField = value;
            }
        }

        /// <remarks/>
        public string gtin
        {
            get
            {
                return this.gtinField;
            }
            set
            {
                this.gtinField = value;
            }
        }

        /// <remarks/>
        public string productName
        {
            get
            {
                return this.productNameField;
            }
            set
            {
                this.productNameField = value;
            }
        }

        /// <remarks/>
        public string shelf
        {
            get
            {
                return this.shelfField;
            }
            set
            {
                this.shelfField = value;
            }
        }

        /// <remarks/>
        public string productType
        {
            get
            {
                return this.productTypeField;
            }
            set
            {
                this.productTypeField = value;
            }
        }

        /// <remarks/>
        public Money price
        {
            get
            {
                return this.priceField;
            }
            set
            {
                this.priceField = value;
            }
        }

        /// <remarks/>
        public ItemPublishStatus publishedStatus
        {
            get
            {
                return this.publishedStatusField;
            }
            set
            {
                this.publishedStatusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool publishedStatusSpecified
        {
            get
            {
                return this.publishedStatusFieldSpecified;
            }
            set
            {
                this.publishedStatusFieldSpecified = value;
            }
        }
    }
}
