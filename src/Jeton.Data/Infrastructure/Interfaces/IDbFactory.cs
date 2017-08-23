using System;
using Jeton.Data.DbContext;

namespace Jeton.Data.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        JetonDbContext Init();
    }
}
