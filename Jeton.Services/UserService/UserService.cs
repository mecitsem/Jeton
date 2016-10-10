using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Repositories.UserRepo;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }

        #region CREATE
        public void Insert(User user)
        {
            userRepository.Add(user);
        }
        #endregion

        #region READ
        public IEnumerable<User> GetLiveUsers()
        {
            return userRepository.GetActiveUsers();
        }

        public User GetUserById(Guid userId)
        {
            return userRepository.GetUserById(userId);
        }

        public User GetUserByName(string name)
        {
            return userRepository.GetUserByName(name);
        }

        public User GetUserByNameId(string nameId)
        {
            return userRepository.GetUserByNameId(nameId);
        }

        public IEnumerable<User> GetUsers()
        {
            return userRepository.GetAll();
        }
        #endregion

        #region UPDATE
        public void Update(User user)
        {
            userRepository.Update(user);
        }
        #endregion

        #region DELETE
        public void Delete(User user)
        {
            userRepository.Delete(user);
        }
        #endregion

  

        public void Save()
        {
            unitOfWork.Commit();
        }

  
    }
}
