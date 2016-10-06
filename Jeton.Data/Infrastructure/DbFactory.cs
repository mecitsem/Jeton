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
        JetonEntities dbContext;

        public JetonEntities Init()
        {
            return dbContext ?? (dbContext = new JetonEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
