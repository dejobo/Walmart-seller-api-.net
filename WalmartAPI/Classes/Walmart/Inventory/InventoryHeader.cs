using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.Inventory
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class InventoryHeader
    {

        private WalmartAPI.Classes.Walmart.Inventory.InventoryHeaderVersion versionField;

        private System.DateTime feedDateField;

        private bool feedDateFieldSpecified;

        public InventoryHeader()
        {
            this.versionField = WalmartAPI.Classes.Walmart.Inventory.InventoryHeaderVersion.Item14;
        }

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Inventory.InventoryHeaderVersion version
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
    }
}
