using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace PlagiarismChecker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //public static IWebHostBuilder CreateHostBuilder(string[] args)
        //{
        //    var config = new ConfigurationBuilder()
        //        .SetBasePath(Directory.GetCurrentDirectory())
        //        .AddJsonFile("appsettings.json", optional: true)
        //        .AddCommandLine(args)
        //        .Build();

        //    return WebHost.CreateDefaultBuilder(args)
        //        .UseConfiguration(config)
        //        .UseStartup<Startup>();
        //}

    }
}
