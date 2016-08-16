namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class swatchImage
    {

        private string swatchImageUrlField;

        private string swatchVariantAttributeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "anyURI")]
        public string swatchImageUrl
        {
            get
            {
                return this.swatchImageUrlField;
            }
            set
            {
                this.swatchImageUrlField = value;
            }
        }

        /// <remarks/>
        public string swatchVariantAttribute
        {
            get
            {
                return this.swatchVariantAttributeField;
            }
            set
            {
                this.swatchVariantAttributeField = value;
            }
        }
    }
}
