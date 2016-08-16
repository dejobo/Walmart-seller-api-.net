using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Extensions;
using Serilog;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;

namespace WalmartAPI
{
    public class wmRequest
    {
        private string _consumerId { get; set; }
        private string correlationId { get; set; }
        public HttpWebRequest request { get; private set; }
        private Authentication _authentication { get; set; }

        /// <summary>
        /// Creates a walmart api request with the required headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authentication"></param>
        public wmRequest(string url,Authentication authentication) //: this(url)
        {
            _authentication = authentication;

            Log.Verbose("Starting wmRequest with {url}", url);
            request = WebRequest.Create(url) as HttpWebRequest;
            getHeaders();
        }

        public wmRequest()
        {

        }
        /// <summary>
        /// Creates a walmart api request with the required headers.
        /// </summary>
        /// <param name="url"></param>
        public wmRequest(string url)
        {
            //Log.Logger = new LoggerConfiguration();

            Log.Verbose("Starting wmRequest with {url}", url);
            request = WebRequest.Create(url) as HttpWebRequest;
            getHeaders();
            
        }

        public T getWMresponse<T>()
        {
            //var requestStream = request.GetRequestStream();
            using (var response = request.GetResponse().GetResponseStream())
            {

                var xmlDesrializer = new XmlSerializer(typeof(T));
                var resObj = xmlDesrializer.Deserialize(response);

                return (T)resObj;
            }
        }
        private void getHeaders()
        {
            if (_authentication == null)
                _authentication = new Authentication();
            _authentication.baseUrl = request.RequestUri.AbsoluteUri;
            correlationId = Guid.NewGuid().ToString().Substring(0, 5);
            //sign data...........
            _authentication.signData();
            Debug.WriteLine("WM_SEC.AUTH_SIGNATURE:{0}", _authentication.signature);
            Debug.WriteLine("WM_SEC.TIMESTAMP:{0}", _authentication.timeStemp);


            request.Accept = "application/xml";
            //request.Host = "https://marketplace.walmartapis.com";
            request.Headers.Add("WM_SVC.NAME:Walmart Marketplace");
            request.Headers.Add("WM_SEC.AUTH_SIGNATURE:{0}".FormatWith(_authentication.signature));
            request.Headers.Add("WM_CONSUMER.ID:{0}".FormatWith(_authentication.consumerId));
            request.Headers.Add("WM_SEC.TIMESTAMP:{0}".FormatWith(_authentication.timeStemp));
            request.Headers.Add("WM_QOS.CORRELATION_ID:{0}".FormatWith(correlationId));
            //request.Headers.Add(HttpRequestHeader.Host, "https://marketplace.walmartapis.com");


            if (!_authentication.channelType.IsNullOrEmpty())
                request.Headers.Add("WM_CONSUMER.CHANNEL.TYPE:{0}".FormatWith(_authentication.channelType));

        }
    }
}
