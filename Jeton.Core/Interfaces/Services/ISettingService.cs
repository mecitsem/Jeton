using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ISettingService
    {

        //GET by name
        Setting GetSettingByName(string name);
        Task<Setting> GetSettingByNameAsync(string name);

        //IsExist check by name
        bool IsExist(string name);
        Task<bool> IsExistAsync(string name);

    }
}
