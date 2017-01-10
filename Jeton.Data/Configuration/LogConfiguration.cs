using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Configuration
{
    public class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            ToTable("Logs");
            HasKey(l => l.Id);
            Property(l => l.Application).HasMaxLength(200);
            Property(l => l.Machine).HasMaxLength(100);
            Property(l => l.ResponseContentType).HasMaxLength(100);
            Property(l => l.RequestMethod).HasMaxLength(50);
            Property(l => l.RequestIpAddress).HasMaxLength(50);
            Property(l => l.ResponseContentType).HasMaxLength(100);
        }
    }
}
