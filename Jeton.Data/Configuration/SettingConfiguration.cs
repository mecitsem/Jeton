using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Data.Configuration
{
    public class SettingConfiguration : EntityTypeConfiguration<Setting>
    {
        public SettingConfiguration()
        {
            ToTable("Settings");
            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(255);
            Property(s => s.Value).IsOptional();
            Property(s => s.Description).IsOptional().HasMaxLength(500);
        }


    }
}
