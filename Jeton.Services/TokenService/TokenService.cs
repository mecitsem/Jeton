using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Core.Helpers;
using Jeton.Core.Common;

namespace Jeton.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IRepository<Token> tokenRepository;

        public TokenService(IRepository<Token> tokenRepository)
        {
            this.tokenRepository = tokenRepository;
        }

        #region CREATE
        public Token Insert(Token token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            return tokenRepository.Insert(token);
        }
        #endregion

        #region READ
        public virtual Token GetTokenById(Guid tokenId)
        {
            return tokenRepository.GetById(tokenId);
        }

        public virtual Token GetTokenByKey(string tokenKey)
        {
            if (string.IsNullOrEmpty(tokenKey))
                return null;

            var table = tokenRepository.Table;

            return table.FirstOrDefault(t => t.TokenKey.Equals(tokenKey));
        }

        public virtual Token GetTokenByUser(User user)
        {
            if (user == null)
                return null;

            var table = tokenRepository.Table;

            return table.FirstOrDefault(t => t.UserID.Equals(user.UserID));
        }

        public virtual Token GetTokenByUserID(Guid userId)
        {
            if (userId == null)
                return null;

            var table = tokenRepository.Table;

            return table.FirstOrDefault(t => t.UserID.Equals(userId));
        }

        public virtual IEnumerable<Token> GetTokens()
        {
            return tokenRepository.Table.ToList();
        }

        public virtual IEnumerable<Token> GetLiveTokens()
        {
            var now = DateTime.Now;
            var table = tokenRepository.Table;
            return table.Where(t => t.Expire > now).ToList();
        }
        #endregion

        #region UPDATE
        public virtual void Update(Token token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            tokenRepository.Update(token);
        }
        #endregion

        #region DELETE
        public virtual void Delete(Token token)
        {
            if (token == null)
                throw new ArgumentNullException("token");

            tokenRepository.Delete(token);
        }
        #endregion


        public virtual bool IsExist(string tokenKey)
        {
            return tokenRepository.TableNoTracking.Any(t => t.TokenKey.Equals(tokenKey));
        }

        public virtual bool IsExistByUser(User user)
        {
            return tokenRepository.TableNoTracking.Any(t => t.UserID.Equals(user.UserID));
        }

        public virtual Token Generate(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            
            var tokenManager = new TokenManager(Constants.TimeType.Minute);
            var tokenKey = tokenManager.GenerateTokenKey(user.NameId, user.Name);
            var tokenExprire = tokenManager.GetExpire();
            var table = tokenRepository.Table;


            Token token;
            if (IsExistByUser(user)) //Token isExist Update Token
            {
                token = table.FirstOrDefault(t => t.UserID.Equals(user.UserID));
                token.TokenKey = tokenKey;
                token.Expire = tokenExprire;
                tokenRepository.Update(token);
            }
            else //Create Token
            {
                
                var newToken = new Token();
                newToken.TokenKey = tokenKey;
                newToken.Expire = tokenExprire;
                token = tokenRepository.Insert(newToken);
            }

            return token;

        }
    }
}
