using System;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
namespace Jeton.Core.Interfaces.Repositories
{
    public interface ISettingRepository:IRepository<Setting>
    {
        //Sync
        Setting GetSettingById(Guid settingId);
        Setting GetSettingByName(string name);
        bool IsExist(Setting setting);
        bool IsExist(Guid settingId);
        bool IsExist(string name);
        string GetSecretKey();
        int GetTokenDuration();
        Constants.CheckExpireFrom GetCheckExpireFrom();

        //Async
        Task<Setting> GetSettingByIdAsync(Guid settingId);
        Task<Setting> GetSettingByNameAsync(string name);
        Task<bool> IsExistAsync(Setting setting);
        Task<bool> IsExistAsync(Guid settingId);
        Task<bool> IsExistAsync(string name);
    }
}
