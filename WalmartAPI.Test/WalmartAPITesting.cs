using System;
using Xunit;
using System.IO;
using System.Xml.Serialization;
using System.CodeDom.Compiler;
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
using System.Linq;
using System.Net;
using Serilog.Context;
using static WalmartAPI.Classes.OrderCancelationRequestResponse;

namespace WalmartAPI.Test
{
    public class WalmartAPITesting : IDisposable
    {
        #region Test class implementation

        private readonly ITestOutputHelper output;
        const string consumerId = "bfcfcaac-433d-42b1-adee-3e8e81486cd0";
        const string channelType = "0f3e4dd4-0514-4346-b39d-af0e00ea066d";
        string privateKey;
        private Authentication auth;

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

            auth = new Authentication(consumerId, privateKey, channelType);

        }

        public static void ConfigureLogger()
        {
            //var applicationAssembly = Assembly.GetEntryAssembly().GetName();
            Serilog.Log.Logger = new LoggerConfiguration()
                            .WriteTo.Seq("http://srv3:5341", apiKey: "3yTsme0vzQWq50LW5ixB")
                            .WriteTo.Console()
                            .MinimumLevel.Verbose()
                            .Enrich.FromLogContext()
                            .Enrich.WithEnvironmentUserName()
                            .Enrich.WithMachineName()
                            .Enrich.WithProcessId()
                            .Enrich.WithThreadId()
                            .Enrich.WithProperty("ApplicationName", "WalmartAPITesting")
                            .Enrich.WithProperty("IsProduction", false)
                            .CreateLogger();
        }

        #endregion

        [Trait("Writes data", "false")]
        [Fact(DisplayName = "Xunit is working")]
        public void test()
        {
            string sut = "tst";
            Assert.Equal("tst", sut);
        }

        [Trait("Writes data", "false")]
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
        [Trait("Writes data", "false")]
        [InlineData(@"https://marketplace.walmartapis.com/v2/items", "GET")]
        [InlineData(@"https://marketplace.walmartapis.com/v2/feeds?feedType=inventory", "POST")]
        [InlineData(@"https://marketplace.walmartapis.com/v2/inventory?sku=114216", "PUT")]
        public void shouldCreateSignature(string url, string requestMethod)
        {

            var sut = auth;
            sut.baseUrl = url;
            sut.httpRequestMethod = requestMethod;
            sut.signData();
            output.WriteLine("WM_SVC.NAME:Walmart Marketplace");
            output.WriteLine("WM_QOS.CORRELATION_ID:{0}", sut.correlationId);
            output.WriteLine("WM_SEC.AUTH_SIGNATURE:{0}", sut.signature);
            output.WriteLine("WM_SEC.TIMESTAMP:{0}", sut.timeStamp);
            output.WriteLine("WM_CONSUMER.ID:{0}", sut.consumerId);

            Assert.True(true);
        }

        [Trait("Writes data", "false")]
        [Theory]
        [InlineData(@"mock\FeedRecordResponse.xml", typeof(feedRecordResponse))]
        [InlineData(@"mock\XMLFile.xml", typeof(feedRecordResponse))]
        [InlineData(@"mock\OrdersResponse.xml", typeof(ordersListType))]
        [InlineData(@"mock\CancelationResponse.xml", typeof(OrderCancelationResponse))]
        public void testDeserialization(string file, Type type)
        {
            using (var str = File.OpenRead(file))
            {
                var xs = new XmlSerializer(type);

                var sut = xs.Deserialize(str);

                Assert.IsType(type, sut);
            }
        }

        [Fact]
        [Trait("Writes data", "false")]
        public void orderRequestResponse()
        {

            var sut = new OrdersRequestResponse(new Authentication(consumerId, privateKey, channelType));
            var qs = new NameValueCollection();
            qs.Add("createdStartDate", DateTime.Now.AddDays(-3).Date.ToString("yyyy-MM-dd"));
            qs.Add("status", "Created");
            //qs.Add("status", "Acknowledged");
            sut.request.wmRequest.appendQueryStrings(qs);
            var resp = sut.request.wmRequest.getWMresponse<WalmartAPI.Classes.Walmart.Orders.ordersListType>();
            sut.response = new OrdersRequestResponse.GetOrdersResponse();
            sut.response.InitFromWmType<ordersListType>(resp);

            Assert.NotNull(sut.response);

            Assert.NotEmpty(sut.response.elements);
        }

        [Fact(Skip = "")]
        [Trait("Writes data", "true")]
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

        [Trait("Writes data", "false")]
        [Trait("Action", "Logging")]
        [Fact]
        public void loggerShouldWork()
        {
            var en = Log.IsEnabled(LogEventLevel.Error);
            Log.ForContext<Authentication>().Verbose("Testing with context");

            Log.Error("Testing Logger base");

            LogContext.PushProperty("Test", "hello");
            Log.Debug("Test value is {Test}");
        }


        [Fact]
        [Trait("Action", "Orders")]
        public void shouldAcknowlageOrders()
        {
            var sut = new PostOrderAcknowladgements(auth);
            sut.AcknowladgeImportedOrders();
        }

        //[Trait("Action", "Shipping")]
        //[Theory]
        //[InlineData("1576506880455", "9400115901264278320769", "USPS First Class Mail","USPS",4)]
        //public void shouldCreateShipment(string order,string tracking,string method,string carrier,int count)
        //{
        //    var sut = new ShippingUpdateRequestResponse.ShippingUpdateRequest(order).GetShipmentUpdateRequest();
        //    //is shipped
        //    Assert.True(sut.orderLines.All(c => c.orderLineStatuses.All(s => s.status == orderLineStatusValueType.Shipped)));
        //    //tracking number matches
        //    Assert.True(sut.orderLines.All(c => c.orderLineStatuses.All(s => s.trackingInfo.trackingNumber == tracking)));

        //    //carrier matches
        //    Assert.True(sut.orderLines.All(c => c.orderLineStatuses.All(s => s.trackingInfo.carrierName.Item.ToString() == carrier)));
        //    //check count
        //    Assert.True(sut.orderLines.Length == count);
        //}

        //[Trait("Action","Shipping")]
        //[Theory]
        //[InlineData("1017331470", @"mock\1017331470ship.xml")]
        //[InlineData("1576446489347", @"mock\1576446489347ship.xml")]
        //public void checkShipmentXmlMocs(string order, string mockFile)
        //{
        //    var sut = new ShippingUpdateRequestResponse.ShippingUpdateRequest(order).GetShipmentUpdateRequest();
        //    orderShipment sut2;
        //    var mock = new XmlSerializer(typeof(orderShipment));
        //    var ns = new XmlSerializerNamespaces();
        //    ns.Add("ns2", "http://walmart.com/mp/v3/orders");

        //    var xx = new orderShipment().GetType();//.Attributes;


        //    using (var str = new StreamReader(mockFile))
        //    {
        //        sut2 = mock.Deserialize(str) as orderShipment;
        //    }

        //    string sutXml;
        //    string sut2Xml;

        //    using (var writer = new MemoryStream())
        //    {
        //        using (var reader = new StreamReader(writer))
        //        {
        //            mock.Serialize(writer, sut,ns);
        //            writer.Position = 0;
        //            sutXml = reader.ReadToEnd();

        //            writer.SetLength(0);
        //            mock.Serialize(writer, sut2);
        //            writer.Position = 0;
        //            sut2Xml = reader.ReadToEnd();
        //        }
        //    }
        //    Log.Verbose("sut {xml}", sutXml);
        //    Log.Verbose("sut2 {xml}", sut2Xml);
        //    //output.WriteLine(sutXml);
        //    Assert.Equal(sutXml, sut2Xml);
        //}

        [Trait("Writes data", "false")]
        [Fact]
        public void testAttributes()
        {
            var xx = new orderShipment().GetType()
                .CustomAttributes
                .Where(a => a.NamedArguments.Any(n => n.MemberName == "Namespace"));


        }

        [Trait("Action", "Shipping")]
        [Fact]
        public void shouldUpdateAllShipping()
        {
            var sut = new ShippingUpdateRequestResponse(auth);
            sut.uploadAllShipping(true);
        }

        [Trait("Action", "Shipping")]
        [Theory]
        [InlineData("2577153728407")]
        public void shouldUpdateShipping(string order)
        {
            var sut = new ShippingUpdateRequestResponse(auth, order);
            sut.GetReponse();
            sut.SetToUpdated();
        }


        [Trait("Action", "Inventory")]
        [Trait("Writes data", "false")]
        [Fact]
        void shouldCreateInventoryFeed()
        {
            var sut = new InventoryFeedRequestResponse(auth);
            sut.UpdateAllInventory();
            sut.request.SetRequestBody();

            Assert.NotNull(sut.request.inventoryFeed);
            Assert.NotEmpty(sut.request.inventoryFeed.Items);
        }

        [Trait("Action", "Inventory")]
        [Trait("Writes data", "false")]
        [Fact]
        void shouldUpdateAllInventory()
        {
            var sut = new InventoryFeedRequestResponse(auth);
            sut.UpdateAllInventory();
        }

        [Trait("Action", "Inventory")]
        [Trait("Writes data", "false")]
        [Fact]
        public void shouldGetItems()
        {
            Log.Verbose("Testing shouldGetItems() {Status}", "started");
            var sut = new ItemsRequestResponse(auth) { request = new ItemsRequestResponse.ItemsRequest(auth, 20, 200000) };
            try
            {
                var res = sut.request.wmRequest.getWMresponse<MPItemViews>();
                Assert.NotNull(res);
                Assert.NotEmpty(res.MPItemView);
            }
            catch (WebException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }


            Log.Verbose("Testing shouldGetItems() {Status}", "compleeted");
        }
        [Trait("Action", "Inventory")]
        [Trait("Writes data", "false")]
        [Fact]
        void shouldGetAllItemsLazy()
        {
            var sutttt = new ItemsRequestResponse(auth);
            var res = sutttt.getAllItems();
            var sk = res.First().MPItemView.First().sku;
            output.WriteLine(sk);
        }

        [Trait("Writes data", "false")]
        [Fact]
        void shouldReturnTimespan() {
            var start = DateTime.Now;
            System.Threading.Thread.Sleep(5000);
            output.WriteLine("started {0} ended {1} span is {2}", start, DateTime.Now, DateTime.Now.Subtract(start).TotalSeconds);
        }

        [Trait("Action", "Cancellation")]
        [Trait("Writes data", "true")]
        [Theory]
        [InlineData("1577133989369")]
        void shouldCancelOrder(string order)
        {
            var sut = new OrderCancelationRequestResponse(auth);
            sut.CancelOrders(order);

        }

        [Trait("Action", "Cancellation")]
        [Trait("Writes data", "true")]
        [Fact]
        void shouldCancelAllOrder()
        {
            var sut = new OrderCancelationRequestResponse(auth);
            sut.CancelOrders();

        }

    }
}
