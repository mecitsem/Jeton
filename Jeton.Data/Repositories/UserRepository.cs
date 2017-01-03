using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        #region INSERT
        #endregion

        #region UPDATE

        #endregion

        #region DELETE
        #endregion

        #region SELECT
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public User GetUserById(Guid userId)
        {
            return base.GetById(userId);
        }
        /// <summary>
        /// Get user by Id async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            return await base.GetByIdAsync(userId);
        }
        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetUserByName(string name)
        {
            return Table.FirstOrDefault(u => u.Name.Equals(name));
        }
        /// <summary>
        /// Get user by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string name)
        {
            return await Table.FirstOrDefaultAsync(u => string.Equals(name, u.Name, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Get user by nameId
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public User GetUserByNameId(string nameId)
        {
            return Table.FirstOrDefault(u => u.NameId.Equals(nameId));
        }
        /// <summary>
        /// Get user by nameId async
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameIdAsync(string nameId)
        {
            return await Table.FirstOrDefaultAsync(u => string.Equals(nameId, u.NameId, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// Get all users 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<User> GetAllUsers()
        {
            return Table.ToList();
        }
        /// <summary>
        /// Get all users async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await Table.ToListAsync();
        }
        #endregion

        public bool IsExist(string nameId)
        {
            return TableNoTracking.Any(u => string.Equals(u.NameId, nameId, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<bool> IsExistAsync(string nameId)
        {
            return await TableNoTracking.AnyAsync(u => string.Equals(nameId, u.NameId, StringComparison.OrdinalIgnoreCase));
        }

     




    }
}
