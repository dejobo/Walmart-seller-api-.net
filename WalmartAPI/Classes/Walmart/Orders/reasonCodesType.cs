using System;
using System.Collections.Generic;
using System.Linq;

namespace WalmartAPI.Classes.Walmart.Orders
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1064.2")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://walmart.com/mp/v3/orders")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://walmart.com/mp/v3/orders", IsNullable = false)]
    public enum reasonCodesType
    {

        /// <remarks/>
        BillingError,

        /// <remarks/>
        TaxExemptCustomer,

        /// <remarks/>
        ItemNotAsAdvertised,

        /// <remarks/>
        IncorrectItemReceived,

        /// <remarks/>
        CancelledYetShipped,

        /// <remarks/>
        ItemNotReceivedByCustomer,

        /// <remarks/>
        IncorrectShippingPrice,

        /// <remarks/>
        DamagedItem,

        /// <remarks/>
        DefectiveItem,

        /// <remarks/>
        CustomerChangedMind,

        /// <remarks/>
        CustomerReceivedItemLate,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Missing Parts / Instructions")]
        MissingPartsInstructions,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Finance -> Goodwill")]
        FinanceGoodwill,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Finance -> Rollback")]
        FinanceRollback,
    }
}
