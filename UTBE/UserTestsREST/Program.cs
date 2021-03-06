using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace UserTestsREST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //configure logging first
            ConfigureLogging();

            //then create the host, so that if the host fails we can log errors
            CreateHost(args);

            //Setup Serilog
            // Log.Logger = new LoggerConfiguration()
            // .Enrich.FromLogContext()
            // .WriteTo.Console()
            // .CreateLogger();

            // try
            // {
            //     CreateHostBuilder(args).Build().Run();
            //     Log.Information("Started web host");
            // }
            // catch (Exception ex)
            // {
            //     Log.Fatal(ex, "Host terminated unexpectedly");
            // }
        }

        private static void ConfigureLogging()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: false)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        private static void CreateHost(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (System.Exception ex)
            {
                Log.Fatal($"Failed to start {Assembly.GetExecutingAssembly().GetName().Name}", ex);
                Log.Information(ex.ToString());
                throw;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureAppConfiguration(configuration =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                configuration.AddJsonFile("secrets/appsettings.json", optional: true, reloadOnChange: true);
                configuration.AddJsonFile(
                    $"appsettings.{environment}.json",
                    optional: false);
            })
            .UseSerilog();
    }
}
