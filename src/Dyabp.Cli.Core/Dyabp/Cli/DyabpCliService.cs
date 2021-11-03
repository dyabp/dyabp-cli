using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Cli;
using Volo.Abp.DependencyInjection;

namespace Dyabp.Cli
{
    public class DyabpCliService : ITransientDependency
    {
        public ILogger<DyabpCliService> Logger { get; set; }
        protected CliService AbpCliService { get; }

        public DyabpCliService(
            Volo.Abp.Cli.CliService abpCliService
)
        {
            AbpCliService = abpCliService;

            Logger = NullLogger<DyabpCliService>.Instance;
        }

        public async Task RunAsync(string[] args)
        {
            Logger.LogInformation("DYABP CLI (https://dyabp.github.io)");

            await AbpCliService.RunAsync(args);
        }
    }
}
