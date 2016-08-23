namespace Jeton.Data.Migrations
{
    using Common.Helpers;
    using Data;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JetonDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(JetonDBContext context)
        {
            context.JetonUsers.Add(new Entities.JetonUser()
            {
                Name = @"i:0#.w|innovaisc\sa_spadmin",
                NameId = @"s-1-5-21-1974876067-1066505772-4238931308-1608",
                Created = DateTime.Now,
            });

            context.RegisteredApps.Add(new Entities.RegisteredApp("Verim")
            {
                Created = DateTime.Now,
                AccessKey = Guid.NewGuid().ToString(),
            });
        }
    }
}
