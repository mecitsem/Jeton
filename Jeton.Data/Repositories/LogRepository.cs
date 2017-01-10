using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
{
    public class LogRepository : BaseRepository<Log>, ILogRepository
    {
        public LogRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Log> GetLogs(DateTime starDateTime, DateTime endDateTime)
        {
            return Table.AsQueryable().Where(o => o.Created >= starDateTime && o.Created <= endDateTime).ToList();
        }

        public async Task<IEnumerable<Log>> GetLogsAsync(DateTime starDateTime, DateTime endDateTime)
        {
            return await Table.AsQueryable().Where(o => o.Created >= starDateTime && o.Created <= endDateTime).ToListAsync();
        }

        public long GetLogsCount(DateTime starDateTime, DateTime endDateTime)
        {
            return TableNoTracking.AsQueryable().LongCount(o => o.Created >= starDateTime && o.Created <= endDateTime);
        }

        public async Task<long> GetLogsCountAsync(DateTime starDateTime, DateTime endDateTime)
        {
            return await TableNoTracking.AsQueryable().LongCountAsync(o => o.Created >= starDateTime && o.Created <= endDateTime);
        }
    }
}
