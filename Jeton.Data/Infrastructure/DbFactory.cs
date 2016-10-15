using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
