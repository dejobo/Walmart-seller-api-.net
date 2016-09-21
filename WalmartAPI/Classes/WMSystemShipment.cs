using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class WMSystemShipment
    {
        public int id { get; set; }
        public string orderNumber { get; set; }
        public int orderId { get; set; }
        public string TrackingNumber { get; set; }
        public string Method { get; set; }
        public string Carrier { get; set; }
        public string ShippedVia { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool updated { get; set; }

    }
}
