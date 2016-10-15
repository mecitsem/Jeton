using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Jeton.Data.Repositories.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsExist(string nameId);
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User GetUserByNameId(string nameId);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetActiveUsers();
    }
}
