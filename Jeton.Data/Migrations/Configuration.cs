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

            //var tokenManager = new TokenManager();

            //var app = new App()
            //{
            //    Name = "RootApp",
            //    IsRoot = true,
            //    AccessKey = tokenManager.GenerateAccessKey(),
            //};
            ////Save RootApp
            //context.Apps.Add(app);

            //var app1 = new App()
            //{
            //    Name = "App1",
            //    IsRoot = false,
            //    AccessKey = tokenManager.GenerateAccessKey()
            //};
            ////Save App1
            //context.Apps.Add(app1);

            //context.SaveChanges();


        }
    }
}
