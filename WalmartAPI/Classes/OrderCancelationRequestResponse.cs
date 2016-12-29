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
using WalmartAPI.Classes.Walmart.Orders;
using MoreLinq;

namespace WalmartAPI.Classes
{
    public class OrderCancelationRequestResponse : IWMRequestResponse, IDisposable
    {
        #region Properties
        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as OrderCancelationRequest;
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
                response = value as OrderCancelationResponse;
            }
        }

        public OrderCancelationRequest request { get; set; }
        public OrderCancelationResponse response { get; set; }

        private Authentication _authentication { get; set; }

        private List<IDisposable> _disposeBucket { get; set; }
        #endregion

        private OrderCancelationRequestResponse()
        {

        }

        public OrderCancelationRequestResponse(Authentication authentication) :this()
        {
            _authentication = authentication;
        }

        public void CancelOrders()
        {
            CancelOrders("");
        }
        public void CancelOrders(string Order)
        {
            CancelOrders(Order, true);
        }
        public void CancelOrders(string Order,bool checkExisting)
        {
            using (LogContext.PushProperty("MethodSignature","CancelOrders()"))
            {
                try
                {
                    var orderSpecified = !Order.IsNullOrEmpty();
                    using (var db = General.GetContext())
                    {
                        Log.Verbose("Getting list of orders to cancel on walmart.com");
                        var cancelationList = (from or in db.systemCancellationSet
                                               where !orderSpecified || or.orderNumber == Order
                                              select new
                                              {
                                                  orderNumber = or.orderNumber,
                                                  //reason = or.reasonForCancellation,
                                                  orderLines = db.systemOrderSet.Where(s => s.orderNumber == or.orderNumber),
                                              }).ToList();

                        Log.Verbose("there's {Count} order (lines) pending cancellation", cancelationList.Count());

                        //extract and construct the cancellation type
                        var cancellationRequestList = cancelationList
                                                      .Select(cr => new
                                                      {
                                                          cr.orderNumber,
                                                          orderLineType = GetCancelLineType(cr.orderLines),
                                                          skip = false,
                                                      })
                                                      .ToList();

                        if (checkExisting)
                        {
                            Log.Verbose("Checking Orders if it's not yet canceled");

                            var canceledOrders = new List<string>();
                            cancellationRequestList.ForEach(item =>
                            {
                                var stat = new OrdersRequestResponse(_authentication).GetStat(item.orderNumber);
                                if (stat == orderLineStatusValueType.Cancelled 
                                || stat == orderLineStatusValueType.Shipped)
                                {

                                    Log.Verbose("Order {Order} is alrady {Status}, skipping...", item.orderNumber,stat);
                                    //cancellationRequestList.Remove(item);
                                    canceledOrders.Add(item.orderNumber);
                                    //item.skip = true;
                                    setOrderToCanceled(item.orderNumber);
                                }
                            });

                            foreach (var item in canceledOrders)
                            {
                                //setOrderToCanceled(item);
                                var i = cancellationRequestList.Single(c => c.orderNumber == item);
                                cancellationRequestList.Remove(i);

                            }
                            if(cancellationRequestList.Count()< 1)
                            {
                                Log.Debug("No more cancellation records to proccess");
                                return;
                            }
                        }                           
                        //create the web requests
                        var webRequestList = cancellationRequestList
                            .Select(r => new OrderCancelationRequestResponse(_authentication)
                            {
                                request = new OrderCancelationRequest(_authentication, r.orderNumber, new orderCancellation { orderLines = r.orderLineType })
                            });

                        //proccess the webRequests

                        webRequestList.ForEach(w =>
                        {
                            try
                            {
                                w.response = new OrderCancelationResponse();
                                w.response = w.request.GetResponse();

                                Log.Information("Walmart {Order} is set to {Status} on walmart.com", w.response.purchaseOrderId, w.response.orderLines.FirstOrDefault().orderLineStatuses.FirstOrDefault());

                                setOrderToCanceled(w.request.order);
                            }
                            catch(Exception ex)
                            {
                                ex.LogWithSerilog();
                            }
                        });
                                                      
                    }

                }
                catch (Exception ex)
                {
                    throw ex.LogWithSerilog();
                }
            }
        }

        private void setOrderToCanceled(string orderNumber)
        {
            using (LogContext.PushProperty("MethodSignature","setOrderToCanceled(string)"))
            {
                try
                {
                    using(var db = General.GetContext())
                    {
                        var orderLines = db.systemOrderSet
                            .Where(o => o.orderNumber == orderNumber);

                        orderLines.ForEach(o => o.orderLineStatus = "Cancelled");
                        var c = db.SaveChanges();

                        if (c > 0)
                        {
                            Log.Information("{Order} set to cancelled",orderNumber);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex.LogWithSerilog();
                }
            }
        }

        cancelLineType[] GetCancelLineType(IQueryable<WMSystemOrder> OrderList)
        {
            using (LogContext.PushProperty("MethodSignature", "GetCancelLineType(IQueryable<WMSystemOrder>"))
            {
                try
                {
                    var cancelLines = OrderList.ToList().Select(o => new cancelLineType
                    {
                        lineNumber = o.lineNumber,
                        orderLineStatuses = new[] {
                        new cancelLineStatusType {
                            status = orderLineStatusValueType.Cancelled,
                            statusQuantity = new quantityType
                            {
                                amount =  o.quantity.ToString(),
                                unitOfMeasurement = "EACH"
                            },
                            cancellationReason = cancellationReasonType.CANCEL_BY_SELLER,
                        }
                    }
                    }).ToArray();
                    return cancelLines;
                }
                catch (Exception ex)
                {
                    throw ex.LogWithSerilog();
                }
            }
        }

        #region Inner Classes
        public class OrderCancelationRequest : IWMPostRequest
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

            private Authentication _authentication { get; set; }

            public string order { get; set; }
            private OrderCancelationRequest()
            {

            }
            internal OrderCancelationRequest(Authentication authentication,string order, orderCancellation orderCancelation) : this()
            {
                _authentication = authentication;
                setRequestBody(orderCancelation);
                setRequestUri(order);
                setWmRequest();
            }

            private void setWmRequest()
            {
                wmRequest = new WMRequest(requestUri.AbsoluteUri, _authentication);
            }

            internal OrderCancelationResponse GetResponse()
            {
                return wmRequest.getWMresponse<Order>(httpMethod, requestBody) as OrderCancelationResponse;
            }

            private void setRequestUri(string order)
            {
                requestUri = new Uri("https://marketplace.walmartapis.com/v3/orders/{0}/cancel".FormatWith(order));
            }

            private void setRequestBody(orderCancellation orderCancellation)
            {
                using (LogContext.PushProperty("MethodSignature", "setRequestBody(orderCancellation)"))
                {
                    try
                    {
                        Log.Verbose("Creating xml serielizer");
                        var xserial = new XmlSerializer(typeof(orderCancellation));

                        using (var memStr = new MemoryStream())
                        {
                            using (var reader = new StreamReader(memStr))
                            {
                                xserial.Serialize(memStr, orderCancellation);
                                memStr.Position = 0;
                                requestBody = reader.ReadToEnd();
                                Log.Debug("Finished serielization for cancelation");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex.LogWithSerilog();
                    }
                }
            }
        }

        public class OrderCancelationResponse : Order, IWMResponse
        {

        }

        #endregion

        public void Dispose()
        {
            _disposeBucket.ForEach(d => d.Dispose());
        }
    }
}
