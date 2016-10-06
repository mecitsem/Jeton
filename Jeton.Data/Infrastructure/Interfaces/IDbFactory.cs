using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        JetonEntities Init();
    }
}
