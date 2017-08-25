using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Jeton.RootApp.Helpers
{
    public class ConfigHelper
    {
        public static string GetAppSettingsValue(string appSettingsKey)
        {
            return ConfigurationManager.AppSettings[$"jeton:{appSettingsKey}"];
        }
    }
}