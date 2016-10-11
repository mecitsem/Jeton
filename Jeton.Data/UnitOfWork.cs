using Jeton.Core;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Data.Repositories.AppRepo;
using Jeton.Data.Repositories.TokenRepo;
using Jeton.Data.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private JetonEntities dbContext;

        private AppRepository _appRepository;
        private TokenRepository _tokenRepository;
        private UserRepository _userRepository;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public JetonEntities DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public int Commit()
        {
            try
            {
                return DbContext.Commit();
            }
            catch (Exception ex)
            {
                throw; //TODO: Logging
            }

        }

        public AppRepository AppRepository
        {
            get
            {
                return _appRepository ?? (new AppRepository(dbFactory));
            }
        }

        public TokenRepository TokenRepository
        {
            get
            {
                return _tokenRepository ?? (new TokenRepository(dbFactory));
            }
        }

        public UserRepository UserRepository
        {
            get
            {
                return _userRepository ?? (new UserRepository(dbFactory));
            }
        }

    }
}
