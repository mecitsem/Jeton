using System;
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
        public SettingRepository(IDbFactory dbFactory)
            : base(dbFactory) { }


        public Setting GetSettingById(Guid settingId)
        {
            return DbContext.Settings.Find(settingId);
        }

        public Setting GetSettingByName(string name)
        {
            return DbContext.Settings.FirstOrDefault(
                    s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsExist(Setting setting)
        {
            return DbContext.Settings.Any(
                s => s.Name.Equals(setting.Name, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsExist(Guid settingId)
        {
            return DbContext.Settings.Any(s => s.Id.Equals(settingId));
        }

        public bool IsExist(string name)
        {
            return DbContext.Settings.Any(
                  s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public string GetSecretKey()
        {
            return DbContext.Settings.FirstOrDefault(
                s => s.Name.Equals(Constants.Settings.SecretKey, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        public int GetTokenDuration()
        {
            return Convert.ToInt32(DbContext.Settings.FirstOrDefault(
                s => s.Name.Equals(Constants.Settings.TokenDuration, StringComparison.OrdinalIgnoreCase))?.Value);
        }

        public Constants.CheckExpireFrom GetCheckExpireFrom()
        {
            return JetonEnumHelper.GetEnumValue<Constants.CheckExpireFrom>(DbContext.Settings.FirstOrDefault(
                 s => s.Name.Equals(Constants.Settings.CheckExpireFrom, StringComparison.OrdinalIgnoreCase))?.Value);
        }

        public Task<Setting> GetSettingByIdAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<Setting> GetSettingByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Setting setting)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Guid settingId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(string name)
        {
            throw new NotImplementedException();
        }

        public override Setting Insert(Setting entity)
        {
            if (IsExist(entity))
                throw new ArgumentException("This setting already exists.");

            return base.Insert(entity);
        }
    }
}
