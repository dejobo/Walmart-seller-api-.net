namespace WalmartAPI.Classes
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/", IsNullable = true)]
    public partial class activeIngredient
    {

        private string activeIngredientNameField;

        private decimal activeIngredientPercentageField;

        private bool activeIngredientPercentageFieldSpecified;

        /// <remarks/>
        public string activeIngredientName
        {
            get
            {
                return this.activeIngredientNameField;
            }
            set
            {
                this.activeIngredientNameField = value;
            }
        }

        /// <remarks/>
        public decimal activeIngredientPercentage
        {
            get
            {
                return this.activeIngredientPercentageField;
            }
            set
            {
                this.activeIngredientPercentageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool activeIngredientPercentageSpecified
        {
            get
            {
                return this.activeIngredientPercentageFieldSpecified;
            }
            set
            {
                this.activeIngredientPercentageFieldSpecified = value;
            }
        }
    }
}
