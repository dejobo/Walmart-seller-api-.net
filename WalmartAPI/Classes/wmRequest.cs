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
using System.Collections.Specialized;
using WalmartAPI.Classes.Walmart.Responses;
using WalmartAPI.Classes.Walmart.Orders;
using System.Net.Http;

namespace WalmartAPI.Classes
{
    public class WMRequest
    {
        private HttpWebRequest request { get; set; }
        private Authentication _authentication { get; set; }

        /// <summary>
        /// Creates a walmart api request with the required headers.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="authentication"></param>
        public WMRequest(string url, Authentication authentication) //: this(url)
        {
            _authentication = authentication;

            SetRequest(url);
        }


        public void SetRequest(string url)
        {
            Log.Verbose("Starting wmRequest with {url}", url);
            request = WebRequest.Create(url) as HttpWebRequest;
            getHeaders();

        }
        public void appendQueryStrings(NameValueCollection queryCollection)
        {
            var uriBuild = new UriBuilder(request.Address);
            var qs = (from k in queryCollection.AllKeys
                      from v in queryCollection.GetValues(k)
                      select "{0}={1}".FormatWith(k, v))
                     .ToArray()
                     .Join("&");
            Log.Debug("Compiled {queryString}", qs);
            uriBuild.Query = qs;
            SetRequest(uriBuild.Uri.AbsoluteUri);
        }

        public T getWMresponse<T>()
        {
            return getWMresponse<T>(HttpMethod.Get);
        }

        public T getWMresponse<T>(HttpMethod method)
        {
            try
            {
                Log.Debug("Getting WM response for type {type}", typeof(T).MemberType.ToString());
                request.Method = method.Method;
                request.ContentLength = 0; //testin
                //request.ContentType = "application/xml";
                using (var response = request.GetResponse().GetResponseStream())
                {

                    var xmlDesrializer = new XmlSerializer(typeof(T));
                    var resObj = xmlDesrializer.Deserialize(response);

                    var res= (T)resObj;

                    var metaProperties = res.GetType().GetProperties()
                        .Where(p => p.PropertyType == typeof(metaType));
                    //check meta properties
                    if (metaProperties.Count() > 0)
                    {
                        //if(metaProperties.Count()>1)
                        Log.Debug("meta properties found, let's get it!");
                        var pp = metaProperties.Single();
                        var ppp = pp.GetValue(res) as metaType;
                        Log.Debug("got {nextCursor} from the meta", ppp.nextCursor);
                    }

                    return res;
                }
            }
            catch (WebException wex)
            {
                using (var response = wex.Response.GetResponseStream())
                {
                    var xmlDesrializer = new XmlSerializer(typeof(errors));
                    //var rsStr = new StreamReader(response).ReadToEnd();
                    var resObj = xmlDesrializer.Deserialize(response) as errors;
                    Log.Error(wex, resObj.error.First().description);
                    throw;
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
                    _authentication = new Authentication();
                _authentication.baseUrl = request.RequestUri.AbsoluteUri;
                //_correlationId = Guid.NewGuid().ToString().Substring(0, 5);
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
                request.Headers.Add("WM_QOS.CORRELATION_ID:{0}".FormatWith(_authentication.correlationId));
                request.ContentType = "application/xml";

                if (_authentication.channelType.IsNullOrEmpty())
                {
                    Log.Warning("WM_CONSUMER.CHANNEL.TYPE is not defined according to walmart documentation this is requred for orders api communication");
                }
                else
                {
                    request.Headers.Add("WM_CONSUMER.CHANNEL.TYPE:{0}".FormatWith(_authentication.channelType));
                }
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }
    }
}
