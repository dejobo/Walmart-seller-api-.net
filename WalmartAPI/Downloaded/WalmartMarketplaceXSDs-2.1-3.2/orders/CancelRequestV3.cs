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
public partial class orderCancellation {
    
    private cancelLineType[] orderLinesField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("orderLine", IsNullable=false)]
    public cancelLineType[] orderLines {
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
public partial class cancelLineType {
    
    private string lineNumberField;
    
    private cancelLineStatusType[] orderLineStatusesField;
    
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
    [System.Xml.Serialization.XmlArrayItemAttribute("orderLineStatus", IsNullable=false)]
    public cancelLineStatusType[] orderLineStatuses {
        get {
            return this.orderLineStatusesField;
        }
        set {
            this.orderLineStatusesField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class cancelLineStatusType {
    
    private orderLineStatusValueType statusField;
    
    private cancellationReasonType cancellationReasonField;
    
    private quantityType statusQuantityField;
    
    /// <remarks/>
    public orderLineStatusValueType status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    public cancellationReasonType cancellationReason {
        get {
            return this.cancellationReasonField;
        }
        set {
            this.cancellationReasonField = value;
        }
    }
    
    /// <remarks/>
    public quantityType statusQuantity {
        get {
            return this.statusQuantityField;
        }
        set {
            this.statusQuantityField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public enum orderLineStatusValueType {
    
    /// <remarks/>
    Created,
    
    /// <remarks/>
    Acknowledged,
    
    /// <remarks/>
    Shipped,
    
    /// <remarks/>
    Cancelled,
    
    /// <remarks/>
    Refund,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public enum cancellationReasonType {
    
    /// <remarks/>
    CANCEL_BY_SELLER,
    
    /// <remarks/>
    CUSTOMER_REQUESTED_SELLER_TO_CANCEL,
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://walmart.com/mp/v3/orders")]
public partial class quantityType {
    
    private string unitOfMeasurementField;
    
    private string amountField;
    
    /// <remarks/>
    public string unitOfMeasurement {
        get {
            return this.unitOfMeasurementField;
        }
        set {
            this.unitOfMeasurementField = value;
        }
    }
    
    /// <remarks/>
    public string amount {
        get {
            return this.amountField;
        }
        set {
            this.amountField = value;
        }
    }
}
