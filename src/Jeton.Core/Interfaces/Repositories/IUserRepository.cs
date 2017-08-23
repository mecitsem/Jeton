using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        bool IsExist(string nameId);
        Task<bool> IsExistAsync(string nameId);

        User GetUserById(Guid userId);
        Task<User> GetUserByIdAsync(Guid userId);

        User GetUserByName(string name);
        Task<User> GetUserByNameAsync(string name);

        User GetUserByNameId(string nameId);
        Task<User> GetUserByNameIdAsync(string nameId);

        IEnumerable<User> GetAllUsers();
        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
