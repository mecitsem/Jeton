using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
{
    public class TokenRepository : RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<Token> GetLiveTokens()
        {
            return DbContext.Tokens.Where(t => t.Expire > DateTime.Now);
        }

        public Token GetTokenById(Guid tokenId)
        {
            return DbContext.Tokens.Find(tokenId);
        }

        public Token GetTokenByKey(string tokenKey)
        {
            return DbContext.Tokens.FirstOrDefault(t => t.TokenKey.Equals(tokenKey));
        }

        public Task<bool> IsExistAsync(string tokenKey)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Token>> GetTokensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Token>> GetLiveTokensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByIdAsync(Guid tokenId)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByKeyAsync(string tokenKey)
        {
            throw new NotImplementedException();
        }

        public Token GetTokenByUser(User user)
        {
            return DbContext.Tokens.FirstOrDefault(t => t.User.Equals(user));
        }

        public Token GetTokenByUserId(Guid userId)
        {
            return DbContext.Tokens.FirstOrDefault(t => t.Id.Equals(userId));
        }

        public IEnumerable<Token> GetTokens()
        {
            return DbContext.Tokens;
        }

        public bool IsExist(string tokenKey)
        {
            return DbContext.Tokens.Any(t => t.TokenKey.Equals(tokenKey));
        }

        public override void Update(Token entity)
        {
            entity.Modified = DateTime.Now;
            base.Update(entity);
        }

        public override Token Insert(Token entity)
        {
            entity.Created = DateTime.Now;
            entity.Modified = DateTime.Now;
            return base.Insert(entity);
        }
    }
}
