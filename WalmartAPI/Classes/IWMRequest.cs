using System;
using System.Net.Http;

namespace WalmartAPI.Classes
{
    public interface IWMRequest
    {
        HttpMethod httpMethod { get; }
        Uri requestUri { get; }
        WMRequest wmRequest { get; set; }
    }
}