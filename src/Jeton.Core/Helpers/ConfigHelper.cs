using Jeton.Core.Common;
using System;
using System.Configuration;

namespace Jeton.Core.Helpers
{
    public class ConfigHelper
    {
        /// <summary>
        /// Key Format [prefix]:[keyName] For exp: <add key="jeton:PassPhrase" value="" />
        /// </summary>
        /// <param name="appSettingsKey"></param>
        /// <returns></returns>
        public static string GetAppSettingsValue(Constants.AppSettings appSettingsKey)
        {
            return ConfigurationManager.AppSettings[$"{Constants.Application.ToLowerInvariant()}:{appSettingsKey}"];
        }
    }
}
