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
using System.Text;
using Serilog.Events;

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
            setHeaders();

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
            return getWMresponse<T>(method, string.Empty);
        }
        public T getWMresponse<T>(HttpMethod method, string requestBody)
        {
            return getWMresponse<T>(method, requestBody, "application/xml");
        }
        public T getWMresponse<T>(HttpMethod method,string requestBody, string contentType)
        {
            var fileUpload = contentType == "multipart/form-data";

            var tp = typeof(T);
            try
            {
                Log.Debug("Getting WM response for type {type}", tp.GetType());
                request.Method = method.Method;
                _authentication.httpRequestMethod = request.Method;
                _authentication.baseUrl = request.Address.AbsoluteUri;
                _authentication.signData();

                //set contentType
                request.ContentType = contentType;

                setAuthHeaders();
                var postingData = !requestBody.IsNullOrEmpty();
                if (fileUpload)
                {

                    var content = Encoding.UTF8.GetBytes(requestBody);
                    request.ContentLength = content.Length;



                    var headers = new WebHeaderCollection();

                    headers.Add(HttpRequestHeader.ContentLength, content.Length.ToString());

                    foreach (var item in request.Headers.AllKeys)
                    {
                        headers.Add(item, request.Headers.Get(item));
                    }

                    var cli = new WebClient() { Headers = headers };
                    //cli.Headers[HttpRequestHeader.ContentType] = contentType;
                    //cli.Headers.Add(HttpRequestHeader.ContentLength, content.Length.ToString());
                    //using (var str = cli.OpenWrite(request.RequestUri, method.Method))
                    //{
                    //    str.Write(content, 0, content.Length);
                    //}
                    //using (var writer = new StreamWriter(@"c:\temp\tstWminv.xml"))
                    //{
                    //    writer.Write(requestBody);
                    //}
                    //var rsp = cli.UploadFile(request.RequestUri, @"c:\temp\tstWminv.xml");
                    var rsp = cli.UploadData(request.RequestUri, method.Method, content);

                    using (var strm = new MemoryStream(rsp))
                    {
                        var xmlDesrializer = new XmlSerializer(typeof(T));
                        var resObj = xmlDesrializer.Deserialize(strm);


                        var res = (T)resObj;
                        return res;

                    }


                }
                else
                {
                    byte[] data = null;
                    //set body
                    if (postingData)
                    {
                        Log.Debug("posting data {requestBody}", requestBody);
                        data = Encoding.Default.GetBytes(requestBody);
                        request.ContentLength = data.Length;

                        using (var stream = request.GetRequestStream())
                        {
                            stream.Write(data, 0, data.Length);
                        }
                    }
                    else
                    {
                        request.ContentLength = 0;
                    }


                    if(request.RequestUri.AbsoluteUri != _authentication.baseUrl)
                    {
                        throw new InvalidDataException("The request uri and the authentication uri don't match");
                    }

                    //request.ContentType = "application/xml";
                    var rs = request.GetResponse();
                    using (var response = rs.GetResponseStream())
                    {

                        var xmlDesrializer = new XmlSerializer(typeof(T));
                        var resObj = xmlDesrializer.Deserialize(response);

                        var res = (T)resObj;

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
            }
            catch (WebException wex)
            {
                try
                {
                    using (var response = wex.Response.GetResponseStream())
                    {
                        var xmlDesrializer = new XmlSerializer(typeof(errors));
                        //var rsStr = new StreamReader(response).ReadToEnd();
                        var resObj = xmlDesrializer.Deserialize(response) as errors;

                        foreach (var err in resObj.error)
                        {
                            //get error info from error object
                            var level = new LogEventLevel();
                            switch (err.severity)
                            {
                                case errorSeverity.INFO:
                                    level = LogEventLevel.Information;
                                    break;
                                case errorSeverity.WARN:
                                    level = LogEventLevel.Warning;
                                    break;
                                case errorSeverity.ERROR:
                                    level = LogEventLevel.Error;
                                    break;
                            }

                            Log.Write(level, wex, err.description);
                        }
                        throw;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

                
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }


        private void setAuthHeaders()
        {
            request.Headers.Add("WM_SEC.AUTH_SIGNATURE:{0}".FormatWith(_authentication.signature));
            request.Headers.Add("WM_SEC.TIMESTAMP:{0}".FormatWith(_authentication.timeStamp));
        }

        private void setHeaders()
        {
            try
            {
                if (_authentication == null)
                    _authentication = new Authentication();
                _authentication.baseUrl = request.RequestUri.AbsoluteUri;
                //_correlationId = Guid.NewGuid().ToString().Substring(0, 5);
                //sign data...........
                _authentication.signData();

                request.Accept = "application/xml";
                //request.Host = "https://marketplace.walmartapis.com";
                request.Headers.Add("WM_SVC.NAME:Walmart Marketplace");
                request.Headers.Add("WM_CONSUMER.ID:{0}".FormatWith(_authentication.consumerId));
                request.Headers.Add("WM_QOS.CORRELATION_ID:{0}".FormatWith(_authentication.correlationId));
                //request.ContentType = "application/xml";

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
