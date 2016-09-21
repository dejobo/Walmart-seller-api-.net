using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Extensions;

namespace ChannelOrderDownloads
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeRush", "Can combine initialization with declaration")]
        static void Main()
        {
            ConfigureLogger();
            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new OrderCollector()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, ex.GetBottomException().Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        static void ConfigureLogger()
        {
            //var applicationAssembly = Assembly.GetEntryAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Seq("http://srv3:5341", apiKey: "3yTsme0vzQWq50LW5ixB")
                            .MinimumLevel.Verbose()
                            .Enrich.FromLogContext()
                            .Enrich.WithEnvironmentUserName()
                            .Enrich.WithMachineName()
                            .Enrich.WithProcessId()
                            .Enrich.WithThreadId()
                            .Enrich.WithProperty("ApplicationName", "Order collector service")
                            .CreateLogger();
            Log.Debug("Logger configured");
        }
    }
}
