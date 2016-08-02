using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WalmartAPI
{
    class Class1
    {
        void requestOrders()
        {
            var request = WebRequest.Create("https://marketplace.walmartapis.com/v3/");
            var headers = new WebHeaderCollection();
            
            //Headers
            headers.Add("WM_SVC.NAME:Walmart Marketplace");
            headers.Add("WM_SEC.AUTH_SIGNATURE:");
            headers.Add("WM_CONSUMER.ID:bfcfcaac-433d-42b1-adee-3e8e81486cd0");
            headers.Add("WM_SEC.TIMESTAMP:");
            headers.Add("WM_QOS.CORRELATION_ID:123456abcdef");
            headers.Add(HttpRequestHeader.Accept, "qpplication/xml");
            headers.Add("WM_CONSUMER.CHANNEL.TYPE:");
            headers.Add(HttpRequestHeader.Host, "https://marketplace.walmartapis.com");

            //get created orders
            var reqStream = request.GetRequestStream();

            var res = request.GetResponse();
            ordersListType ordersResponse;
            using (var resStream = new StreamReader(res.GetResponseStream()))
            {
                using (var xmlStream = new XmlTextReader(resStream))
                {
                    ordersResponse = xmlStream.ReadContentAs(typeof(ordersListType), null) as ordersListType;
                }
            }
        }
    }
}
