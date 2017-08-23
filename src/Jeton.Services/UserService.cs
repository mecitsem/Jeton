using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;

namespace Jeton.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        #region CREATE
        
        #endregion

        #region GET
       
        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public User GetUserByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return _userRepository.GetUserByName(name);
        }
        /// <summary>
        /// Get user by name async
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            return await _userRepository.GetUserByNameAsync(name);
        }
        /// <summary>
        /// Get user by nameId
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public User GetUserByNameId(string nameId)
        {
            if (string.IsNullOrEmpty(nameId))
                throw new ArgumentNullException(nameof(nameId));

            return _userRepository.GetUserByNameId(nameId);
        }
        /// <summary>
        /// Get user by nameId async
        /// </summary>
        /// <param name="nameId"></param>
        /// <returns></returns>
        public async Task<User> GetUserByNameIdAsync(string nameId)
        {
            if (string.IsNullOrEmpty(nameId))
                throw new ArgumentNullException(nameof(nameId));

            return await _userRepository.GetUserByNameIdAsync(nameId);
        }
        
        #endregion

        #region UPDATE

        #endregion

        #region DELETE
        #endregion


        public bool IsExist(string nameId)
        {
            if (string.IsNullOrWhiteSpace(nameId))
                throw new ArgumentNullException(nameof(nameId));

            return _userRepository.IsExist(nameId);
        }

        public async Task<bool> IsExistAsync(string nameId)
        {
            if (string.IsNullOrWhiteSpace(nameId))
                throw new ArgumentNullException(nameof(nameId));

            return await _userRepository.IsExistAsync(nameId);
        }
    }
}
