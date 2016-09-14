using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class WMSystemOrder
    {
        #region Properties
        public int id { get; set; }
        [Index("IX_OrderNumber_OrderLine",1,IsUnique =true)]
        [Column(TypeName ="varchar")]
        [StringLength(255)]
        public string orderNumber { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerPurchaseOrder { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime orderDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime estimatedShipDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime estimatedDeliveryDate { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string shippingMethod { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerName { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(500)]
        public string customerAddress1 { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerAddress2 { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerCity { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerState { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerPostalCode { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerCountry { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerAddressType { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerPhoneNumber { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string customerEmail { get; set; }
        [Index("IX_OrderNumber_OrderLine", 2, IsUnique = true)]
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string lineNumber { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string sku { get; set; }
        public decimal itemPrice { get; set; }
        public decimal itemTax { get; set; }
        public decimal shippingPrice { get; set; }
        public decimal shippingTax { get; set; }
        public int quantity { get; set; }
        [Column(TypeName = "varchar")]
        [StringLength(255)]
        public string orderLineStatus { get; set; }
        public decimal orderTotal { get; set; }
        public bool isImported { get; set; }
        public decimal lineTotal { get; set; }
        [Column(TypeName ="datetime2")]
        public DateTime dateAdded { get; set; }
        #endregion
    }
}
