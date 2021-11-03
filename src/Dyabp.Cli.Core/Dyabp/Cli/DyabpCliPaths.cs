using System;
using System.IO;
using System.Text;

namespace Dyabp.Cli
{
    public static class DyabpCliPaths
    {
        public static string TemplateCache => Path.Combine(DyabpRootPath, "templates"); //TODO: Move somewhere else?
        public static string Log => Path.Combine(DyabpRootPath, "cli", "logs");
        public static string Root => Path.Combine(DyabpRootPath, "cli");
        public static string AccessToken => Path.Combine(DyabpRootPath, "cli", "access-token.bin");
        public static string Build => Path.Combine(DyabpRootPath, "build");
        public static string Lic => Path.Combine(Path.GetTempPath(), Encoding.ASCII.GetString(new byte[] { 65, 98, 112, 76, 105, 99, 101, 110, 115, 101, 46, 98, 105, 110 }));

        private static readonly string DyabpRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dyabp");
    }
}
