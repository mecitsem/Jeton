using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }

        public bool IsExist(string name)
        {
           return _settingRepository.IsExist(name);
        }

        public bool IsExist(Guid settingId)
        {
            return _settingRepository.IsExist(settingId);
        }

        public bool IsExist(Setting setting)
        {
            return _settingRepository.IsExist(setting);
        }

        public Setting GetSettingById(Guid settingId)
        {
            return _settingRepository.GetSettingById(settingId);
        }

        public Setting GetSettingByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _settingRepository.GetSettingByName(name);
        }

        public Setting Insert(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            return _settingRepository.Insert(setting);
        }

        public void Update(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException(nameof(setting));

            _settingRepository.Update(setting);
        }

        public void Delete(Guid settingId)
        {
            if (!_settingRepository.IsExist(settingId))
                throw new ArgumentException("Setting is not exist");

            var setting = _settingRepository.GetSettingById(settingId);

            _settingRepository.Delete(setting);
        }

        public IEnumerable<Setting> GetAllSettings()
        {
            return _settingRepository.Table.ToList();
        }

        public Task<bool> IsExistAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task<Setting> GetSettingByIdAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<Setting> GetSettingByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Setting> InsertAsync(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Setting>> GetAllSettingsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
