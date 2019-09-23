﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hyperscale.Microcore.SharedLogic;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ReportService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Environment.SetEnvironmentVariable("HS_CONFIG_ROOT", Environment.CurrentDirectory);
            CurrentApplicationInfo.Init("TimeTracker-ReportService");

            IConfiguration config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", true, true)
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
              .AddEnvironmentVariables()
              .Build();

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                Task.Factory.StartNew(() => CreateWebHostBuilder(args).Build().Run(), TaskCreationOptions.AttachedToParent)
                    .ContinueWith(task =>
                    {
                        if (task.IsFaulted)
                        {
                            Console.WriteLine(task.Exception.ToString());
                        }
                    });
            }

            var host = new ReportServiceHost(config);
            if (int.TryParse(Environment.GetEnvironmentVariable("PORT"), out int envPort))
            {
                host.Run(new ServiceArguments(basePortOverride: envPort));
            }
            else
            {
                host.Run();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
