using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Core.Helpers;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
{
    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        #region Ctor
        public SettingRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
            
        }
        #endregion

        #region INSERT
        #endregion

        #region UPDATE
        #endregion

        #region DELETE
        #endregion

        #region SELECT
        /// <summary>
        /// Get setting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Setting GetSettingByName(string name)
        {
            return Table.FirstOrDefault(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Get setting by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Setting> GetSettingByNameAsync(string name)
        {
            return await Table.FirstOrDefaultAsync(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Get secretKey setting value
        /// </summary>
        /// <returns></returns>
        public string GetSecretKey()
        {
            return Table.FirstOrDefault(s => s.Name.Equals(Constants.Settings.SecretKey, StringComparison.OrdinalIgnoreCase))?.Value;
        }
        /// <summary>
        /// Get secretKey setting value async
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSecretKeyAsync()
        {
            var secretKey = await Table.FirstOrDefaultAsync(s => s.Name.Equals(Constants.Settings.SecretKey, StringComparison.OrdinalIgnoreCase));

            return secretKey?.Value;
        }
        /// <summary>
        /// Get token duration value
        /// </summary>
        /// <returns></returns>
        public int GetTokenDuration()
        {
            return Convert.ToInt32(Table.FirstOrDefault(s => s.Name.Equals(Constants.Settings.TokenDuration, StringComparison.OrdinalIgnoreCase))?.Value);
        }
        /// <summary>
        /// Get token duration value async
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetTokenDurationAsync()
        {
            var tokenDuration = await Table.FirstOrDefaultAsync(s => s.Name.Equals(Constants.Settings.TokenDuration, StringComparison.OrdinalIgnoreCase));

            return Convert.ToInt32(tokenDuration?.Value);
        }
        /// <summary>
        /// Get check expire from value
        /// </summary>
        /// <returns></returns>
        public Constants.CheckExpireFrom GetCheckExpireFrom()
        {
            return JetonEnumHelper.GetEnumValue<Constants.CheckExpireFrom>(Table.FirstOrDefault(s => s.Name.Equals(Constants.Settings.CheckExpireFrom, StringComparison.OrdinalIgnoreCase))?.Value);
        }
        /// <summary>
        /// Get check expire from value async
        /// </summary>
        /// <returns></returns>
        public async Task<Constants.CheckExpireFrom> GetCheckExpireFromAsync()
        {
            var cef = await Table.FirstOrDefaultAsync(s => s.Name.Equals(Constants.Settings.CheckExpireFrom, StringComparison.OrdinalIgnoreCase));

            return JetonEnumHelper.GetEnumValue<Constants.CheckExpireFrom>(cef?.Value);
        }
        #endregion

        /// <summary>
        /// Check is exist by entity
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool IsExist(Setting setting)
        {
            return TableNoTracking.Any(s => s.Name.Equals(setting.Name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Check is exist by entity async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Setting setting)
        {
            return await TableNoTracking.AnyAsync(s => s.Equals(setting));
        }
        /// <summary>
        /// Check isExist by Id
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public bool IsExist(Guid settingId)
        {
            return TableNoTracking.Any(s => s.Id.Equals(settingId));
        }
        /// <summary>
        /// Check isExist by Id async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Guid settingId)
        {
            return await TableNoTracking.AnyAsync(s => s.Id.Equals(settingId));
        }
        /// <summary>
        /// Check isExist by name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            return TableNoTracking.Any(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Check isExist by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(string name)
        {
            return await TableNoTracking.AnyAsync(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        }


    }
}
