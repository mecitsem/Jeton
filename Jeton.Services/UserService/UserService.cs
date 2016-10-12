using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        #region CREATE
        public User Insert(User user)
        {
            if (user == null)
                throw new ArgumentException("user");

            return userRepository.Insert(user);
        }
        #endregion

        #region READ
        public User GetUserById(Guid userId)
        {
            if (userId == null)
                throw new ArgumentNullException("userId");

            return userRepository.GetById(userId);
        }

        public User GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            var table = userRepository.Table;

            return table.FirstOrDefault(u => u.Name.Equals(name));
        }

        public User GetUserByNameId(string nameId)
        {
            if (string.IsNullOrEmpty(nameId))
                throw new ArgumentNullException("nameId");

            var table = userRepository.Table;

            return table.FirstOrDefault(u => u.NameId.Equals(nameId));
        }

        public IEnumerable<User> GetUsers()
        {
            return userRepository.Table.ToList();
        }
        #endregion

        #region UPDATE
        public void Update(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            userRepository.Update(user);
        }
        #endregion

        #region DELETE
        public void Delete(User user)
        {
            userRepository.Delete(user);
        }
        #endregion


        public bool IsExist(string nameId)
        {
            if (string.IsNullOrEmpty(nameId))
                throw new ArgumentNullException("nameId");
            var table = userRepository.TableNoTracking;

            return table.Any(u => u.NameId.Equals(nameId));
        }
    }
}
