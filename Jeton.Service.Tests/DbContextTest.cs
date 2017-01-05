using System;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Data.DbContext;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using FluentAssertions;

namespace Jeton.Service.Tests
{
    [TestClass]
    public class DbContextTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new JetonDbContext())
            {
                var secretKey = new Setting()
                {
                    Name = Constants.Settings.SecretKey,
                    Value = "iBqOVMFLd5VK_otREU_6llwE04xpm973cX5Vdo5VyuY",
                    ValueType = Constants.ValueType.String,
                    IsEssential = true,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                };

                if (!context.Settings.Any(s => s.Name.Equals(secretKey.Name)))
                    context.Settings.Add(secretKey);

                var checkExpireFrom = new Setting()
                {
                    Name = Constants.Settings.CheckExpireFrom,
                    Value = ((int)Constants.CheckExpireFrom.Database).ToString(),
                    ValueType = Constants.ValueType.Integer,
                    IsEssential = true,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                };
                if (!context.Settings.Any(s => s.Name.Equals(checkExpireFrom.Name)))
                    context.Settings.Add(checkExpireFrom);
                var tokenDuration = new Setting()
                {
                    Name = Constants.Settings.TokenDuration,
                    Value = 24.ToString(),
                    ValueType = Constants.ValueType.Integer,
                    IsEssential = true,
                    Created = DateTime.UtcNow,
                    Modified = DateTime.UtcNow,
                };

                if (!context.Settings.Any(s => s.Name.Equals(tokenDuration.Name)))
                    context.Settings.Add(tokenDuration);

                context.SaveChanges().Should().BeGreaterThan(0);
            }

        }
    }
}
