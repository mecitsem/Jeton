using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface ILogRepository : IRepository<Log>
    {
        IEnumerable<Log> GetLogs(DateTime starDateTime, DateTime endDateTime);
        Task<IEnumerable<Log>> GetLogsAsync(DateTime starDateTime, DateTime endDateTime);

        long GetLogsCount(DateTime starDateTime, DateTime endDateTime);
        Task<long> GetLogsCountAsync(DateTime starDateTime, DateTime endDateTime);
    }
}
