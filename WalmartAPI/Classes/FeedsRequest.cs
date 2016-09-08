using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace WalmartAPI.Classes
{
    public class FeedsRequest : IWMRequest
    {
        public HttpMethod httpMethod
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Uri requestUri
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public WalmartAPI.Classes.WMRequest wmRequest { get; set; }

        public void getFeeds()
        {
            //wmRequest = new wmRequest("https://marketplace.walmartapis.com/v2/feeds");
            //wmRequest.getWMresponse();
        }
    }
}
