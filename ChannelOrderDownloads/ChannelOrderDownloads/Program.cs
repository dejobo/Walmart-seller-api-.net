using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ChannelOrderDownloads
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new OrderCollector()
            };
            ServiceBase.Run(ServicesToRun);
        }
        public static void ConfigureLogger()
        {
            //var applicationAssembly = Assembly.GetEntryAssembly().GetName();
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Seq("http://srv3:5341", apiKey: "3yTsme0vzQWq50LW5ixB")
                            .MinimumLevel.Verbose()
                            .Enrich.WithEnvironmentUserName()
                            .Enrich.WithMachineName()
                            .Enrich.WithProcessId()
                            .Enrich.WithThreadId()
                            .Enrich.WithProperty("ApplicationName", "WalmartAPITesting")
                            .CreateLogger();
        }
    }
}
