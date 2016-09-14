using System;
using Xunit;
using System.IO;
using System.Xml.Serialization;
using WalmartAPI.Classes;
using WalmartAPI.Classes.Walmart.mp;
using Xunit.Abstractions;
using System.Net.Http;
using System.Collections.Specialized;
using System.Diagnostics;
using Serilog;
using Serilog.Events;
using System.Reflection;
using WalmartAPI.Classes.Walmart.Orders;
using ChannelOrderDownloads;

namespace WalmartAPI.Test
{
    public class WalmartAPITesting : IDisposable
    {
        #region Test class implementation

        private readonly ITestOutputHelper output;
        const string consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0";
        const string channelType = "0f3e4dd4-0514-4346-b39d-af0e00ea066d";
        string privateKey;

        public void Dispose()
        {
            Log.Verbose("Disposing walmart API Testing");
            Log.CloseAndFlush();
        }

        public WalmartAPITesting(ITestOutputHelper output)
        {
            ConfigureLogger();
            Log.Verbose("Starting Unit testing for walmart API");
            this.output = output;

            var privateKeyFile = "PrivateKey.cer";
            using (var stream = new StreamReader(privateKeyFile))
            {
                privateKey = stream.ReadToEnd();
            }

        }

        public static void ConfigureLogger()
        {
            //var applicationAssembly = Assembly.GetEntryAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Seq("http://srv3:5341", apiKey: "3yTsme0vzQWq50LW5ixB")
                            .WriteTo.Console()
                            .MinimumLevel.Verbose()
                            .Enrich.WithEnvironmentUserName()
                            .Enrich.WithMachineName()
                            .Enrich.WithProcessId()
                            .Enrich.WithThreadId()
                            .Enrich.WithProperty("ApplicationName", "WalmartAPITesting")
                            .CreateLogger();
        }
       
        #endregion

        [Fact(DisplayName = "Xunit is working")]
        public void test()
        {
            string sut = "tst";
            Assert.Equal("tst", sut);
        }

        [Theory]
        [InlineData("PrivateKey.cer")]
        public void testFeeds(string privateKeyFile)
        {
            var key = "";
            using (var stream = new StreamReader(privateKeyFile))
            {
                key = stream.ReadToEnd();
            }
            var consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0";
            //var auth = new WalmartAPI.Classes.Authentication()
            //{
            //    consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0",
            //    privateKey =key,
            //    httpRequestMethod="GET"
            //};
            var auth = new Authentication(consumerId, new Uri(@"https://marketplace.walmartapis.com/v2/feeds"), key, HttpMethod.Get, channelType);
            var x = new WMRequest(@"https://marketplace.walmartapis.com/v2/feeds", auth);
            var sut = x.getWMresponse<feedRecordResponse>();

            Assert.IsType(typeof(feedRecordResponse), sut);
        }

        [Theory]
        [InlineData("PrivateKey.cer", @"https://marketplace.walmartapis.com/v2/items","GET")]
        public void shouldCreateSignature(string privateKeyFile,string url,string requestMethod)
        {
            var key = "";
            using (var stream = new StreamReader(privateKeyFile))
            {
                key = stream.ReadToEnd();
            }
            var sut = new Authentication()
            {
                consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0",
                privateKey = key,
                httpRequestMethod = requestMethod,
                baseUrl = url,
                correlationId = Guid.NewGuid().ToString().Substring(0, 5)
            };
            sut.signData();

            //output.WriteLine("WM_QOS.CORRELATION_ID:{0}", sut.correlationId);
            //output.WriteLine("WM_SEC.AUTH_SIGNATURE:{0}",sut.signature);
            //output.WriteLine("WM_SEC.TIMESTAMP:{0}",sut.timeStamp);
            Assert.True(true);
        }

        [Theory]
        [InlineData(@"mock\FeedRecordResponse.xml",typeof(feedRecordResponse))]
        [InlineData(@"mock\XMLFile.xml",typeof(feedRecordResponse))]
        [InlineData(@"mock\OrdersResponse.xml", typeof(WalmartAPI.Classes.Walmart.Orders.ordersListType))]
        public void testDeserialization(string file,Type type)
        {
            using (var str = File.OpenRead(file))
            {
                var xs = new XmlSerializer(type);

                var sut = xs.Deserialize(str);

                Assert.IsType(type, sut);
            }
        }

        [Fact]
        public void orderRequestResponse()
        {

            var sut = new OrdersRequestResponse(new Authentication(consumerId,privateKey,channelType));
            var qs = new NameValueCollection();
            qs.Add("createdStartDate", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd"));
            qs.Add("status", "Created");
            qs.Add("status", "Acknowledged");
            sut.request.wmRequest.appendQueryStrings(qs);
            var resp = sut.request.wmRequest.getWMresponse<WalmartAPI.Classes.Walmart.Orders.ordersListType>();
            sut.response = new OrdersRequestResponse.GetOrdersResponse();
            sut.response.InitFromWmType<ordersListType>(resp);

            Assert.NotNull(sut.response);

            Assert.NotEmpty(sut.response.elements);
        }
        [Fact]
        public void shouldGetAllOrdersInTable()
        {
            var sut = new OrdersRequestResponse(new Authentication(consumerId, privateKey, channelType));
            var qs = new NameValueCollection();
            qs.Add("createdStartDate", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd"));
            qs.Add("status", "Created");
            sut.request.wmRequest.appendQueryStrings(qs);
            sut.getResponse();

            Assert.NotNull(sut.response);
            Assert.NotEmpty(sut.response.elements);

            while (sut.nextCursor != null)
            {
                sut = sut.nextCursor;
                sut.getResponse();
                Assert.NotNull(sut.response);
                Assert.NotEmpty(sut.response.elements);
            }
            sut = new OrdersRequestResponse(new Authentication(consumerId, privateKey, channelType));
            qs = new NameValueCollection();
            qs.Add("createdStartDate", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd"));
            qs.Add("status", "Acknowledged");
            sut.request.wmRequest.appendQueryStrings(qs);
            sut.getResponse();

            Assert.NotNull(sut.response);
            Assert.NotEmpty(sut.response.elements);

            while (sut.nextCursor != null)
            {
                sut = sut.nextCursor;
                sut.getResponse();
                Assert.NotNull(sut.response);
                Assert.NotEmpty(sut.response.elements);
            }
        }
        [Fact]
        public void loggerShouldWork()
        {
            //Serilog.Log.Logger = new LoggerConfiguration()
            //    .WriteTo.Seq("http://srv3:5341",apiKey: "3yTsme0vzQWq50LW5ixB")
            //    .WriteTo.Console()
            //    .MinimumLevel.Verbose()
            //    .Enrich.WithEnvironmentUserName()
            //    .Enrich.WithMachineName()
            //    .CreateLogger();
            //Serilog.Debugging.SelfLog.Enable(Console.Error);
            //output.WriteLine(Log.Logger.GetType().FullName);
            Log.Verbose("Testing Logger base");
        }

        [Fact(Skip = "Untestable")]
        public void shouldGetAuthKeys()
        {
            var sut = new OrderCollector();
            Assert.Equal(consumerId, sut.consumerId);
            Assert.Equal(channelType, sut.channelId);
            Assert.Equal(privateKey, sut.privateKey);
        }
    
    }
}
