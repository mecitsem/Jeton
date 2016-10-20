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

        public virtual IEnumerable<Token> GetActiveTokens()
        {
            var tokenManager = new TokenManager();
            var table = _tokenRepository.Table;
            return table.AsEnumerable().Where(t => t.Expire.HasValue ? tokenManager.TokenIsActive(t.Expire.Value) : false).ToList();
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
            if (user == null) return false;

            return _tokenRepository.TableNoTracking.Any(t => t.UserID.Equals(user.UserID));
        }

        public virtual bool IsExistByApp(App app)
        {
            if (app == null) return false;

            return _tokenRepository.TableNoTracking.Any(t => t.AppID.Equals(app.AppID));
        }

        public virtual Token Generate(User user, App app)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (app == null)
                throw new ArgumentNullException(nameof(app));
            
            //Check App is Root
            if (!app.IsRoot)
                throw new ArgumentException("The app can't generate token.Becase it is not a root app.");

            var tokenManager = new TokenManager();
            var time = tokenManager.Now;
            var tokenKey = tokenManager.GenerateTokenKey(user.NameId, user.Name);
            var tokenExprire = tokenManager.GetExpire(time);
            var table = _tokenRepository.Table;


            Token token;
            if (IsExistByUser(user) && IsExistByApp(app)) //Token isExist Update Token
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
                    Expire = tokenExprire,
                    App = app,
                    AppID = app.AppID
                };
                token = _tokenRepository.Insert(newToken);
            }

            return token;

        }

        public bool IsActive(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var tokenManager = new TokenManager();

            var result = token.Expire.HasValue ? tokenManager.TokenIsActive(token.Expire.Value) : tokenManager.TokenIsActive(token.TokenKey);

            return result;
        }

        public bool IsActiveByTokenKey(string tokenKey)
        {
            if (string.IsNullOrEmpty(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            if (!IsExist(tokenKey))
                throw new ArgumentException("Token is not exist");

            var token = GetTokenByKey(tokenKey);

            return IsActive(token);

        }

        public int GetTokensCount()
        {
            return _tokenRepository.TableNoTracking.Count();
        }

        public int GetActiveTokensCount()
        {
            var tokenManager = new TokenManager();
            var table = _tokenRepository.TableNoTracking;
            return table.AsEnumerable().Where(t => t.Expire.HasValue ? tokenManager.TokenIsActive(t.Expire.Value) : false).Count();
        }
    }
}
