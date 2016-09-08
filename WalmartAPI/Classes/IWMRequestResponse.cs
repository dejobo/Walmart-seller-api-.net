using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    interface IWMRequestResponse
    {
        IWMRequest request { get; set; }
        IWMResponse response { get; set; }
    }
}
