using Jeton.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Jeton.Data.Configuration
{
    public class AppConfiguration:EntityTypeConfiguration<App>
    {
        public AppConfiguration()
        {
            ToTable("Apps");
            HasKey(a => a.Id);
            Property(a => a.Name).IsRequired().HasMaxLength(255);
            Property(a => a.AccessKey).HasMaxLength(4000);
        }
    }
}
