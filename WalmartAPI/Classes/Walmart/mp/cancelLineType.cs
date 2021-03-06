namespace WalmartAPI
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/mp/v3/orders", IsNullable = true)]
    public partial class cancelLineType
    {

        private string lineNumberField;

        private WalmartAPI.Classes.Walmart.Orders.cancelLineStatusType[] orderLineStatusesField;

        /// <remarks/>
        public string lineNumber
        {
            get
            {
                return this.lineNumberField;
            }
            set
            {
                this.lineNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("orderLineStatus", IsNullable = false)]
        public WalmartAPI.Classes.Walmart.Orders.cancelLineStatusType[] orderLineStatuses
        {
            get
            {
                return this.orderLineStatusesField;
            }
            set
            {
                this.orderLineStatusesField = value;
            }
        }
    }
}
