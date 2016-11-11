using Jeton.Core.Entities;
using Jeton.Data.Configuration;
using System.Data.Entity;

namespace Jeton.Data
{
    public class JetonEntities : DbContext
    {
        public JetonEntities() : base("JetonEntities") { }

        public DbSet<App> Apps { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Log> Log { get; set; }

        public virtual int Commit()
        {
            return SaveChanges();
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
