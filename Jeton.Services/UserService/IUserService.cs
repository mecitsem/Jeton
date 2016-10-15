using Jeton.Core.Entities;
using System;
using System.Collections.Generic;

namespace Jeton.Services.UserService
{
    public interface IUserService
    {
        bool IsExist(string nameId);
        IEnumerable<User> GetUsers();
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User GetUserByNameId(string nameId);
        User Insert(User user);
        void Update(User user);
        void Delete(User user);
    }
}
