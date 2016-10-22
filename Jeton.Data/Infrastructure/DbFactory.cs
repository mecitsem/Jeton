using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private JetonEntities _dbContext;

        public JetonEntities Init()
        {
            return _dbContext ?? (_dbContext = new JetonEntities());
        }

        protected override void DisposeCore()
        {
            _dbContext?.Dispose();
        }
    }
}
