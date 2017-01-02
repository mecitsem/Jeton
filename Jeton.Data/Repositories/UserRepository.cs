using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
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

        public Task<bool> IsExistAsync(string nameId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByNameIdAsync(string nameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            throw new NotImplementedException();
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
