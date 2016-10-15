using Jeton.Core.Common;
using System;
using System.Configuration;

namespace Jeton.Core.Helpers
{
    public class ConfigHelper
    {
        private const string AppSettingsPrefix = "jeton";

        public static string GetPassPhrase()
        {
            var passPhrase = GetAppSettingsValue(Constants.AppSettings.PassPhrase);

            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentException("passPhrase is null. Please check config file.");

            return passPhrase;
        }

        /// <summary>
        /// Key Format [prefix]:[keyName] For exp: <add key="jeton:PassPhrase" value="" />
        /// </summary>
        /// <param name="appSettingsKey"></param>
        /// <returns></returns>
        public static string GetAppSettingsValue(Constants.AppSettings appSettingsKey)
        {
            return ConfigurationManager.AppSettings[$"{AppSettingsPrefix}:{appSettingsKey}"];
        }
    }
}
