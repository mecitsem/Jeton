using System.Runtime.CompilerServices;

namespace Jeton.Data.Migrations
{
    using Core.Common;
    using Core.Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Jeton.Data.JetonEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Jeton.Data.JetonEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var secretKey = new Setting()
            {
                Name = Constants.Settings.SecretKey,
                Value = "iBqOVMFLd5VK_otREU_6llwE04xpm973cX5Vdo5VyuY",
                ValueType = Constants.ValueType.String,
                IsEssential = true
            };
            if (!context.Settings.Any(s => s.Name.Equals(secretKey.Name, StringComparison.OrdinalIgnoreCase)))
                context.Settings.Add(secretKey);

            var checkExpireFrom = new Setting()
            {
                Name = Constants.Settings.CheckExpireFrom,
                Value = ((int)Constants.CheckExpireFrom.Database).ToString(),
                ValueType = Constants.ValueType.Integer,
                IsEssential = true
            };
            if (!context.Settings.Any(s => s.Name.Equals(checkExpireFrom.Name, StringComparison.OrdinalIgnoreCase)))
                context.Settings.Add(checkExpireFrom);
            var tokenDuration = new Setting()
            {
                Name = Constants.Settings.TokenDuration,
                Value = 24.ToString(),
                ValueType = Constants.ValueType.Integer,
                IsEssential = true
            };
            if (!context.Settings.Any(s => s.Name.Equals(tokenDuration.Name, StringComparison.OrdinalIgnoreCase)))
                context.Settings.Add(tokenDuration);
            context.SaveChanges();


        }
    }
}
