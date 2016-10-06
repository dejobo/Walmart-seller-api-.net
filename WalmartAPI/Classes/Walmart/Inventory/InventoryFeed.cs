using System.Xml.Serialization;

namespace WalmartAPI.Classes.Walmart.Inventory
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class InventoryFeed
    {

        private WalmartAPI.Classes.Walmart.Inventory.InventoryHeader inventoryHeaderField;

        private WalmartAPI.Classes.Walmart.Inventory.inventory[] itemsField;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.Inventory.InventoryHeader InventoryHeader
        {
            get
            {
                return this.inventoryHeaderField;
            }
            set
            {
                this.inventoryHeaderField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("inventory")]
        public WalmartAPI.Classes.Walmart.Inventory.inventory[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
