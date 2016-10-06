using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Configuration
{
    public class AppConfiguration:EntityTypeConfiguration<App>
    {
        public AppConfiguration()
        {
            ToTable("Apps");
            Property(a => a.Name).IsRequired().HasMaxLength(50);
            Property(a => a.AccessKey).HasMaxLength(255);
            
        }
    }
}
