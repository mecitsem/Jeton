using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Services.UserService
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetLiveUsers();
        User GetUserById(Guid userId);
        User GetUserByName(string name);
        User GetUserByNameId(string nameId);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
        void Save();
    }
}
