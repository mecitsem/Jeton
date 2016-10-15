using System;

namespace Jeton.Data.Infrastructure.Interfaces
{
    public interface IDbFactory : IDisposable
    {
        JetonEntities Init();
    }
}
