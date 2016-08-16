using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Extensions;

namespace WalmartAPI.Classes
{
    class getOrders
    {
        void requestOrders()
        {
            var request = WebRequest.Create("https://marketplace.walmartapis.com/v3/");
            
            //get created orders
            var reqStream = request.GetRequestStream();

            var res = request.GetResponse();
            //ordersListType ordersResponse;
            using (var resStream = new StreamReader(res.GetResponseStream()))
            {
                using (var xmlStream = new XmlTextReader(resStream))
                {
                    //ordersResponse = xmlStream.ReadContentAs(typeof(ordersListType), null) as ordersListType;
                }
            }
        }
    }
}
