using System;
using System.Collections.Generic;
using System.Linq;
using Jeton.Core.Entities;
using Jeton.Core.Common;
using Jeton.Core.Helpers;
using Jeton.Core.Models;
using Jeton.Data.Repositories.SettingRepo;
using Jeton.Data.Repositories.TokenRepo;

namespace Jeton.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ISettingRepository _settingRepository;

        public TokenService(ITokenRepository tokenRepository,
                            ISettingRepository settingRepository)
        {
            _tokenRepository = tokenRepository;
            _settingRepository = settingRepository;
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
            var secretKey = _settingRepository.GetSecretKey();
            var checkExpireFrom = _settingRepository.GetCheckExpireFrom();
            var tokenDuration = _settingRepository.GetTokenDuration();

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            var tokenManager = new TokenManager(secretKey)
            {
                CheckExpireFrom = checkExpireFrom,
                TokenDuration = tokenDuration,
            };
            var table = _tokenRepository.Table;
            return table.AsEnumerable().Where(t => !tokenManager.TokenIsExpired(t)).ToList();
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
            return user != null && _tokenRepository.TableNoTracking.Any(t => t.UserID.Equals(user.UserID));
        }

        public virtual bool IsExistByApp(App app)
        {
            return app != null && _tokenRepository.TableNoTracking.Any(t => t.AppID.Equals(app.AppID));
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

            var secretKey = _settingRepository.GetSecretKey();
            var checkExpireFrom = _settingRepository.GetCheckExpireFrom();
            var tokenDuration = _settingRepository.GetTokenDuration();

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            var tokenManager = new TokenManager(secretKey)
            {
                CheckExpireFrom = checkExpireFrom,
                TokenDuration = tokenDuration,
            };


            var time = tokenManager.Now;
            var unixTimestamp = tokenManager.GetUnixTimeStamp(time);

            var expire = tokenManager.GetExpire(time);
            var unixExpstamp = tokenManager.GetUnixTimeStamp(expire);

            var payload = new Payload()
            {
                Time = unixTimestamp,
                RootAppId = app.AppID,
                UserName = user.Name,
                UserNameId = user.NameId,
                Expire = unixExpstamp
            };

            var tokenKey = tokenManager.GenerateTokenKey(payload);

            var table = _tokenRepository.Table;


            Token token;
            if (IsExistByUser(user) && IsExistByApp(app)) //Token isExist Update Token
            {
                token = table.FirstOrDefault(t => t.UserID.Equals(user.UserID));
                if (token == null) return null;
                token.TokenKey = tokenKey;
                token.Expire = expire;
                _tokenRepository.Update(token);
            }
            else //Create Token
            {

                var newToken = new Token()
                {
                    User = user,
                    UserID = user.UserID,
                    TokenKey = tokenKey,
                    Expire = expire,
                    App = app,
                    AppID = app.AppID
                };
                token = _tokenRepository.Insert(newToken);
            }

            return token;

        }

        public bool IsExpired(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var secretKey = _settingRepository.GetSecretKey();
            var checkExpireFrom = _settingRepository.GetCheckExpireFrom();
            var tokenDuration = _settingRepository.GetTokenDuration();

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            var tokenManager = new TokenManager(secretKey)
            {
                CheckExpireFrom = checkExpireFrom,
                TokenDuration = tokenDuration,
            };

            //Check Token expired by Payload in TokenKey
            var result = tokenManager.TokenIsExpired(token);

            return result;
        }

        public int GetTokensCount()
        {
            return _tokenRepository.TableNoTracking.Count();
        }

        public int GetActiveTokensCount()
        {
            var secretKey = _settingRepository.GetSecretKey();
            var checkExpireFrom = _settingRepository.GetCheckExpireFrom();
            var tokenDuration = _settingRepository.GetTokenDuration();

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            var tokenManager = new TokenManager(secretKey)
            {
                CheckExpireFrom = checkExpireFrom,
                TokenDuration = tokenDuration,
            };

            var table = _tokenRepository.TableNoTracking;
            return table.AsEnumerable().Count(t => !tokenManager.TokenIsExpired(t));
        }
    }
}
