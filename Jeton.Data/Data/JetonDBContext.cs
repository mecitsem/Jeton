using Jeton.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Common;
namespace Jeton.Data.Data
{
    public class JetonDBContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public JetonDBContext() : base(JetonConstants.ConnectionStrings.Jeton) { }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<RegisteredApp> RegisteredApps { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<JetonUser> JetonUsers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DbSet<GeneratedToken> GeneratedTokes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
