using System.Data.Entity;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Configuration;

namespace Jeton.Data.DbContext
{
    public class JetonDbContext : System.Data.Entity.DbContext
    {
        public JetonDbContext() : base("JetonDbContext") { }

        public DbSet<App> Apps { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Log> Log { get; set; }

        public virtual int Commit()
        {
            return SaveChanges();
        }

        public virtual async Task<int> CommitAsync()
        {
            return await SaveChangesAsync();
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppConfiguration());
            modelBuilder.Configurations.Add(new TokenConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new SettingConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
        }
    }
}
