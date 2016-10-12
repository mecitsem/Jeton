using Jeton.Data.Repositories.AppRepo;
using Jeton.Data.Repositories.TokenRepo;
using Jeton.Data.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
