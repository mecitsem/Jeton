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
        private const string _appSettingsPrefix = "jeton";

        public static string GetPassPhrase()
        {
            var passPhrase = GetAppSettingsValue(Constants.AppSettings.PassPhrase);

            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentNullException("passPhrase is null");

            return passPhrase;
        }

        /// <summary>
        /// Key Format [prefix]:[keyName] For exp: <add key="jeton:PassPhrase" value="" />
        /// </summary>
        /// <param name="appSettingsKey"></param>
        /// <returns></returns>
        public static string GetAppSettingsValue(Constants.AppSettings appSettingsKey)
        {
            return ConfigurationManager.AppSettings[string.Format("{0}:{1}", _appSettingsPrefix, appSettingsKey.ToString())];
        }
    }
}
