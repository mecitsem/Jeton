using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories.TokenRepo
{
    public class TokenRepository : RepositoryBase<Token>,ITokenRepository
    {
        public TokenRepository(IDbFactory dbFactory) 
            : base(dbFactory)
        {

        }

        public IEnumerable<Token> GetLiveTokens()
        {
            return this.DbContext.Tokens.Where(t => t.Expire <= DateTime.Now);
        }

        public Token GetTokenById(Guid tokenId)
        {
            return this.DbContext.Tokens.Find(tokenId);
        }

        public Token GetTokenByUser(User user)
        {
            return this.DbContext.Tokens.FirstOrDefault(t => t.User.Equals(user));
        }

        public Token GetTokenByUserId(Guid userId)
        {
            return this.DbContext.Tokens.FirstOrDefault(t => t.UserID.Equals(userId));
        }

        public IEnumerable<Token> GetTokens()
        {
            return this.DbContext.Tokens;
        }

        public override void Update(Token entity)
        {
            entity.Modified = DateTime.Now;
            entity.TokenKey = entity.User.NameId; //Generate Token Key
        }
    }
}
