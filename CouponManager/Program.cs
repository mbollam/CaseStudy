
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace CouponManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
            //var configuration = new ConfigurationBuilder()
            //    .AddCommandLine(args)
            //    .AddEnvironmentVariables("")
            //    .Build();

            //var url = configuration["ASPNETCORE_URLS"] ?? "http://*:8080";

            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseConfiguration(configuration)
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .UseUrls(url)
            //    //.UseApplicationInsights()
            //    .Build();           

            //host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}
