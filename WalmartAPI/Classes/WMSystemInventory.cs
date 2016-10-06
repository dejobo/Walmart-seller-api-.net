using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class WMSystemInventory
    {
        //public int id { get; set; }
        [Key]
        public string sku { get; set; }
        public int quantity { get; set; }
        public int fulfillmentLagTime { get; set; }
    }
}
