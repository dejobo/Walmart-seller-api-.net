using Extensions;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WalmartAPI.Classes.Walmart.Orders;

namespace WalmartAPI.Classes
{
    public class ShippingUpdateRequestResponse : IWMRequestResponse
    {
        #region Properties
        public ShippingUpdateRequest request { get; set; }
        public ShippingUpDateResponse response { get; set; }
        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return request;
            }

            set
            {
                request = value as ShippingUpdateRequest;
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
                response = value as ShippingUpDateResponse;
            }
        }
        private Authentication _authentication { get; set; }
        private string _order { get; set; }
        private static ILogger Log { get; set; }

        #endregion

        #region Constructors

        public ShippingUpdateRequestResponse(Authentication authentication, string order) :this(authentication)
        {
            Log = Log.ForContext("order", order);
            _order = order;
            //_authentication = authentication;
            request = new ShippingUpdateRequest(_order, _authentication);
        }
        public ShippingUpdateRequestResponse(Authentication authentication) :this()
        {
            _authentication = authentication;
        }
        private ShippingUpdateRequestResponse()
        {
            Log = Serilog.Log.ForContext<ShippingUpdateRequestResponse>();
        }
        #endregion

        #region Inner Classes
        public class ShippingUpdateRequest : IWMPostRequest
        {
            internal ShippingUpdateRequest(string order,Authentication authentication)
            {
                
                _order = order;
                requestUri = new Uri("https://marketplace.walmartapis.com/v3/orders/{0}/shipping".FormatWith(_order));
                //GetShipmentUpdateRequest();

                Log.Debug("Configuring wmRequest");
                wmRequest = new WMRequest(requestUri.AbsoluteUri, authentication);
                setRequestBody();
            }
            
            private string _order { get; set; }
            private static shipLineStatusType[] GetShipping(WMSystemOrder o, WMSystemShipment s)
            {
                var shipType =  new[]
                {
                        new shipLineStatusType
                        {
                        status = orderLineStatusValueType.Shipped,
                        statusQuantity = new quantityType
                        {
                            amount = o.quantity.ToString(),
                            unitOfMeasurement = "Each"
                        },
                        trackingInfo = new trackingInfoType
                        {
                            carrierName = new carrierNameType
                            {
                            
                            },
                        methodCode = shippingMethodCodeType.Standard,
                        shipDateTime = s.CreatedTime,
                        trackingNumber = s.TrackingNumber
                        }
                    }
                };

                //parse carrier
                carrierType ct;
                try
                {
                    var ctTemp = Enum.Parse(typeof(carrierType), s.Carrier, true);
                    if (ctTemp is carrierType)
                    {
                        ct = (carrierType)ctTemp;
                        shipType[0].trackingInfo.carrierName.Item = ct;
                    }
                    else
                    {
                        shipType[0].trackingInfo.carrierName.Item = s.Carrier;
                    }

                    switch (s.ShippedVia)
                    {
                        case "First":
                        case "PRIORITY":
                            shipType[0].trackingInfo.methodCode = shippingMethodCodeType.Express;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ex.LogWithSerilog();
                }
                return shipType;
            }
            internal orderShipment GetShipmentUpdateRequest()
            {
                var order = _order;

                

                Log.Debug("Creating shippment request for {order}", order);
                using (var db = General.GetContext(l => Debug.WriteLine(l)))
                {
                    var shipping = from o in db.systemOrderSet
                                   join s in db.systemShipmentSet on o.orderNumber equals s.orderNumber
                                   where o.orderNumber == order
                                   select new
                                   {
                                       order = o,
                                       shipment = s,
                                   };
                    var shipList = shipping.ToList();

                    var orderLines = from f in shipList
                                     select new shippingLineType
                                     {
                                         lineNumber = f.order.lineNumber,
                                         orderLineStatuses = GetShipping(f.order, f.shipment)
                                     };
                    Log.Debug("created shipment with {count} lines", orderLines.Count());
                    return new orderShipment { orderLines = orderLines.ToArray() };
                }
            }
            public HttpMethod httpMethod
            {
                get
                {
                    return HttpMethod.Post;
                }
            }

            public Uri requestUri { get; private set; }

            public WMRequest wmRequest { get; set; }

            public string requestBody { get; set; }
            public object XmlSerielizer { get; private set; }

            void setRequestBody()
            {
                var orderShip = GetShipmentUpdateRequest();
                var xSerielize = new XmlSerializer(typeof(orderShipment));

                var nameSpaces = new XmlSerializerNamespaces();
                nameSpaces.Add("ns2", "http://walmart.com/mp/v3/orders");
                nameSpaces.Add("ns3", "http://walmart.com/");

                using (var memStr = new MemoryStream())
                {
                    using (var reader = new StreamReader(memStr))
                    {
                        xSerielize.Serialize(memStr, orderShip,nameSpaces);
                        memStr.Position = 0;
                        requestBody = reader.ReadToEnd();
                    }
                }

            }
            


        }
        public class ShippingUpDateResponse : Order, IWMResponse
        {

        }
        #endregion

        #region Methods
        public void GetReponse()
        {
            var rsp = request.wmRequest.getWMresponse<Order>(request.httpMethod, request.requestBody);
            response = new ShippingUpDateResponse();
            response.InitFromWmType<Order>(rsp);
            Log.Information("{order} updated with tracking {TrackingNumber}", response.purchaseOrderId, request.GetShipmentUpdateRequest().orderLines.First().orderLineStatuses.First().trackingInfo.trackingNumber);
        }

        private void verifyValidStatus(IQueryable<WMSystemShipment> ships)
        {
            Log.Debug("Verifying existing shipments");
            var orToV = ships
                .ToList()
                .Select(s => s.orderNumber)
                .Distinct()
                .ToList();

            var taskList = new List<Task>();
            foreach (var item in orToV)
            {
                var tsk = new Task(() =>
                {
                    orderLineStatusValueType stat;
                    try
                    {
                        stat = new OrdersRequestResponse(_authentication).GetStat(item);
                        Log.Verbose("order {order} status is {status}", item, stat);


                        if (stat == orderLineStatusValueType.Shipped)
                        {
                            var toupdate = ships.Where(o => o.orderNumber == item);

                            foreach (var ord in toupdate)
                            {
                                ord.updated = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.LogWithSerilog();
                    }
                });
                tsk.Start();
                taskList.Add(tsk);
                Task.WaitAll(taskList.ToArray());
            }

        }
        public async void uploadAllShipping(bool verify)
        {
            Log.Debug("starting uploadAllShipping()");
            using (var db = General.GetContext())
            {
                var ships = from f in db.systemShipmentSet
                            where !f.updated
                            select f;

                if (verify)
                {
                    verifyValidStatus(ships);
                    var sr = db.SaveChanges();
                    Log.Debug("saved {count} records", sr);
                }

                var orderShips = ships
                    .Select(s => s.orderNumber)
                    .Distinct()
                    .ToList()
                    .Select(s => new ShippingUpdateRequestResponse(_authentication, s))
                    .ToList();

                orderShips.ForEach(o =>
                {
                    o.GetReponse();
                    o.SetToUpdated();
                    Log.Information("{order} updated");
                });
                

            }
                        

        }

        private async void SetToUpdated()
        {
            using (var db = General.GetContext())
            {
                var set = from f in db.systemShipmentSet
                          where f.orderNumber == _order
                          select f;

                foreach (var item in set)
                {
                    item.updated = true;
                }
                await db.SaveChangesAsync();
            }
        }
        #endregion
    }
}
