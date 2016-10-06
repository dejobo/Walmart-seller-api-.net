using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WalmartAPI.Classes.Walmart.mp;

namespace WalmartAPI.Classes
{
    public class ItemsRequestResponse : IWMRequestResponse
    {
        public ItemsRequestResponse(Authentication authentication)
        {
            this._authentication = authentication;
            request = new ItemsRequest(_authentication);
            response = new ItemsResponse();
        }
        private Authentication _authentication { get; set; }
        public ItemsRequest request { get; set; }
        public ItemsResponse response { get; set; }

        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as ItemsRequest;
            }
        }

        IWMResponse IWMRequestResponse.response
        {
            get
            {
                return response;
            }

            set
            {
                response = value as ItemsResponse;
            }
        }

        public IEnumerable<Task<MPItemViews>> GetAllItemsAsync()
        {
            var limit = 20;
            var offset = 0;
            var Continue = true;
            while (Continue)
            {
                //var can = new CancellationToken(shouldBreak);
                var tsk = new Task<MPItemViews>(() =>
                {
                    var tmpAuth = new Authentication(_authentication.consumerId, _authentication.privateKey, _authentication.channelType);
                    request = new ItemsRequest(tmpAuth, limit, offset);
                    var res = new MPItemViews();
                    try
                    {
                        res = request.wmRequest.getWMresponse<MPItemViews>();
                    }
                    catch (WebException wex)
                    {
                        var rsp = wex.Response as HttpWebResponse;
                        if (rsp.StatusCode == HttpStatusCode.NotFound)
                        {
                            Continue = false;
                        }
                    }
                    return res;
                });
                //tsk.RunSynchronously();
                tsk.Start();
                //if (Continue)
                //    break;
                if (offset == 0)
                    offset++;
                offset += limit;
                yield return tsk;
            }
        }

        public IEnumerable<MPItemViews> getAllItems()
        {
            return getAllItems(20, 0);
        }

        public IEnumerable<MPItemViews> getAllItems(int limit,int offset)
        {
            //var limit = 20;
            //var offset = 0;

            while (true)
            {
                request = new ItemsRequest(_authentication, limit, offset);
                var res = new MPItemViews();
                try
                {
                    res = request.wmRequest.getWMresponse<MPItemViews>();
                }
                catch (WebException wex)
                {
                    var rsp = wex.Response as HttpWebResponse;
                    if (rsp.StatusCode == HttpStatusCode.NotFound)
                        break;
                    //try
                    //{
                    //    var xSeril = new XmlSerializer(typeof(errors));
                    //    using (var rsp = wex.Response.GetResponseStream())
                    //    {
                    //        var err = xSeril.Deserialize(rsp);
                    //        var errErr = (errors)err;
                    //        if (errErr.error.Single().description == "No item found")
                    //            break;
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    ex.LogWithSerilog();
                    //    throw;
                    //}
                    //throw;
                }
                if (offset==0)
                    offset++;
                offset += limit;
                yield return res;
            }
        }

        public class ItemsRequest : IWMRequest
        {
            internal ItemsRequest(Authentication authentication)
            {
                this._authentication = authentication;
                setRequestUri();
                setWmRequest();
            }
            internal ItemsRequest(Authentication authentication, int limit,int offset)// : this(authentication)
            {
                this._authentication = authentication;
                setRequestUri(limit, offset);
                setWmRequest();
            }

            private Authentication _authentication { get; set; }
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Get;
                }
            }

            public Uri requestUri { get; private set; }

            public WMRequest wmRequest { get; set; }

            void setRequestUri()
            {
                requestUri = new Uri(@"https://marketplace.walmartapis.com/v2/items");
            }
            void setRequestUri(int limit , int offset)
            {
                requestUri = new Uri(@"https://marketplace.walmartapis.com/v2/items?limit={0}&offset={1}".FormatWith(limit,offset));
            }
            void setWmRequest()
            {
                wmRequest = new WMRequest(requestUri.AbsoluteUri, _authentication);
            }
        }

        public class ItemsResponse : MPItemViews , IWMResponse
        {

        }
    }
}
