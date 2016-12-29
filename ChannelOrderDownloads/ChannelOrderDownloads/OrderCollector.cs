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
using System.Reflection;

namespace ChannelOrderDownloads
{
    public partial class OrderCollector : ServiceBase
    {
        #region properties
        public string consumerId { get; set; }
        public string channelId { get; set; }
        public string privateKey { get; set; }
        public System.Timers.Timer timer { get; set; }
        public System.Timers.Timer inventoryTimer { get; set; }
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
        protected override void OnShutdown()
        {
            Log.Warning("service was shutdown");
            Thread.Sleep(5000);
            base.OnShutdown();
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                IDisposable s = null;

                Log.Verbose("Timer elapsed on {SignalTime}", e.SignalTime);
                timer.Interval = timerInterval;

                s = LogContext.PushProperty("Method", "getOrders(\"Created\")");
                Log.Information("Calling {Method}");
                await getOrders("Created");
                s.Dispose();

                //s = LogContext.PushProperty("Method", "getOrders(\"Acknowledged\")");
                //Log.Information("Calling {Method}");
                //await getOrders("Acknowledged");
                //s.Dispose();

                s = LogContext.PushProperty("Method", "acknowladgeOrders");
                Log.Information("Calling {Method}");
                await acknowladgeOrders();
                s.Dispose();

                s = LogContext.PushProperty("Method", "updateShipping");
                Log.Information("Calling {Method}");
                await updateShipping();
                s.Dispose();

                await Task.Run(() =>
                 {
                     using (LogContext.PushProperty("Method", "CancelOrders()"))
                     {
                         Log.Information("Calling {Method}");
                         var cnc = new OrderCancelationRequestResponse(new Authentication(consumerId, privateKey, channelId));
                         cnc.CancelOrders();
                     }
                 });

            }
            catch (Exception ex)
            {
                throw ex.LogWithSerilog();
            }
            finally
            {
                timer.Start();
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
            inventoryTimer = new System.Timers.Timer(500);
            inventoryTimer.Start();

            //timer.Interval = timerInterval;
            //timer.Enabled = true;
            timer.Start();


            

            //subscribe to elapsed event
            timer.Elapsed += Timer_Elapsed;
            inventoryTimer.Elapsed += InventoryTimer_Elapsed;
        }

        private void InventoryTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            inventoryTimer.Stop();
            Log.Verbose("Timer elapsed on {SignalTime}", e.SignalTime);
            //inventoryTimer.Stop();
            inventoryTimer.Interval = 3600000;
            //inventoryTimer.Start();
            using (LogContext.PushProperty("Method", "UpdateAllInventory"))
            {
                try
                {
                    Log.Information("Calling UpdateAllInventory()");

                    var auth = new Authentication(consumerId, privateKey, channelId);
                    var feedReq = new InventoryFeedRequestResponse(auth);
                    feedReq.UpdateAllInventory();

                    //var sut = new InventoryRequestResponse(new Authentication(consumerId, privateKey, channelId));
                    //sut.UpdateAllInventory();
                }
                catch (Exception ex)
                {
                    ex.LogWithSerilog();
                }
                finally
                {
                    inventoryTimer.Start();
                }
            }
        }

        #endregion

        private async Task updateShipping()
        {
            await Task.Run(() =>
            {
                try
                {
                    var ships = new ShippingUpdateRequestResponse(new Authentication(consumerId, privateKey, channelId));
                    ships.uploadAllShipping(true);

                }
                catch (Exception ex)
                {
                    ex.LogWithSerilog();
                }
            });
        }
        public OrderCollector()
        {
            InitializeComponent();
        }
        private void getConfigSettings()
        {
            IDisposable s = null;
            try
            {
                s = LogContext.PushProperty("Method", "getConfigSettings");
                var assembly = Assembly.GetEntryAssembly();
                var currentPath = Path.GetDirectoryName(assembly.Location);

                //privateKey = Encoding.UTF8.GetString(Resources.PrivateKey);
                //Log.Debug("Got {privateKey}", privateKey);
                //var config = ConfigurationManager.OpenExeConfiguration("ChannelOrderDownloads.exe").AppSettings;
                consumerId = ConfigurationManager.AppSettings["ConsumerId"];
                channelId = ConfigurationManager.AppSettings["channelType"];

                var privateKeyFile = string.Format(@"{0}\{1}", currentPath, ConfigurationManager.AppSettings["privateKey"]);
                Log.Debug("Key file is at {privateKeyFile}", privateKeyFile);
                using (var stream = new StreamReader(privateKeyFile))
                {
                    privateKey = stream.ReadToEnd();
                    Log.Debug("Got {privateKey} from {file}", privateKey,privateKeyFile);
                }
                daysToDownload = int.Parse(ConfigurationManager.AppSettings["DaysToSearch"]);
                timerInterval = int.Parse(ConfigurationManager.AppSettings["TimerInterval"]);

                Log.Debug("started service with {consumerId} {channelId} {privateKey}", consumerId, channelId, privateKey);
            }
            catch(Exception ex)
            {
                ex.LogWithSerilog();
            }
            finally
            {
                s.Dispose();
            }
        }
        private async Task getOrders(string orderStatus)
        {
            await Task.Run(() =>
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

                    if (orders.response == null)
                        return;

                    Log.Debug("Received {count} orders in {orderStatus} status", orders.response.elements.Count(), orderStatus);

                    while (orders.nextCursor != null)
                    {
                        orders = orders.nextCursor;
                        orders.getResponse();
                        Log.Debug("Received {count} orders in {orderStatus} status", orders.response.elements.Count(), orderStatus);
                    }
                }
                catch (Exception ex)
                {
                    ex.LogWithSerilog();
                }
            });
        }
        private async Task acknowladgeOrders()
        {
            await Task.Run(() =>
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
            });
        }

    }
}
