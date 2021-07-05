using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Demo.ApplicationProcess.Api
{
    public class Program
    {

        public static void Main(string[] args)
        { 
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter("Microsoft", LogLevel.Information);
                    logging.AddFilter("System", LogLevel.Error);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((hst, cnf) =>
                    {
                        cnf.ReadFrom.Configuration(hst.Configuration);
                        cnf.Enrich.FromLogContext();
                        cnf.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information);
                        cnf.MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning);
                        cnf.Enrich.WithProperty("ApplicationName", hst.HostingEnvironment.ApplicationName);
                        cnf.WriteTo.Console();
                        cnf.WriteTo.File("Logs/api.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
