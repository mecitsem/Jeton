using System;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
namespace Jeton.Core.Interfaces.Repositories
{
    public interface ISettingRepository:IRepository<Setting>
    {
        //Get setting by name
        Setting GetSettingByName(string name);
        Task<Setting> GetSettingByNameAsync(string name);

        //Check isExist by name
        bool IsExist(string name);
        Task<bool> IsExistAsync(string name);

        //Get secret key
        string GetSecretKey();
        Task<string> GetSecretKeyAsync();

        //Get token duration
        int GetTokenDuration();
        Task<int> GetTokenDurationAsync();


    }
}
