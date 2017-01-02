using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsExist(string nameId);
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User GetUserByNameId(string nameId);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetActiveUsers();

        Task<bool> IsExistAsync(string nameId);
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByNameIdAsync(string nameId);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetActiveUsersAsync();
    }
}
