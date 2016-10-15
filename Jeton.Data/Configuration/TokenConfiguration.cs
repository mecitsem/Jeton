using Jeton.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Jeton.Data.Configuration
{
    public class TokenConfiguration : EntityTypeConfiguration<Token>
    {
        public TokenConfiguration()
        {
            ToTable("Tokes");
            Property(t => t.TokenKey).IsRequired().HasMaxLength(5000);
            
        }
    }
}
