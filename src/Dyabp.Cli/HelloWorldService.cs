using System;
using Volo.Abp.DependencyInjection;

namespace Dyabp.Cli
{
    public class HelloWorldService : ITransientDependency
    {
        public void SayHello()
        {
            Console.WriteLine("\tHello World!");
        }
    }
}
