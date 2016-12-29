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
        public T getWMresponse<T>(HttpMethod method, string requestBody, string contentType)
        {
            var fileUpload = contentType == "multipart/form-data";

            var tp = typeof(T);
            try
            {
                Log.Debug("Getting WM response for type {type}", tp.GetType().Name);
                request.Method = method.Method;
                _authentication.httpRequestMethod = request.Method;
                _authentication.baseUrl = request.Address.AbsoluteUri;
                _authentication.signData();

                //set contentType
                var postingData = !requestBody.IsNullOrEmpty();
                if (fileUpload)
                {
                    var fileBoundry = "--InventoryUpload{0}".FormatWith(Guid.NewGuid().ToString().Replace("-", string.Empty));

                    Log.Verbose("Created file boundry {FileBoundry}", fileBoundry);
                    var boundryData = "Content-Disposition: form-data; name=\"fileUpload\"; filename=\"Bulk_inventory.xml\"";
                    boundryData += Environment.NewLine;
                    boundryData += "Content-Type: text/xml";


                    requestBody = "--{0}{2}{1}{2}{2}{3}{2}{2}{0}--".FormatWith(fileBoundry,boundryData, Environment.NewLine, requestBody);

                    contentType += "; boundary={0}".FormatWith(fileBoundry);

                }

                request.ContentType = contentType;

                setAuthHeaders();

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


                if (request.RequestUri.AbsoluteUri != _authentication.baseUrl)
                {
                    throw new InvalidDataException("The request uri and the authentication uri don't match");
                }

                    //request.ContentType = "application/xml";
                    var rs = request.GetResponse();
                using (var response = CopyAndClose(rs.GetResponseStream()))
                {
                    //try logging the response
                    using (var rdr = new StreamReader(response))
                    {
                        if (response.CanSeek)
                        {
                            var rsp = rdr.ReadToEnd();
                            Log.Debug("Received web response {Response}", rsp);
                            response.Position = 0;
                        }

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
                        using (var rdr = new StreamReader(response))
                        {
                            try
                            {
                                var rsp = rdr.ReadToEnd();
                                response.Position = 0;

                                Log.Debug("Received web response {Response}", rsp);

                            }
                            catch (Exception ex)
                            {
                                throw ex.LogWithSerilog();
                            }

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
                            
                            if(resObj.error.Any(e => e.severity == errorSeverity.ERROR))
                            {
                                throw;
                            }
                            else
                            {
                                Log.Verbose("Returning null from WMRequest because a non error exception");
                                return default(T);
                            }


                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex.LogWithSerilog();
                }
            }
            catch (Exception ex)
            {
                throw ex.LogWithSerilog();
            }
        }
    


    


        private static Stream CopyAndClose(Stream inputStream)
        {
            const int readSize = 256;
            byte[] buffer = new byte[readSize];
            MemoryStream ms = new MemoryStream();

            int count = inputStream.Read(buffer, 0, readSize);
            while (count > 0)
            {
                ms.Write(buffer, 0, count);
                count = inputStream.Read(buffer, 0, readSize);
            }
            ms.Position = 0;
            inputStream.Close();
            return ms;
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
