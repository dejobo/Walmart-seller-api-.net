using Extensions;
using MoreLinq;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WalmartAPI.Classes.Walmart.Feeds;
using WalmartAPI.Classes.Walmart.Inventory;

namespace WalmartAPI.Classes
{
    class InventoryFeedRequestResponse : IWMRequestResponse
    {
        public InventoryFeedRequest request { get; set; }
        public InventoryFeedResponse response { get; set; }
        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as InventoryFeedRequest;
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
                response = value as InventoryFeedResponse;
            }
        }
        private Authentication _authentication { get; set; }
        private static ILogger Log { get; set; }

        private InventoryFeedRequestResponse()
        {
            Log = Serilog.Log
                .ForContext<InventoryFeedRequestResponse>()
                .ForContext("", "");
        }
        public InventoryFeedRequestResponse(Authentication authentication) :this()
        {
            this._authentication = authentication;
            request = new InventoryFeedRequest(_authentication);
        }

        public void UpdateAllInventoryInBatch()
        {

        }

        public void UpdateAllInventory()
        {
            try
            {
                Log.Debug("Building the inventory feed");
                using (var db = General.GetContext())
                {
                    var feed = from f in db.systemInventorySet
                               select new inventory
                               {
                                   sku = f.sku,
                                   quantity = new Quantity { amount = (decimal)f.quantity, unit = UnitOfMeasurement.EACH },
                                   fulfillmentLagTime = f.fulfillmentLagTime
                               };

                    //creating batches of 2k items per batch
                    Log.Debug("Splitting up the ineventory to batches of 2k");
                    var feedBatches = feed.Batch(2000);
                    var taskList = new List<Task>();

                    foreach (var item in feedBatches)
                    {
                        var tsk = new Task(() =>
                        {
                            try
                            {
                                //create new InventoryFeedRequestResponseObject
                                var inventoryFeed = new InventoryFeedRequestResponse(_authentication);
                                inventoryFeed.request.inventoryFeed = new InventoryFeed
                                {
                                    InventoryHeader = new InventoryHeader
                                    {
                                        feedDate = DateTime.Now,
                                        feedDateSpecified = true,
                                        version = InventoryHeaderVersion.Item14,
                                    },
                                    Items = item.ToArray(),
                                };
                                inventoryFeed.request.SetRequestBody();

                                var resp = inventoryFeed.request.wmRequest.getWMresponse<FeedAcknowledgement>(HttpMethod.Post, inventoryFeed.request.requestBody, "multipart/form-data");

                                inventoryFeed.response = new InventoryFeedResponse();
                                inventoryFeed.response.InitFromWmType<FeedAcknowledgement>(resp);

                                Log.Information("Feed submitted with {ItemCount} items feed id is  {FeedId}", inventoryFeed.request.inventoryFeed.Items.Length, inventoryFeed.response.feedId);

                            }
                            catch (Exception ex)
                            {
                                ex.LogWithSerilog();
                            }
                        });
                        tsk.RunSynchronously();
                    }

                    Log.Information("Inventory uploads initiallized waiting for {Count} tasks to complete", taskList.Count());
                    Task.WaitAll(taskList.ToArray());
                    
                }
                Log.Debug("InventoryFeed object created with {Count} items", request.inventoryFeed.Items.Length);
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
            }
        }

        public class InventoryFeedRequest : IWMRequest , IWMPostRequest
        {
            private InventoryFeedRequest()
            {

            }
            public InventoryFeedRequest(Authentication authentication) : this()
            {
                _authentication = authentication;
                requestUri = new Uri("https://marketplace.walmartapis.com/v2/feeds?feedType=inventory");
                wmRequest = new WMRequest(requestUri.AbsoluteUri, _authentication);
                new InventoryFeed()
                {
                    Items = new[]
                    {
                        new inventory() { }
                    },
                    InventoryHeader = new InventoryHeader() { }
                };
                
            }
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Post;
                }
            }
            
            public string requestBody { get; set; }
            public InventoryFeed inventoryFeed { get; set; }
            public Uri requestUri { get; set; }

            public WMRequest wmRequest { get; set; }
            private Authentication _authentication { get; set; }

            public void SetRequestBody()
            {
                var xSerelize = new XmlSerializer(typeof(InventoryFeed));
                using (var memStr = new MemoryStream())
                {
                    using (var reader = new StreamReader(memStr))
                    {
                        Log.Debug("Building xml document from inventoryFeed object");
                        xSerelize.Serialize(memStr, inventoryFeed);
                        memStr.Position = 0;
                        requestBody = reader.ReadToEnd();

                    }
                }
            }

        }
        public class InventoryFeedResponse : FeedAcknowledgement, IWMResponse
        {

        }
    }
}
