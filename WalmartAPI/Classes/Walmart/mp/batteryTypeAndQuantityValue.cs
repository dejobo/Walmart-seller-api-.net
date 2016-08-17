namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class batteryTypeAndQuantityValue
    {

        private batteryTypeAndQuantityValueBatteryTechnologyType batteryTechnologyTypeField;

        private bool batteryTechnologyTypeFieldSpecified;

        private string numberOfBatteriesField;

        /// <remarks/>
        public batteryTypeAndQuantityValueBatteryTechnologyType batteryTechnologyType
        {
            get
            {
                return this.batteryTechnologyTypeField;
            }
            set
            {
                this.batteryTechnologyTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool batteryTechnologyTypeSpecified
        {
            get
            {
                return this.batteryTechnologyTypeFieldSpecified;
            }
            set
            {
                this.batteryTechnologyTypeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string numberOfBatteries
        {
            get
            {
                return this.numberOfBatteriesField;
            }
            set
            {
                this.numberOfBatteriesField = value;
            }
        }
    }
}
