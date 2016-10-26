using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Repositories.SettingRepo;
namespace Jeton.Services.SettingService
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
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
    }
}
