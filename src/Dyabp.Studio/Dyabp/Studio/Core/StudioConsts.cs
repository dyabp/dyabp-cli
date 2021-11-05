using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Dyabp.Studio.Core
{
    public class StudioConsts
    {
        public const string AppSettingsJsonFileName = "appsettings.json";

        public static readonly string DyabpStudioRootPath;

        public static readonly string DyabpStudioLogsPath;

        public static string ContentRootPath;

        public static string LogPath;

        public static string AppSettingsJsonFilePath => Path.Combine(ContentRootPath, AppSettingsJsonFileName);


        public static StudioConsole Console { get; set; }

        public static string GetCurrentVersion()
        {
            return FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        public StudioConsts()
        {
        }

        static StudioConsts()
        {
            DyabpStudioRootPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dyabp", "studio");
            DyabpStudioLogsPath = Path.Combine(DyabpStudioRootPath, "Logs");

        }
    }
}
