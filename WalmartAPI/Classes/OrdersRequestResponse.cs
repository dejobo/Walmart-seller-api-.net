﻿using Extensions;
using MoreLinq;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using WalmartAPI.Classes.Walmart.Orders;

namespace WalmartAPI.Classes
{
    public class OrdersRequestResponse : IWMRequestResponse
    {
        #region Constructors
        private OrdersRequestResponse()
        {
        }
        public OrdersRequestResponse(Authentication authentication) : this()
        {
            Log = Serilog.Log.ForContext<OrdersRequestResponse>();

            _authentication = authentication;
            this.request = new GetOrdersRequest(_authentication, new Uri("https://marketplace.walmartapis.com/v3/orders"));
        }
        #endregion
        #region Properties
        private Authentication _authentication { get; set; }
        public GetOrdersRequest request { get; set; }
        public GetOrdersResponse response { get; set; }
        public OrdersRequestResponse nextCursor { get; set; }
        IWMRequest IWMRequestResponse.request
        {
            get
            {
                return this.request;
            }

            set
            {
                this.request = value as GetOrdersRequest;
            }
        }
        IWMResponse IWMRequestResponse.response
        {
            get
            {
                return this.response;
            }

            set
            {
                this.response = value as GetOrdersResponse;
            }
        }
        public static ILogger Log { get; set; }
        #endregion

        #region Inner classes
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

            public GetOrdersRequest(Authentication authentication, Uri requestUri)
            {
                this.requestUri = requestUri;
                //new Uri("https://marketplace.walmartapis.com/v3/orders");

                Log.Verbose("Creating authorization for orders request");
                //create auth
                var auth = new Authentication(authentication.consumerId, requestUri, authentication.privateKey, HttpMethod.Get, authentication.channelType);
                //Init wmRequest
                Log.Verbose("Initiating wmRequest prop");
                wmRequest = new WMRequest(requestUri.AbsoluteUri, auth);
            }

        }
        public class GetOrdersResponse : ordersListType, IWMResponse
        {

        }
        #endregion

        public orderLineStatusValueType GetStat(string order)
        {
            using (LogContext.PushProperty("MethodSignature", "orderLineStatusValueType GetStat(string)"))
            {
                Log.Debug("Checking status on {order}", order);
                var orderReq = new OrdersRequestResponse(_authentication);
                var queryStrings = new NameValueCollection();
                queryStrings.Add("purchaseOrderId", order);
                orderReq.request.wmRequest.appendQueryStrings(queryStrings);
                orderReq.getResponse();
                var stat = orderReq.response.elements.Single().orderLines.First().orderLineStatuses.First().status;

                Log.Debug("{order} status is {status}", order, stat);
                return stat;
            }
        }
        public void getResponse()
        {
            try
            {
                //get response from walmart
                var resp = request.wmRequest.getWMresponse<ordersListType>();
                if (resp == null)
                {
                    Log.Debug("Request didn't return any results, see previous messages for more info");
                    response = null;
                    return;
                }
                response = new OrdersRequestResponse.GetOrdersResponse();
                //map response to GetOrdersResponse type
                response.InitFromWmType<ordersListType>(resp);

                var ordersList = new List<WMSystemOrder>();
                foreach (var item in response.elements)
                {
                    var order = createSystemOrder(item);
                    ordersList.AddRange(order);
                }

                using (var db = General.GetContext())
                {

                    if (db.systemOrderSet.Count() == 0)
                    {
                        db.systemOrderSet.AddRange(ordersList);
                    }
                    else
                    {
                        //filter lines already in the database
                        var filteredlist = ordersList
                        .ExceptBy(db.systemOrderSet,
                        s => new { s.orderNumber, s.lineNumber });
                        //add items to table
                        db.systemOrderSet.AddRange(filteredlist);
                    }
                    var cnt = db.SaveChanges();
                    Log.Verbose("Saved {count} records to the WMSystemOrders table", cnt);
                }

                //check if next cursor was provided.
                if (!response.meta.nextCursor.IsNullOrEmpty())
                {
                    var uriNext = new UriBuilder(request.requestUri)
                    {
                        Query = response.meta.nextCursor.TrimStart('?')
                    };

                    //create new OrdersRequestResponse
                    var newReq = new OrdersRequestResponse(_authentication);
                    newReq.request.wmRequest.SetRequest(uriNext.Uri.AbsoluteUri);
                    //set the next cursor
                    nextCursor = newReq;
                }
                else
                {
                    nextCursor = null;
                }
            }
            catch(WebException wex)
            {
                //Log.Verbose(wex, "Web exception already handled lower in the stack");
                throw;
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
                throw;
            }
        }

        private List<WMSystemOrder> createSystemOrder(Order item)
        {
            Log.Debug("creating a systemorder object for {orderNumber} with {lines} lines", item.purchaseOrderId, item.orderLines.Count());
            var orders = new List<WMSystemOrder>();
            foreach (var orderItems in item.orderLines)
            {
                var order = new WMSystemOrder()
                {
                    orderNumber = item.purchaseOrderId,
                    customerPurchaseOrder = item.customerOrderId,
                    orderDate = item.orderDate,
                    estimatedShipDate = item.shippingInfo.estimatedShipDate,
                    estimatedDeliveryDate = item.shippingInfo.estimatedDeliveryDate,
                    shippingMethod = item.shippingInfo.methodCode.ToString(),
                    customerName = item.shippingInfo.postalAddress.name,
                    customerAddress1 = item.shippingInfo.postalAddress.address1,
                    customerAddress2 = item.shippingInfo.postalAddress.address2,
                    customerCity = item.shippingInfo.postalAddress.city,
                    customerState = item.shippingInfo.postalAddress.state,
                    customerPostalCode = item.shippingInfo.postalAddress.postalCode,
                    customerCountry = item.shippingInfo.postalAddress.country,
                    customerAddressType = item.shippingInfo.postalAddress.addressType,
                    customerPhoneNumber = item.shippingInfo.phone,
                    customerEmail = item.customerEmailId,
                    lineNumber = orderItems.lineNumber,
                    sku = orderItems.item.sku,
                    dateAdded = DateTime.Now
                };

                var itemCharges = orderItems.charges.Single(c => c.chargeName == "ItemPrice");
                order.itemPrice = itemCharges.chargeAmount.amount;
                if (itemCharges.tax != null)
                    order.itemTax = ((decimal?)itemCharges.tax.taxAmount.amount) ?? 0;

                var shippingCharges = orderItems.charges.SingleOrDefault(c => c.chargeName == "Shipping");
                if (shippingCharges != null)
                {
                    order.shippingPrice = ((decimal?)shippingCharges.chargeAmount.amount) ?? 0;
                    if (shippingCharges.tax != null)
                        order.shippingTax = ((decimal?)shippingCharges.tax.taxAmount.amount) ?? 0;
                }
                order.quantity = int.Parse(orderItems.orderLineQuantity.amount);
                order.orderLineStatus = orderItems.orderLineStatuses.First().status.ToString();
                order.lineTotal = order.itemPrice + order.itemTax + order.shippingPrice + order.shippingTax;

                //add to orders collection
                orders.Add(order);

            }
            //calculate total
            var sum = orders.Sum(p => p.itemPrice * p.quantity);
            //add it to the details
            orders = orders.Pipe(o => o.orderTotal = sum).ToList();

            Log.Debug("systemOrder object created for {orderNumber} with {lines} lines", item.purchaseOrderId, item.orderLines.Count());
            return orders;
        }
    }
}
