using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class PriceFeedRequestResponse : IWMRequestResponse
    {
        private Authentication _authentication { get; set; }
        public PriceFeedRequest request { get; set; }
        public PriceFeedResponse response { get; set; }

        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as PriceFeedRequest;
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
                response = value as PriceFeedResponse;
            }
        }

        public class PriceFeedRequest : IWMPostRequest
        {
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Post;
                }
            }

            public string requestBody { get; set; }

            public Uri requestUri { get; private set; }

            public WMRequest wmRequest { get; set; }



        }
        public class PriceFeedResponse : IWMResponse
        {

        }

    }
}
