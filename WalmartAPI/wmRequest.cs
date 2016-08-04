using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Extensions;


namespace WalmartAPI
{
    class wmRequest
    {
        private string _consumerId { get; set; }
        private string correlationId { get; set; }
        public WebRequest request { get; private set; }


        public wmRequest()
        {

        }
        public wmRequest(string url)
        {
            Log.Logger = new LoggerConfiguration();

            Log.Verbose("Starting wmRequest with {url}", url);
            request = new WebRequest(url);
            request.Headers = getHeaders();
            
        }

        private WebHeaderCollection getHeaders()
        {
            var auth = new Authentication();

            //sign data...........

            var headers = new WebHeaderCollection();
            headers.Add("WM_SVC.NAME:Walmart Marketplace");
            headers.Add("WM_SEC.AUTH_SIGNATURE:{0}".FormatWith(auth.signature));
            headers.Add("WM_CONSUMER.ID:{0}".FormatWith(auth.consumerId));
            headers.Add("WM_SEC.TIMESTAMP:{0}".FormatWith(auth.timeStemp));
            headers.Add("WM_QOS.CORRELATION_ID:123456abcdef");
            headers.Add(HttpRequestHeader.Accept, "qpplication/xml");
            headers.Add("WM_CONSUMER.CHANNEL.TYPE:{0}".FormatWith(auth.channelType));
            headers.Add(HttpRequestHeader.Host, "https://marketplace.walmartapis.com");

            return headers;
        }
    }
}
