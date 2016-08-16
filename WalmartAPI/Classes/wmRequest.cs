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
using Serilog.Core;

namespace WalmartAPI.Classes
{
    public class wmRequest
    {
        private string _consumerId { get; set; }
        private string _correlationId { get; set; }
        public HttpWebRequest request { get; private set; }
        private WalmartAPI.Classes.Authentication _authentication { get; set; }

        /// <summary>
        /// Creates a walmart api request with the required headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authentication"></param>
        public wmRequest(string url,WalmartAPI.Classes.Authentication authentication) //: this(url)
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
            try
            {
                Log.Debug("Getting WM response for type {type}", typeof(T).MemberType.ToString());
                using (var response = request.GetResponse().GetResponseStream())
                {

                    var xmlDesrializer = new XmlSerializer(typeof(T));
                    var resObj = xmlDesrializer.Deserialize(response);

                    return (T)resObj;
                }
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }
        private void getHeaders()
        {
            try
            {
                if (_authentication == null)
                    _authentication = new WalmartAPI.Classes.Authentication();
                _authentication.baseUrl = request.RequestUri.AbsoluteUri;
                _correlationId = Guid.NewGuid().ToString().Substring(0, 5);
                //sign data...........
                _authentication.signData();
                Debug.WriteLine("WM_SEC.AUTH_SIGNATURE:{0}", _authentication.signature);
                Debug.WriteLine("WM_SEC.TIMESTAMP:{0}", _authentication.timeStamp);


                request.Accept = "application/xml";
                //request.Host = "https://marketplace.walmartapis.com";
                request.Headers.Add("WM_SVC.NAME:Walmart Marketplace");
                request.Headers.Add("WM_SEC.AUTH_SIGNATURE:{0}".FormatWith(_authentication.signature));
                request.Headers.Add("WM_CONSUMER.ID:{0}".FormatWith(_authentication.consumerId));
                request.Headers.Add("WM_SEC.TIMESTAMP:{0}".FormatWith(_authentication.timeStamp));
                request.Headers.Add("WM_QOS.CORRELATION_ID:{0}".FormatWith(_correlationId));


                if (!_authentication.channelType.IsNullOrEmpty())
                    request.Headers.Add("WM_CONSUMER.CHANNEL.TYPE:{0}".FormatWith(_authentication.channelType));
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }
    }
}
