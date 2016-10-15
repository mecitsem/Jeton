using Jeton.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Jeton.Data.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");
            Property(u => u.Name).IsRequired().HasMaxLength(255);
            Property(u => u.NameId).IsRequired().HasMaxLength(255);
        }
    }
}
