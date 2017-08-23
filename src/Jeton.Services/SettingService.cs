using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public class SettingService : BaseService<Setting>, ISettingService
    {
        #region Ctor
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository) : base(settingRepository)
        {
            _settingRepository = settingRepository;
        }
        #endregion

        #region CREATE
        
        #endregion

        #region READ

        
        /// <summary>
        /// Get setting by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Setting GetSettingByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _settingRepository.GetSettingByName(name);
        }
        /// <summary>
        /// Get setting by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Setting> GetSettingByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await _settingRepository.GetSettingByNameAsync(name);
        }

        
        #endregion

        #region UPDATE
        
        #endregion

        #region DELETE
        
        #endregion

        /// <summary>
        /// Check setting is exist by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _settingRepository.IsExist(name);
        }
        /// <summary>
        /// Check setting is exist by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await _settingRepository.IsExistAsync(name);
        }

        
        
    }
}
