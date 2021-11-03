using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Cli;
using Volo.Abp.Modularity;

namespace Dyabp.Cli
{
    [DependsOn(typeof(AbpCliCoreModule))]
    public class DyabpCliCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
