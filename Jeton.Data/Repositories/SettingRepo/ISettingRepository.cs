using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories.SettingRepo
{
    public interface ISettingRepository:IRepository<Setting>
    {
        Setting GetSettingById(Guid settingId);
        Setting GetSettingByName(string name);
        bool IsExist(Setting setting);
        bool IsExist(Guid settingId);
    }
}
