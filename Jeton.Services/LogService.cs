using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public class LogService : BaseService<Log>, ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository repository) : base(repository)
        {
            _logRepository = repository;
        }

        public IEnumerable<Log> GetLogs(DateTime starDateTime, DateTime endDateTime)
        {
            return _logRepository.GetLogs(starDateTime, endDateTime);
        }

        public async Task<IEnumerable<Log>> GetLogsAsync(DateTime starDateTime, DateTime endDateTime)
        {
            return await _logRepository.GetLogsAsync(starDateTime, endDateTime);
        }

        public long GetLogsCount(DateTime starDateTime, DateTime endDateTime)
        {
            return _logRepository.GetLogsCount(starDateTime, endDateTime);
        }

        public async Task<long> GetLogsCountAsync(DateTime starDateTime, DateTime endDateTime)
        {
            return await _logRepository.GetLogsCountAsync(starDateTime, endDateTime);
        }

        public IEnumerable<Log> GetDailyLogs()
        {
            var startDateTime = DateTime.UtcNow.AddDays(-1).AddMinutes(1);
            var endDateTime = DateTime.UtcNow.AddDays(1).AddMinutes(-1);
            return _logRepository.GetLogs(startDateTime, endDateTime);
        }

        public async Task<IEnumerable<Log>> GetDailyLogsAsync()
        {
            var startDateTime = DateTime.UtcNow.AddDays(-1).AddMinutes(1);
            var endDateTime = DateTime.UtcNow.AddDays(1).AddMinutes(-1);
            return await _logRepository.GetLogsAsync(startDateTime, endDateTime);
        }

        public long GetDailyLogsCount()
        {
            var startDateTime = DateTime.UtcNow.AddDays(-1).AddMinutes(1);
            var endDateTime = DateTime.UtcNow.AddDays(1).AddMinutes(-1);
            return _logRepository.GetLogsCount(startDateTime, endDateTime);
        }

        public async Task<long> GetDailyLogsCountAsync()
        {
            var startDateTime = DateTime.UtcNow.AddDays(-1).AddMinutes(1);
            var endDateTime = DateTime.UtcNow.AddDays(1).AddMinutes(-1);
            return await _logRepository.GetLogsCountAsync(startDateTime, endDateTime);
        }
    }
}
