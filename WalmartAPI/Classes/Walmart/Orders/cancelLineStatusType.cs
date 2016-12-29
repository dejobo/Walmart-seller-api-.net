namespace WalmartAPI.Classes.Walmart.Orders
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/mp/v3/orders", IsNullable = true)]
    public partial class cancelLineStatusType
    {

        private orderLineStatusValueType statusField;

        private cancellationReasonType cancellationReasonField;

        private quantityType statusQuantityField;

        /// <remarks/>
        public orderLineStatusValueType status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public cancellationReasonType cancellationReason
        {
            get
            {
                return this.cancellationReasonField;
            }
            set
            {
                this.cancellationReasonField = value;
            }
        }

        /// <remarks/>
        public quantityType statusQuantity
        {
            get
            {
                return this.statusQuantityField;
            }
            set
            {
                this.statusQuantityField = value;
            }
        }
    }
}
