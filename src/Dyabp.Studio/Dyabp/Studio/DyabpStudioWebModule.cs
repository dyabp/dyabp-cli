using Dyabp.Cli;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Dyabp.Studio
{

    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(DyabpCliCoreModule)
    )]
    public class DyabpStudioWebModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

        }
    }
}
