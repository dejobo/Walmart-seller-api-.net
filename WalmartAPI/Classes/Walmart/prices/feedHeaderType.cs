﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 


namespace WalmartAPI.Classes.Walmart.prices
{

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/")]
    [System.Xml.Serialization.XmlRootAttribute("PriceHeader", Namespace = "http://walmart.com/", IsNullable = false)]
    public partial class feedHeaderType
    {

        private WalmartAPI.Classes.Walmart.prices.feedHeaderTypeVersion versionField;

        private System.DateTime feedDateField;

        private bool feedDateFieldSpecified;

        /// <remarks/>
        public WalmartAPI.Classes.Walmart.prices.feedHeaderTypeVersion version
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