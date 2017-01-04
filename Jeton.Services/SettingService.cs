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
    public class SettingService : ISettingService
    {
        #region Ctor
        private readonly ISettingRepository _settingRepository;

        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        #endregion

        #region CREATE
        /// <summary>
        /// Create new setting
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public Setting Create(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            if (_settingRepository.IsExist(setting))
                throw new ArgumentException("Setting is already exist");

            return _settingRepository.Insert(setting);
        }
        /// <summary>
        /// Create new setting async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<Setting> CreateAsync(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            if (await _settingRepository.IsExistAsync(setting))
                throw new ArgumentException("Setting is already exist");

            return await _settingRepository.InsertAsync(setting);
        }
        #endregion

        #region READ

        /// <summary>
        /// Get setting by Id
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public Setting GetSettingById(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            return _settingRepository.GetById(settingId);
        }
        /// <summary>
        /// Get setting by Id async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task<Setting> GetSettingByIdAsync(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            return await _settingRepository.GetByIdAsync(settingId);
        }
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

        /// <summary>
        /// Get all settings
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Setting> GetAllSettings()
        {
            return _settingRepository.GetAll();
        }
        /// <summary>
        /// Get all settings async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
            return await _settingRepository.GetAllAsync();
        }
        #endregion

        #region UPDATE
        /// <summary>
        /// Update setting
        /// </summary>
        /// <param name="setting"></param>
        public void Update(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            if (!_settingRepository.IsExist(setting))
                throw new ArgumentException("Setting is not exist");

            _settingRepository.Update(setting);
        }
        /// <summary>
        /// Update setting async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            if (!await _settingRepository.IsExistAsync(setting))
                throw new ArgumentException("Setting is not exist");

            await _settingRepository.UpdateAsync(setting);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete setting
        /// </summary>
        /// <param name="settingId"></param>
        public void Delete(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            if (!_settingRepository.IsExist(settingId))
                throw new ArgumentException("Setting is not exist");

            var setting = _settingRepository.GetById(settingId);

            _settingRepository.Delete(setting);
        }
        /// <summary>
        /// Delete setting async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            if (!_settingRepository.IsExist(settingId))
                throw new ArgumentException("App is not exist");

            var setting = await GetSettingByIdAsync(settingId);

            await _settingRepository.DeleteAsync(setting);
        }
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

        /// <summary>
        /// Check setting is exist by Id
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public bool IsExist(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            return _settingRepository.IsExist(settingId);
        }

        /// <summary>
        /// Check setting is exist by Id async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Guid settingId)
        {
            if (settingId == null)
                throw new ArgumentNullException(nameof(settingId));

            return await _settingRepository.IsExistAsync(settingId);
        }
        /// <summary>
        /// Check setting is exist by entity
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public bool IsExist(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            return _settingRepository.IsExist(setting);
        }
        /// <summary>
        /// Check setting is exist by entity async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public async Task<bool> IsExistAsync(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            return await _settingRepository.IsExistAsync(setting);
        }
    }
}
