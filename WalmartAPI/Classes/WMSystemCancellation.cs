using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    [Table("vw_WMCancelations")]
    public class WMSystemCancellation
    {
        public int id { get; set; }
        public string orderNumber { get; set; }
        //public string reasonForCancellation { get; set; }
        //public string itemId { get; set; }

    }
}
