using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ISettingService
    {
        //CREATE
        Setting Create(Setting setting);
        Task<Setting> CreateAsync(Setting setting);

        //UPDATE
        void Update(Setting setting);
        Task UpdateAsync(Setting setting);

        //DELETE
        void Delete(Guid settingId);
        Task DeleteAsync(Guid settingId);

        //GET by Id
        Setting GetSettingById(Guid settingId);
        Task<Setting> GetSettingByIdAsync(Guid settingId);

        //GET by name
        Setting GetSettingByName(string name);
        Task<Setting> GetSettingByNameAsync(string name);

        //GET All
        IEnumerable<Setting> GetAllSettings();
        Task<IEnumerable<Setting>> GetAllSettingsAsync();

        //IsExist check by name
        bool IsExist(string name);
        Task<bool> IsExistAsync(string name);

        //IsExist check by Id
        bool IsExist(Guid settingId);
        Task<bool> IsExistAsync(Guid settingId);

        //IsExist check
        bool IsExist(Setting setting);
        Task<bool> IsExistAsync(Setting setting);
    }
}
