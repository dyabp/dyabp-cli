using System.Collections.Generic;

namespace Dyabp.Studio.Core
{
    public class StudioConsole
    {
        private readonly IReadOnlyCollection<string> Args;

        public StudioConsole(IReadOnlyCollection<string> args)
        {
            Args = args;
        }
    }
}