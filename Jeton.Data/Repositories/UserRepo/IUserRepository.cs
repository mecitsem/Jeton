using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User GetUserByNameId(string nameId);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetActiveUsers();
    }
}
