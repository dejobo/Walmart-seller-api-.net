using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Extensions;
using System.Net;
using WalmartAPI.Classes.Walmart.Orders;
using System.Collections.Specialized;
using Serilog;

namespace WalmartAPI.Classes
{
    public class PostOrderAcknowladgements : IWMRequestResponse
    {
        private Authentication _authentication { get; set; }
        public PostOrderAcknowladgements(Authentication authentication)
        {
            this._authentication = authentication;
        }
        public void AcknowladgeImportedOrders()
        {
            var taskList = new List<Task>();
            using (var db = General.GetContext(l => Log.Verbose("executing {sql}", l)))
            {
                var set = from orders in db.systemOrderSet
                          where orders.isImported && orders.orderLineStatus == "Created"
                          select orders.orderNumber;

                //select distinct for acknowladgement
                if (set.Count() > 0)
                {
                    set.Distinct()
                    .ForEach(o =>
                    {
                        var tsk = new Task(() =>
                        {
                            if (acknowladgeOrder(o, true))
                            {
                                //if successfull set the order to acknowladged
                                var co = from oo
                                         in db.systemOrderSet
                                         where oo.orderNumber == o
                                         select oo;

                                co.ForEach(ord => ord.orderLineStatus = orderLineStatusValueType.Acknowledged.ToString());
                            }
                        });
                        tsk.Start();
                        taskList.Add(tsk);
                    });
                    Log.Debug("starting the wait for {taskCount} tasks", taskList.Count());
                    Task.WaitAll(taskList.ToArray());
                    Log.Debug("all tasks are done");
                    db.SaveChanges();
                }
                else
                {
                    Log.Warning("No orders to acknowladge");
                }
            }
        }
        private bool acknowladgeOrder(string order,bool verify)
        {
            if (verify)
            {
                var stat = new OrdersRequestResponse(_authentication).GetStat(order);
                switch (stat)
                {
                    case orderLineStatusValueType.Created:
                        return acknowladgeOrder(order);
                    case orderLineStatusValueType.Acknowledged:
                        return true;
                    default:
                        return false;
                }
            }
            return acknowladgeOrder(order);
        }
        private bool acknowladgeOrder(string order)
        {
            try
            {
                Log.Debug("acknowladging order {order}", order);
                var req = new AcknowladgeOrderRequest(_authentication, order);

                req.wmRequest.getWMresponse<Order>(req.httpMethod);
                return true;
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
                return false;
            }
        }
        public class AcknowladgeOrderRequest : IWMRequest
        {
            public AcknowladgeOrderRequest(Authentication authentication, string order)
            {
                requestUri = new Uri("https://marketplace.walmartapis.com/v3/orders/{0}/acknowledge".FormatWith(order));

                wmRequest = new WMRequest(requestUri.AbsoluteUri, new Authentication(authentication.consumerId,requestUri,authentication.privateKey,this.httpMethod,authentication.channelType));
            }

            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Post;
                }
            }

            public Uri requestUri { get; set; }

            public WMRequest wmRequest { get; set; }
        }

        public IWMRequest request
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

        public IWMResponse response
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
    }
}
