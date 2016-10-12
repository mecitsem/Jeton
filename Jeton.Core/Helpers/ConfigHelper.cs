using Jeton.Core.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Helpers
{
    public class ConfigHelper
    {
        public static string GetPassPhrase()
        {
            var passPhrase = GetAppSettingsValue(Constants.AppSettings.PassPhrase);

            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentNullException("passPhrase is null");

            return passPhrase;
        }

        public static string GetAppSettingsValue(Constants.AppSettings appSettingsKey)
        {
            return ConfigurationManager.AppSettings[appSettingsKey.ToString()];
        }
    }
}
