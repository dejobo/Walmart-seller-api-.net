﻿using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using System.Reflection;

namespace ChannelOrderDownloads
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
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
                System.Threading.Thread.Sleep(2500);
                Log.CloseAndFlush();
            }
        }
        internal static void ConfigureLogger()
        {
            var applicationAssembly = Assembly.GetEntryAssembly();//.GetName();
            
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Seq("http://srv3:5341", apiKey: "3yTsme0vzQWq50LW5ixB")
                            .WriteTo.EventLog("Order collector service",restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
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
