using Dyabp.Cli;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dyabp.Studio
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMvcModule),
        typeof(DyabpCliCoreModule)
    )]
    public class DyabpStudioWebModule : AbpModule
    {
        public ILogger<DyabpStudioWebModule> Logger { get; set; }

        public DyabpStudioWebModule()
        {
            Logger = NullLogger<DyabpStudioWebModule>.Instance;
        }

        public override void OnPreApplicationInitialization(ApplicationInitializationContext context)
        {
            Logger = context.ServiceProvider.GetRequiredService<ILogger<DyabpStudioWebModule>>();
            Logger.LogInformation($"Testing logger of {nameof(DyabpStudioWebModule)}");
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            IApplicationBuilder applicationBuilder = ApplicationInitializationContextExtensions.GetApplicationBuilder(context);
            applicationBuilder.UseStaticFiles();
            AbpApplicationBuilderExtensions.UseAbpRequestLocalization(applicationBuilder);
            applicationBuilder.UseRouting();
            applicationBuilder.UseEndpoints(endpoints =>
            {
            });

            AbpAspNetCoreApplicationBuilderExtensions.UseConfiguredEndpoints(applicationBuilder);
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            IConfiguration configuration = ServiceCollectionConfigurationExtensions.GetConfiguration(context.Services);
            ServiceProvider serviceProvider = context.Services.BuildServiceProvider();

            Configure<DyabpStudioOptions>(configuration.GetSection("DyabpStudio"));
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
        }
    }
}
