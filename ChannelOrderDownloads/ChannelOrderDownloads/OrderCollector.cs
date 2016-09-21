using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Extensions;
using WalmartAPI.Classes;
using System.Collections.Specialized;
using Serilog.Context;

namespace ChannelOrderDownloads
{
    public partial class OrderCollector : ServiceBase
    {
        #region properties
        public string consumerId { get; set; }
        public string channelId { get; set; }
        public string privateKey { get; set; }
        public System.Timers.Timer timer { get; set; }
        public int daysToDownload { get; set; }
        public int timerInterval { get; set; }
        #endregion

        #region Event handling

        protected override void OnStop()
        {
            Log.Information("Order collector service stopped");
            //Log.CloseAndFlush();
            //System.Threading.Thread.Sleep(1000);
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IDisposable s = null;

            Log.Verbose("Timer elapsed on {SignalTime}", e.SignalTime);
            timer.Interval = timerInterval;

            s = LogContext.PushProperty("Method", "getOrders");
            Log.Verbose("Calling getOrders(\"Created\")");
            getOrders("Created");

            Log.Verbose("Calling getOrders(\"Acknowledged\")");
            getOrders("Acknowledged");
            s.Dispose();

            s = LogContext.PushProperty("Method", "acknowladgeOrders");
            Log.Verbose("Calling acknowladgOrders()");
            acknowladgeOrders();
            s.Dispose();

            s = LogContext.PushProperty("Method", "updateShipping");
            Log.Verbose("Calling updateShipping()");
            updateShipping();
            s.Dispose();
        }

        private void updateShipping()
        {
            try
            {
                var ships = new ShippingUpdateRequestResponse(new Authentication(consumerId,privateKey,channelId));
                ships.uploadAllShipping(true);

            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
            }
        }

        protected override void OnStart(string[] args)
        {
            //ConfigureLogger();
            //System.Threading.Thread.Sleep(10000);
            Log.Information("Order collector service is starting");
            Log.Debug("Configuring timer");
            getConfigSettings();
            timer = new System.Timers.Timer();
            timer.Interval = 60000;
            //timer.Interval = timerInterval;
            //timer.Enabled = true;
            timer.Start();
            
            //subscribe to elapsed event
            timer.Elapsed += Timer_Elapsed;

        }

        #endregion

        public OrderCollector()
        {
            InitializeComponent();
        }
        private void getConfigSettings()
        {
            try
            {
                //var config = ConfigurationManager.OpenExeConfiguration("ChannelOrderDownloads.exe").AppSettings;
                consumerId = ConfigurationManager.AppSettings["ConsumerId"];
                channelId = ConfigurationManager.AppSettings["channelType"];

                var privateKeyFile = ConfigurationManager.AppSettings["privateKey"];
                using (var stream = new StreamReader(privateKeyFile))
                {
                    privateKey = stream.ReadToEnd();
                }
                daysToDownload = int.Parse(ConfigurationManager.AppSettings["DaysToSearch"]);
                timerInterval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]);

                Log.Debug("started service with {consumerId} {channelId} {privateKey}", consumerId, channelId, privateKey);
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
            }
        }
        private void getOrders(string orderStatus)
        {
            try
            {
                //create ordersrequestresponse object
                var orders = new OrdersRequestResponse(new Authentication(consumerId, privateKey, channelId));

                //add required query strings
                var qs = new NameValueCollection();
                qs.Add("createdStartDate", DateTime.Now.AddDays(-daysToDownload).Date.ToString("yyyy-MM-dd"));
                qs.Add("status", orderStatus);
                orders.request.wmRequest.appendQueryStrings(qs);
                orders.getResponse();

                Log.Debug("Received {count} orders in {orderStatus} status", orders.response.elements.Count(), orderStatus);

                while (orders.nextCursor != null)
                {
                    orders = orders.nextCursor;
                    orders.getResponse();
                    Log.Debug("Received {count} orders in {orderStatus} status", orders.response.elements.Count(), orderStatus);
                }
            }catch(Exception ex)
            {
                ex.LogWithSerilog();
            }
        }
        private void acknowladgeOrders()
        {
            try
            {

                var ack = new PostOrderAcknowladgements(new Authentication(consumerId, privateKey, channelId));
                ack.AcknowladgeImportedOrders();
            }
            catch (Exception ex)
            {
                ex.LogWithSerilog();
            }
        }

    }
}
