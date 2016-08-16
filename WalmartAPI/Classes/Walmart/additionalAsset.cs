namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class additionalAsset
    {

        private string altTextField;

        private string assetUrlField;

        private string assetTypeField;

        private additionalAssetAttribute[] additionalAssetAttributesField;

        /// <remarks/>
        public string altText
        {
            get
            {
                return this.altTextField;
            }
            set
            {
                this.altTextField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string assetUrl
        {
            get
            {
                return this.assetUrlField;
            }
            set
            {
                this.assetUrlField = value;
            }
        }

        /// <remarks/>
        public string assetType
        {
            get
            {
                return this.assetTypeField;
            }
            set
            {
                this.assetTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public additionalAssetAttribute[] additionalAssetAttributes
        {
            get
            {
                return this.additionalAssetAttributesField;
            }
            set
            {
                this.additionalAssetAttributesField = value;
            }
        }
    }
}
