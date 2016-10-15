using System;
using System.Collections.Generic;
using System.Linq;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories.UserRepo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return DbContext.Tokens.Where(t => t.Expire > DateTime.Now).Select(u => u.User);
        }

        public User GetUserById(Guid userId)
        {
            return DbContext.Users.Find(userId);
        }

        public User GetUserByName(string name)
        {
            return DbContext.Users.FirstOrDefault(u => u.Name.Equals(name));
        }

        public User GetUserByNameId(string nameId)
        {
            return DbContext.Users.FirstOrDefault(u => u.NameId.Equals(nameId));
        }

        public IEnumerable<User> GetUsers()
        {
            return DbContext.Users;
        }

        public bool IsExist(string nameId)
        {
            return DbContext.Users.Any(u => u.NameId.Equals(nameId));
        }

        public override void Update(User entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }

        public override User Insert(User entity)
        {
            var now = DateTime.Now;
            entity.Created = now;
            entity.Modified = now;
            return base.Insert(entity);
        }
    }
}
