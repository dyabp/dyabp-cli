using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Dyabp.Studio
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceCollectionApplicationExtensions.AddApplication<DyabpStudioWebModule>(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory,
            IHostApplicationLifetime appLifetime, IOptionsSnapshot<DyabpStudioOptions> studioOptions)
        {
            AbpApplicationBuilderExtensions.InitializeApplication(app);
            appLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("Opening" + studioOptions.Value.ApplicationUrl);
                Run(studioOptions.Value.ApplicationUrl);
            });
            appLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("Application Stopped.");
            });
        }

        private static void Run(string applicationUrl)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {applicationUrl}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", applicationUrl);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", applicationUrl);
            }
        }
    }
}