using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return this.DbContext.Tokens.Where(t => t.Expire > DateTime.Now).Select(u => u.User);
        }

        public User GetUserById(Guid userId)
        {
            return this.DbContext.Users.Find(userId);
        }

        public User GetUserByName(string name)
        {
            return this.DbContext.Users.FirstOrDefault(u => u.Name.Equals(name));
        }

        public User GetUserByNameId(string nameId)
        {
            return this.DbContext.Users.FirstOrDefault(u => u.NameId.Equals(nameId));
        }

        public IEnumerable<User> GetUsers()
        {
            return this.DbContext.Users;
        }

        public bool IsExist(string nameId)
        {
            return this.DbContext.Users.Any(u => u.NameId.Equals(nameId));
        }

        public override void Update(User entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }

        public override void Add(User entity)
        {
            var now = DateTime.Now;
            entity.Created = now;
            entity.Modified = now;
            base.Add(entity);
        }
    }
}
