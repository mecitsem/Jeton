using System;
using System.Collections.Generic;
using System.Linq;
using Jeton.Core.Entities;
using Jeton.Core.Common;
using Jeton.Data.Repositories.TokenRepo;

namespace Jeton.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            this._tokenRepository = tokenRepository;
        }

        #region CREATE
        public Token Insert(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            return _tokenRepository.Insert(token);
        }
        #endregion

        #region READ
        public virtual Token GetTokenById(Guid tokenId)
        {
            return _tokenRepository.GetById(tokenId);
        }

        public virtual Token GetTokenByKey(string tokenKey)
        {
            if (string.IsNullOrEmpty(tokenKey))
                return null;

            var table = _tokenRepository.Table;

            return table.FirstOrDefault(t => t.TokenKey.Equals(tokenKey));
        }

        public virtual Token GetTokenByUser(User user)
        {
            if (user == null)
                return null;

            var table = _tokenRepository.Table;

            return table.FirstOrDefault(t => t.UserID.Equals(user.UserID));
        }

        public virtual Token GetTokenByUserId(Guid userId)
        {
            var table = _tokenRepository.Table;

            return table.FirstOrDefault(t => t.UserID.Equals(userId));
        }

        public virtual IEnumerable<Token> GetTokens()
        {
            return _tokenRepository.Table.ToList();
        }

        public virtual IEnumerable<Token> GetLiveTokens()
        {
            var now = DateTime.Now;
            var table = _tokenRepository.Table;
            return table.Where(t => t.Expire > now).ToList();
        }
        #endregion

        #region UPDATE
        public virtual void Update(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            _tokenRepository.Update(token);
        }
        #endregion

        #region DELETE
        public virtual void Delete(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            _tokenRepository.Delete(token);
        }
        #endregion


        public virtual bool IsExist(string tokenKey)
        {
            return _tokenRepository.TableNoTracking.Any(t => t.TokenKey.Equals(tokenKey));
        }

        public virtual bool IsExistByUser(User user)
        {
            return _tokenRepository.TableNoTracking.Any(t => t.UserID.Equals(user.UserID));
        }

        public virtual Token Generate(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

           
            var tokenManager = new TokenManager();
            var time = tokenManager.Now;
            var tokenKey = tokenManager.GenerateTokenKey(user.NameId, user.Name);
            var tokenExprire = tokenManager.GetExpire(time);
            var table = _tokenRepository.Table;


            Token token;
            if (IsExistByUser(user)) //Token isExist Update Token
            {
                token = table.FirstOrDefault(t => t.UserID.Equals(user.UserID));
                if (token == null) return null;
                token.TokenKey = tokenKey;
                token.Expire = tokenExprire;
                _tokenRepository.Update(token);
            }
            else //Create Token
            {

                var newToken = new Token()
                {
                    User = user,
                    UserID = user.UserID,
                    TokenKey = tokenKey,
                    Expire = tokenExprire
                };
                token = _tokenRepository.Insert(newToken);
            }

            return token;

        }

        public bool IsLive(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var tokenManager = new TokenManager();

            var result = token.Expire.HasValue ? tokenManager.TokenIsLive(token.Expire.Value) : tokenManager.TokenIsLive(token.TokenKey);

            return result;
        }

        public bool IsLiveByTokenKey(string tokenKey)
        {
            if (string.IsNullOrEmpty(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            if (!IsExist(tokenKey))
                throw new AggregateException("Token is not exist");

            var token = GetTokenByKey(tokenKey);

            return IsLive(token);

        }
    }
}
