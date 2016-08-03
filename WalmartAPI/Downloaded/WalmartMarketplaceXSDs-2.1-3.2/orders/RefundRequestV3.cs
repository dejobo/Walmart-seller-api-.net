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


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://walmart.com/mp/v3/orders")]
[System.Xml.Serialization.XmlRootAttribute(Namespace="http://walmart.com/mp/v3/orders", IsNullable=false)]
public partial class orderRefund {
    
    private string purchaseOrderIdField;
    
    private refundLineType[] orderLinesField;
    
    /// <remarks/>
    public string purchaseOrderId {
        get {
            return this.purchaseOrderIdField;
        }
        set {
            this.purchaseOrderIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("orderLine", IsNullable=false)]
    public refundLineType[] orderLines {
        get {
            return this.orderLinesField;
        }
        set {
            this.orderLinesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class refundLineType {
    
    private string lineNumberField;
    
    private refundType[] refundsField;
    
    /// <remarks/>
    public string lineNumber {
        get {
            return this.lineNumberField;
        }
        set {
            this.lineNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("refund", IsNullable=false)]
    public refundType[] refunds {
        get {
            return this.refundsField;
        }
        set {
            this.refundsField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class refundType {
    
    private string refundIdField;
    
    private string refundCommentsField;
    
    private refundChargeType[] refundChargesField;
    
    /// <remarks/>
    public string refundId {
        get {
            return this.refundIdField;
        }
        set {
            this.refundIdField = value;
        }
    }
    
    /// <remarks/>
    public string refundComments {
        get {
            return this.refundCommentsField;
        }
        set {
            this.refundCommentsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("refundCharge", IsNullable=false)]
    public refundChargeType[] refundCharges {
        get {
            return this.refundChargesField;
        }
        set {
            this.refundChargesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class refundChargeType {
    
    private reasonCodesType refundReasonField;
    
    private chargeType chargeField;
    
    /// <remarks/>
    public reasonCodesType refundReason {
        get {
            return this.refundReasonField;
        }
        set {
            this.refundReasonField = value;
        }
    }
    
    /// <remarks/>
    public chargeType charge {
        get {
            return this.chargeField;
        }
        set {
            this.chargeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public enum reasonCodesType {
    
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

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class chargeType {
    
    private string chargeType1Field;
    
    private string chargeNameField;
    
    private moneyType chargeAmountField;
    
    private taxType taxField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("chargeType")]
    public string chargeType1 {
        get {
            return this.chargeType1Field;
        }
        set {
            this.chargeType1Field = value;
        }
    }
    
    /// <remarks/>
    public string chargeName {
        get {
            return this.chargeNameField;
        }
        set {
            this.chargeNameField = value;
        }
    }
    
    /// <remarks/>
    public moneyType chargeAmount {
        get {
            return this.chargeAmountField;
        }
        set {
            this.chargeAmountField = value;
        }
    }
    
    /// <remarks/>
    public taxType tax {
        get {
            return this.taxField;
        }
        set {
            this.taxField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class moneyType {
    
    private currencyType currencyField;
    
    private decimal amountField;
    
    /// <remarks/>
    public currencyType currency {
        get {
            return this.currencyField;
        }
        set {
            this.currencyField = value;
        }
    }
    
    /// <remarks/>
    public decimal amount {
        get {
            return this.amountField;
        }
        set {
            this.amountField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public enum currencyType {
    
    /// <remarks/>
    AED,
    
    /// <remarks/>
    AFN,
    
    /// <remarks/>
    ALL,
    
    /// <remarks/>
    AMD,
    
    /// <remarks/>
    ANG,
    
    /// <remarks/>
    AOA,
    
    /// <remarks/>
    ARS,
    
    /// <remarks/>
    AUD,
    
    /// <remarks/>
    AWG,
    
    /// <remarks/>
    AZN,
    
    /// <remarks/>
    BAM,
    
    /// <remarks/>
    BBD,
    
    /// <remarks/>
    BDT,
    
    /// <remarks/>
    BGN,
    
    /// <remarks/>
    BHD,
    
    /// <remarks/>
    BIF,
    
    /// <remarks/>
    BMD,
    
    /// <remarks/>
    BND,
    
    /// <remarks/>
    BOB,
    
    /// <remarks/>
    BRL,
    
    /// <remarks/>
    BSD,
    
    /// <remarks/>
    BTN,
    
    /// <remarks/>
    BWP,
    
    /// <remarks/>
    BYR,
    
    /// <remarks/>
    BZD,
    
    /// <remarks/>
    CAD,
    
    /// <remarks/>
    CDF,
    
    /// <remarks/>
    CHF,
    
    /// <remarks/>
    CLP,
    
    /// <remarks/>
    CNY,
    
    /// <remarks/>
    COP,
    
    /// <remarks/>
    CRC,
    
    /// <remarks/>
    CUP,
    
    /// <remarks/>
    CVE,
    
    /// <remarks/>
    CZK,
    
    /// <remarks/>
    DJF,
    
    /// <remarks/>
    DKK,
    
    /// <remarks/>
    DOP,
    
    /// <remarks/>
    DZD,
    
    /// <remarks/>
    EGP,
    
    /// <remarks/>
    ERN,
    
    /// <remarks/>
    ETB,
    
    /// <remarks/>
    EUR,
    
    /// <remarks/>
    FJD,
    
    /// <remarks/>
    FKP,
    
    /// <remarks/>
    GBP,
    
    /// <remarks/>
    GEL,
    
    /// <remarks/>
    GHS,
    
    /// <remarks/>
    GIP,
    
    /// <remarks/>
    GMD,
    
    /// <remarks/>
    GNF,
    
    /// <remarks/>
    GTQ,
    
    /// <remarks/>
    GYD,
    
    /// <remarks/>
    HKD,
    
    /// <remarks/>
    HNL,
    
    /// <remarks/>
    HRK,
    
    /// <remarks/>
    HTG,
    
    /// <remarks/>
    HUF,
    
    /// <remarks/>
    IDR,
    
    /// <remarks/>
    ILS,
    
    /// <remarks/>
    INR,
    
    /// <remarks/>
    IQD,
    
    /// <remarks/>
    IRR,
    
    /// <remarks/>
    ISK,
    
    /// <remarks/>
    JMD,
    
    /// <remarks/>
    JOD,
    
    /// <remarks/>
    JPY,
    
    /// <remarks/>
    KES,
    
    /// <remarks/>
    KGS,
    
    /// <remarks/>
    KHR,
    
    /// <remarks/>
    KMF,
    
    /// <remarks/>
    KPW,
    
    /// <remarks/>
    KRW,
    
    /// <remarks/>
    KWD,
    
    /// <remarks/>
    KYD,
    
    /// <remarks/>
    KZT,
    
    /// <remarks/>
    LAK,
    
    /// <remarks/>
    LBP,
    
    /// <remarks/>
    LKR,
    
    /// <remarks/>
    LRD,
    
    /// <remarks/>
    LSL,
    
    /// <remarks/>
    LTL,
    
    /// <remarks/>
    LVL,
    
    /// <remarks/>
    LYD,
    
    /// <remarks/>
    MAD,
    
    /// <remarks/>
    MDL,
    
    /// <remarks/>
    MGA,
    
    /// <remarks/>
    MKD,
    
    /// <remarks/>
    MMK,
    
    /// <remarks/>
    MNT,
    
    /// <remarks/>
    MOP,
    
    /// <remarks/>
    MRO,
    
    /// <remarks/>
    MUR,
    
    /// <remarks/>
    MVR,
    
    /// <remarks/>
    MWK,
    
    /// <remarks/>
    MXN,
    
    /// <remarks/>
    MYR,
    
    /// <remarks/>
    MZN,
    
    /// <remarks/>
    NAD,
    
    /// <remarks/>
    NGN,
    
    /// <remarks/>
    NIO,
    
    /// <remarks/>
    NOK,
    
    /// <remarks/>
    NPR,
    
    /// <remarks/>
    NZD,
    
    /// <remarks/>
    OMR,
    
    /// <remarks/>
    PAB,
    
    /// <remarks/>
    PEN,
    
    /// <remarks/>
    PGK,
    
    /// <remarks/>
    PHP,
    
    /// <remarks/>
    PKR,
    
    /// <remarks/>
    PLN,
    
    /// <remarks/>
    PYG,
    
    /// <remarks/>
    QAR,
    
    /// <remarks/>
    RON,
    
    /// <remarks/>
    RSD,
    
    /// <remarks/>
    RUB,
    
    /// <remarks/>
    RUR,
    
    /// <remarks/>
    RWF,
    
    /// <remarks/>
    SAR,
    
    /// <remarks/>
    SBD,
    
    /// <remarks/>
    SCR,
    
    /// <remarks/>
    SDG,
    
    /// <remarks/>
    SEK,
    
    /// <remarks/>
    SGD,
    
    /// <remarks/>
    SHP,
    
    /// <remarks/>
    SLL,
    
    /// <remarks/>
    SOS,
    
    /// <remarks/>
    SRD,
    
    /// <remarks/>
    STD,
    
    /// <remarks/>
    SYP,
    
    /// <remarks/>
    SZL,
    
    /// <remarks/>
    THB,
    
    /// <remarks/>
    TJS,
    
    /// <remarks/>
    TMT,
    
    /// <remarks/>
    TND,
    
    /// <remarks/>
    TOP,
    
    /// <remarks/>
    TRY,
    
    /// <remarks/>
    TTD,
    
    /// <remarks/>
    TWD,
    
    /// <remarks/>
    TZS,
    
    /// <remarks/>
    UAH,
    
    /// <remarks/>
    UGX,
    
    /// <remarks/>
    USD,
    
    /// <remarks/>
    UYU,
    
    /// <remarks/>
    UZS,
    
    /// <remarks/>
    VEF,
    
    /// <remarks/>
    VND,
    
    /// <remarks/>
    VUV,
    
    /// <remarks/>
    WST,
    
    /// <remarks/>
    XAF,
    
    /// <remarks/>
    XAG,
    
    /// <remarks/>
    XAU,
    
    /// <remarks/>
    XBA,
    
    /// <remarks/>
    XBB,
    
    /// <remarks/>
    XBC,
    
    /// <remarks/>
    XBD,
    
    /// <remarks/>
    XCD,
    
    /// <remarks/>
    XDR,
    
    /// <remarks/>
    XFU,
    
    /// <remarks/>
    XOF,
    
    /// <remarks/>
    XPD,
    
    /// <remarks/>
    XPF,
    
    /// <remarks/>
    XPT,
    
    /// <remarks/>
    XTS,
    
    /// <remarks/>
    XXX,
    
    /// <remarks/>
    YER,
    
    /// <remarks/>
    ZAR,
    
    /// <remarks/>
    ZMK,
    
    /// <remarks/>
    ZWL,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class taxType {
    
    private string taxNameField;
    
    private moneyType taxAmountField;
    
    /// <remarks/>
    public string taxName {
        get {
            return this.taxNameField;
        }
        set {
            this.taxNameField = value;
        }
    }
    
    /// <remarks/>
    public moneyType taxAmount {
        get {
            return this.taxAmountField;
        }
        set {
            this.taxAmountField = value;
        }
    }
}