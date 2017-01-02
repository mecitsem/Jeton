using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
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

        Task<bool> IsExistAsync(string nameId);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(Guid userId);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByNameIdAsync(string nameId);
        Task<User> InsertAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}
