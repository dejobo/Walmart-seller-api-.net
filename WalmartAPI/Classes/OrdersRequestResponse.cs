using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WalmartAPI.Classes
{
    public class OrdersRequestResponse : IWMRequestResponse
    {
        #region Interface Implementation

        IWMRequest IWMRequestResponse.request
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IWMResponse IWMRequestResponse.response
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        public class GetOrdersRequest : IWMRequest
        {
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Get;
                }
            }

            public Uri requestUri { get; set; }

            public WMRequest wmRequest { get; set; }

            public GetOrdersRequest()
            {
                requestUri = new Uri("https://marketplace.walmartapis.com/v3/orders");
            }
            
        }
    }
}
