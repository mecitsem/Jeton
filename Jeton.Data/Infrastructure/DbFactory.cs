using Jeton.Data.DbContext;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private JetonDbContext _dbContext;

        public JetonDbContext Init()
        {
            return _dbContext ?? (_dbContext = new JetonDbContext());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
