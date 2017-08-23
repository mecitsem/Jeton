using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Common;
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
            return _logRepository.GetLogs(Constants.Now.BeginDateTime(), Constants.Now.EndDateTime());
        }

        public async Task<IEnumerable<Log>> GetDailyLogsAsync()
        {
           
            return await _logRepository.GetLogsAsync(Constants.Now.BeginDateTime(), Constants.Now.EndDateTime());
        }

        public long GetDailyLogsCount()
        {
            return _logRepository.GetLogsCount(Constants.Now.BeginDateTime(), Constants.Now.EndDateTime());
        }

        public async Task<long> GetDailyLogsCountAsync()
        {
            return await _logRepository.GetLogsCountAsync(Constants.Now.BeginDateTime(), Constants.Now.EndDateTime());
        }
    }
}
