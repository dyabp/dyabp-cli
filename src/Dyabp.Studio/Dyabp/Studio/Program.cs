using Dyabp.Studio.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.Diagnostics;
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

            LoggerConfiguration loggerConfiguration = ConfigurationLoggerConfigurationExtensions.Configuration(new LoggerConfiguration().MinimumLevel.Debug().MinimumLevel
                .Override("Microsoft", LogEventLevel.Information)
                .Enrich
                .FromLogContext()
                .ReadFrom, configurationRoot);

            Log.Logger = loggerConfiguration.CreateLogger();

            DyabpStudioOptions dyabpStudioOptions = new DyabpStudioOptions();
            configurationRoot.GetSection("DyabpStudio").Bind(dyabpStudioOptions);

            try
            {
                Log.Information("Dyabp studio log test.");
                using IHost host = CreateHostBuilder(args, dyabpStudioOptions.ApplicationUrl, StudioConsts.ContentRootPath).Build();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            BruteForceKillTheApplication();
        }

        internal static IHostBuilder CreateHostBuilder(string[] args, string applicationUrl, string contentRoot)
        {
            return SerilogHostBuilderExtensions.UseSerilog(
                AbpAutofacHostBuilderExtensions.UseAutofac(
                    AbpHostingHostBuilderExtensions.AddAppSettingsSecretsJson(Host.CreateDefaultBuilder(args), true, true, "appsettings.secrets.json")
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>()
                        .UseUrls(applicationUrl)
                        .UseContentRoot(contentRoot);
                    })))
                .UseContentRoot(contentRoot)
                .UseConsoleLifetime(opts =>
                {
                    opts.SuppressStatusMessages = true;
                });
        }

        private static void LogSet(IConfiguration configuration, string logPath, string logName = "studio-log-.txt")
        {
            IConfigurationSection section = configuration.GetSection("Serilog");
            int num = section.GetChildren().Count();
            for (int i = 0; i < num; i++)
            {
            }
        }

        public static void BruteForceKillTheApplication()
        {
            try
            {
                Process.GetCurrentProcess().Kill();
            }
            catch (Exception value)
            {
                Console.WriteLine(value);
            }
        }
    }
}
