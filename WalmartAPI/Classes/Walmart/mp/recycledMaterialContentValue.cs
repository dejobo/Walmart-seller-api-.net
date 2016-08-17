namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class recycledMaterialContentValue
    {

        private string recycledMaterialField;

        private decimal percentageOfRecycledMaterialField;

        private bool percentageOfRecycledMaterialFieldSpecified;

        /// <remarks/>
        public string recycledMaterial
        {
            get
            {
                return this.recycledMaterialField;
            }
            set
            {
                this.recycledMaterialField = value;
            }
        }

        /// <remarks/>
        public decimal percentageOfRecycledMaterial
        {
            get
            {
                return this.percentageOfRecycledMaterialField;
            }
            set
            {
                this.percentageOfRecycledMaterialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool percentageOfRecycledMaterialSpecified
        {
            get
            {
                return this.percentageOfRecycledMaterialFieldSpecified;
            }
            set
            {
                this.percentageOfRecycledMaterialFieldSpecified = value;
            }
        }
    }
}
