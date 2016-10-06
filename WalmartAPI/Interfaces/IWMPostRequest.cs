using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    interface IWMPostRequest : IWMRequest
    {
        string requestBody { get; set; }

    }
}
