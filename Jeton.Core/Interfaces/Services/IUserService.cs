using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface IUserService : IBaseService<User>
    {

        User GetUserByName(string name);
        Task<User> GetUserByNameAsync(string name);

        User GetUserByNameId(string nameId);
        Task<User> GetUserByNameIdAsync(string nameId);

        bool IsExist(string nameId);
        Task<bool> IsExistAsync(string nameId);

    }
}
