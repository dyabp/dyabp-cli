using Dyabp.Studio.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dyabp.Studio
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            StudioConsts.Console = new StudioConsole(args);
            Console.WriteLine($"Starting Dyabp Studio {StudioConsts.GetCurrentVersion()}...");
            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
            };

            StudioConsts.ContentRootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            StudioConsts.LogPath = StudioConsts.DyabpStudioLogsPath;
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(StudioConsts.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .Build();

            LogSet(configurationRoot, StudioConsts.LogPath, "studio-log-.txt");

//            Log.Logger = new LoggerConfiguration()
//#if DEBUG
//                .MinimumLevel.Debug()
//#else
//                .MinimumLevel.Information()
//#endif
//                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
//                .Enrich.FromLogContext()
//                .WriteTo.Async(c => c.File("Logs/logs.txt"))
//                .WriteTo.Async(c => c.Console())
//                .CreateLogger();

            try
            {
                //Log.Information("Starting console host.");
                await CreateHostBuilder(args).RunConsoleAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseAutofac()
                .UseSerilog()
                .ConfigureAppConfiguration((context, config) =>
                {
                    //setup your additional configuration sources
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddApplication<DyabpStudioWebModule>();
                });

        private static void LogSet(IConfiguration configuration, string logPath, string logName = "studio-log-.txt")
        {
            IConfigurationSection section = configuration.GetSection("Serilog");
            int num = section.GetChildren().Count();
            for (int i = 0; i < num; i++)
            {
            }
        }
    }
}
