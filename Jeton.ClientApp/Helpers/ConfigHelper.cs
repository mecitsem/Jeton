using System.Configuration;

namespace Jeton.ClientApp.Helpers
{
    public class ConfigHelper
    {
        public static string GetAppSettingsValue(string appSettingsKey)
        {
            return ConfigurationManager.AppSettings[$"jwt:{appSettingsKey}"];
        }
    }
}