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
            Property(l => l.Level).HasMaxLength(100);
            Property(l => l.Logger).HasMaxLength(100);
            Property(l => l.MachineName).HasMaxLength(100);
        }
    }
}
