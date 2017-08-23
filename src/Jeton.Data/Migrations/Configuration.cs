namespace Jeton.Data.Migrations
{
    using Jeton.Core.Common;
    using Jeton.Core.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Jeton.Data.DbContext.JetonDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Jeton.Data.DbContext.JetonDbContext context)
        {
            var secretKey = new Setting()
            {
                Name = Constants.Settings.SecretKey,
                Value = "iBqOVMFLd5VK_otREU_6llwE04xpm973cX5Vdo5VyuY",
                ValueType = Constants.ValueType.String,
                IsEssential = true
            };

            if (!context.Settings.Any(s => s.Name.Equals(secretKey.Name)))
                context.Settings.Add(secretKey);

            var tokenDuration = new Setting()
            {
                Name = Constants.Settings.TokenDuration,
                Value = 24.ToString(),
                ValueType = Constants.ValueType.Integer,
                IsEssential = true
            };

            if (!context.Settings.Any(s => s.Name.Equals(tokenDuration.Name)))
                context.Settings.Add(tokenDuration);

            context.SaveChanges();
        }
    }
}
