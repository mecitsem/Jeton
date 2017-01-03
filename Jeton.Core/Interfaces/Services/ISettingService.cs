using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ISettingService
    {
        bool IsExist(string name);
        bool IsExist(Guid settingId);
        bool IsExist(Setting setting);
        Setting GetSettingById(Guid settingId);
        Setting GetSettingByName(string name);
        Setting Create(Setting setting);
        void Update(Setting setting);
        void Delete(Guid settingId);
        IEnumerable<Setting> GetAllSettings();

        Task<bool> IsExistAsync(string name);
        Task<bool> IsExistAsync(Guid settingId);
        Task<bool> IsExistAsync(Setting setting);
        Task<Setting> GetSettingByIdAsync(Guid settingId);
        Task<Setting> GetSettingByNameAsync(string name);
        Task<Setting> CreateAsync(Setting setting);
        Task UpdateAsync(Setting setting);
        Task DeleteAsync(Guid settingId);
        Task<IEnumerable<Setting>> GetAllSettingsAsync();
    }
}
