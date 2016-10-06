using Extensions;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WalmartAPI.Classes.Walmart.Inventory;
using WalmartAPI.Classes.Walmart.mp;
using MoreLinq;

namespace WalmartAPI.Classes
{
    public class InventoryRequestResponse : IWMRequestResponse
    {
        private Authentication _authentication { get; set; }
        private static ILogger Log { get; set; }
        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as InventoryRequest;
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
                response = value as InventoryResponse;
            }
        }
        public InventoryRequest request { get; set; }
        public InventoryResponse response { get; set; }

        private InventoryRequestResponse()
        {
            Log = Serilog.Log.ForContext<InventoryRequestResponse>();
        }
        public InventoryRequestResponse(Authentication authentication) :this()
        {
            _authentication = authentication;
        }
        private static inventory GetSystemInventory(string item)
        {
            inventory inv2;
            using (var db = General.GetContext())
            {
                Log.Verbose("Getting iqueryable object from SystemInventory view");
                var inv = from f in db.systemInventorySet
                          where f.sku == item
                          select f;

                inv2 = inv.Where(i => i.sku == item)
                .Select(w => new inventory
                {
                    sku = w.sku,
                    fulfillmentLagTime = w.fulfillmentLagTime,
                    quantity = new Quantity { amount = ((decimal)w.quantity), unit = Walmart.Inventory.UnitOfMeasurement.EACH }
                })
                .Single();
            }
            return inv2;
        }
        public void UpdateInventory(inventory inventory)
        {
            var req = new InventoryRequest(_authentication,inventory);
            var res =  req.GetResponse();
            response = new InventoryResponse();
            response.InitFromWmType<inventory>(res);
        }
        public void UpdateAllInventory()
        {
            UpdateAllInventory(20, 0);
        }
        private void UpdateInventryForItem(string item)
        {
            var sl = LogContext.PushProperty("Item", item);
            try
            {
                //Getting inventory from system
                var inv = GetSystemInventory(item);
                Log.Verbose("Updating {Item} on walmart", inv.sku);

                //creating new authentication
                var tmpAuth = new Authentication(_authentication.consumerId, _authentication.privateKey, _authentication.channelType);

                var invReqRes = new InventoryRequestResponse(tmpAuth)
                {
                    request = new InventoryRequest(tmpAuth, inv),
                    response = new InventoryResponse()
                };

                var res = invReqRes.request.GetResponse();
                invReqRes.response.InitFromWmType<inventory>(res);
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
            }
            finally
            {
                sl.Dispose();
            }
        }
        internal void UpdateAllInventory(int limit, int offset)
        {
            var lc = LogContext.PushProperty("InternalMethod", "UpdateAllInventory");

            var startTime = DateTime.Now;
            Log.Verbose("Getting all items from walmart that requires update");
            var items = new ItemsRequestResponse(_authentication).getAllItems(limit,offset);

            //var itemsNew = new ItemsRequestResponse(_authentication).GetAllItemsAsync();

            var taskList = new List<Task>();
            var taskList1 = new List<Task>();
            var curpos = 1;
            foreach (var item in items)
            {
                curpos++;
                var tsk1 = new Task(() =>
                {
                    try
                    {
                        //filtering items to published
                        var filterItems = item.MPItemView
                            .Where(i => i.publishedStatus == ItemPublishStatus.PUBLISHED)
                            //.Select(i => new Task(() => UpdateInventryForItem(i.sku))) // get tasks
                            //.Pipe(i => i.Start()) //start task
                            .ToList();

                        //taskList.AddRange(filterItems);

                        foreach (var item2 in filterItems)
                        {
                            Log.Verbose("Creating task to execute {Item}", item2.sku);

                            var tsk = new Task(() => UpdateInventryForItem(item2.sku));
                            //adding task to list for tracking
                            taskList.Add(tsk);
                            //tsk.RunSynchronously();
                            tsk.Start();
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.LogWithSerilog();
                    }

                });
                taskList1.Add(tsk1);
                tsk1.Start();
            }
            Log.Verbose("{Count} tasks started with inventory updates", taskList.Count());
            //taskList.ForEach(t => t.Start());
            //var tskStats = from t in taskList1
            //               group t by t.Status into g
            //               select g;
            //foreach (var item in tskStats)
            //{
            //    Log.Information("{Count} tasks in {Status}", item.Count(), item.Key);
            //}
                           
            Log.Information("{Count} tasks started, waiting for compleetion", taskList1.Count);
            Log.Debug("{Count} tasks started in {Duration} minutes", taskList1.Count, DateTime.Now.Subtract(startTime).TotalMinutes);

            Task.WaitAll(taskList1.ToArray(),-1);
            Task.WaitAll(taskList.ToArray(),-1);

            Log.Information("Execution finished for {Count} tasks with inventory updates in {Duration} minutes", taskList.Count(), DateTime.Now.Subtract(startTime).TotalMinutes);

            //Log.Verbose("Updating {Count} inventory items", inv.Count());

            lc.Dispose();
        }

        public class InventoryRequest : IWMRequest,IWMPostRequest
        {
            private InventoryRequest() { }
            internal InventoryRequest(Authentication authentication) : this()
            {
                _authentication = authentication;
            }
            public InventoryRequest(Authentication authentication, inventory inventory) :this(authentication)
            {
                this.inventory = inventory;
                SetUri();
                SetRequestBody();
                SetWmRequest();
            }
            private Authentication _authentication { get; set; }
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Put;
                }
            }

            public string requestBody { get; set; }

            public Uri requestUri { get; private set; }

            public WMRequest wmRequest { get; set; }
            public inventory inventory { get; set; }

            private void SetRequestBody()
            {
                var seri = new XmlSerializer(typeof(inventory));

                using (var memStr = new MemoryStream())
                {
                    using (var strReader = new StreamReader(memStr))
                    {
                        seri.Serialize(memStr, inventory);
                        memStr.Position = 0;
                        requestBody = strReader.ReadToEnd();
                    }
                }
            }
            private void SetWmRequest()
            {
                wmRequest = new WMRequest(requestUri.AbsoluteUri, _authentication);
            }
            private void SetUri()
            {
                requestUri = new Uri("https://marketplace.walmartapis.com/v2/inventory?sku={0}".FormatWith(inventory.sku));
            }
            public inventory GetResponse()
            {
                return wmRequest.getWMresponse<inventory>(httpMethod, requestBody);
            }
        }

        public class InventoryResponse : inventory, IWMResponse
        {

        }
    }
}
