using System;
using System.Collections.Generic;
using System.Linq;

namespace WalmartAPI
{
    public class FeedsRequest : IWMRequest
    {
        public wmRequest wmRequest { get; set; }

        public void getFeeds()
        {
            //wmRequest = new wmRequest("https://marketplace.walmartapis.com/v2/feeds");
            //wmRequest.getWMresponse();
        }
    }
}
