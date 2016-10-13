using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Configuration
{
    public class TokenConfiguration : EntityTypeConfiguration<Token>
    {
        public TokenConfiguration()
        {
            ToTable("Tokes");
            Property(t => t.TokenKey).IsRequired().HasMaxLength(1000);
            
        }
    }
}
