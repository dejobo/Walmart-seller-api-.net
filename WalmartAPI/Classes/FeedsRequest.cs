using System;
using System.Collections.Generic;
using System.Linq;

namespace WalmartAPI.Classes
{
    public class FeedsRequest : WalmartAPI.Classes.IWMRequest
    {
        public WalmartAPI.Classes.wmRequest wmRequest { get; set; }

        public void getFeeds()
        {
            //wmRequest = new wmRequest("https://marketplace.walmartapis.com/v2/feeds");
            //wmRequest.getWMresponse();
        }
    }
}
