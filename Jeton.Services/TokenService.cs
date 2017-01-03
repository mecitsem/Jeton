using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Core.Interfaces.Services;
using Jeton.Core.Models;

namespace Jeton.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ISettingRepository _settingRepository;

        public TokenService(ITokenRepository tokenRepository, ISettingRepository settingRepository)
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

            return table.FirstOrDefault(t => t.UserId.Equals(user.Id));
        }

        public virtual Token GetTokenByUserId(Guid userId)
        {
            var table = _tokenRepository.Table;

            return table.FirstOrDefault(t => t.UserId.Equals(userId));
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

        public async Task<bool> IsVerifiedAsync(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            var token = await GetTokenByKeyAsync(tokenKey);

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            //Check Token expired by Payload in TokenKey
            var result = await IsVerifiedAsync(token);

            return result;
        }

        public async Task<bool> IsVerifiedAsync(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var secretKey = await _settingRepository.GetSecretKeyAsync();
            var checkExpireFrom = await _settingRepository.GetCheckExpireFromAsync();
            var tokenDuration = await _settingRepository.GetTokenDurationAsync();

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            var tokenManager = new TokenManager(secretKey)
            {
                CheckExpireFrom = checkExpireFrom,
                TokenDuration = tokenDuration,
            };

            //Check Token expired by Payload in TokenKey
            var result = tokenManager.IsVerified(token.TokenKey);

            return result;
        }

        public async Task<bool> IsExpiredAsync(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            var secretKey = await _settingRepository.GetSecretKeyAsync();
            var checkExpireFrom = await _settingRepository.GetCheckExpireFromAsync();
            var tokenDuration = await _settingRepository.GetTokenDurationAsync();

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

        public virtual async Task<Token> GenerateAsync(User user, App app)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (app == null)
                throw new ArgumentNullException(nameof(app));

            //Check App is Root
            if (!app.IsRoot)
                throw new ArgumentException("The app can't generate token.Becase it is not a root app.");

            var secretKey = await _settingRepository.GetSecretKeyAsync();
            var checkExpireFrom = await _settingRepository.GetCheckExpireFromAsync();
            var tokenDuration = await _settingRepository.GetTokenDurationAsync();

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
                RootAppId = app.Id,
                UserName = user.Name,
                UserNameId = user.NameId,
                Expire = unixExpstamp
            };

            var tokenKey = tokenManager.GenerateTokenKey(payload);

            var table = _tokenRepository.Table;


            Token token;
            if (IsExistByUser(user) && IsExistByApp(app)) //Token isExist Update Token
            {
                token = await _tokenRepository.GetTokenByUserAsync(user);
                if (token == null) return null;
                token.TokenKey = tokenKey;
                token.Expire = expire;
                await _tokenRepository.UpdateAsync(token);
            }
            else //Create Token
            {

                var newToken = new Token()
                {
                    User = user,
                    UserId = user.Id,
                    TokenKey = tokenKey,
                    Expire = expire,
                    App = app,
                    AppId = app.Id
                };
                token = await _tokenRepository.InsertAsync(newToken);
            }

            return token;
        }

        public virtual Task<bool> IsExistAsync(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            return _tokenRepository.IsExistAsync(tokenKey);
        }

        public virtual async Task<bool> IsExistByUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _tokenRepository.TableNoTracking.AnyAsync(t => t.UserId.Equals(user.Id));
        }

        public virtual async Task<bool> IsExistByAppAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return await _tokenRepository.TableNoTracking.AnyAsync(t => t.AppId.Equals(app.Id));
        }

        public virtual async Task<IEnumerable<Token>> GetTokensAsync()
        {
            return await _tokenRepository.GetAllTokensAsync();
        }

        public virtual async Task<int> GetTokensCountAsync()
        {
            return await _tokenRepository.TableNoTracking.CountAsync();
        }

        public Task<IEnumerable<Token>> GetActiveTokensAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetActiveTokensCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByIdAsync(Guid tokenId)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GetTokenByKeyAsync(string tokenKey)
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

        public Task<Token> InsertAsync(Token token)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Token token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Token token)
        {
            throw new NotImplementedException();
        }

        #endregion


        public virtual bool IsExist(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            return _tokenRepository.TableNoTracking.Any(t => t.TokenKey.Equals(tokenKey));
        }

        public virtual bool IsExistByUser(User user)
        {
            return user != null && _tokenRepository.TableNoTracking.Any(t => t.UserId.Equals(user.Id));
        }

        public virtual bool IsExistByApp(App app)
        {
            return app != null && _tokenRepository.TableNoTracking.Any(t => t.Id.Equals(app.Id));
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
                RootAppId = app.Id,
                UserName = user.Name,
                UserNameId = user.NameId,
                Expire = unixExpstamp
            };

            var tokenKey = tokenManager.GenerateTokenKey(payload);

            var table = _tokenRepository.Table;


            Token token;
            if (IsExistByUser(user) && IsExistByApp(app)) //Token isExist Update Token
            {
                token = table.FirstOrDefault(t => t.UserId.Equals(user.Id));
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
                    UserId = user.Id,
                    TokenKey = tokenKey,
                    Expire = expire,
                    App = app,
                    AppId = app.Id
                };
                token = _tokenRepository.Insert(newToken);
            }

            return token;

        }

        public bool IsVerified(Token token)
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
            var result = tokenManager.IsVerified(token.TokenKey);

            return result;
        }


        public bool IsVerified(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            var token = GetTokenByKey(tokenKey);

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            //Check Token expired by Payload in TokenKey
            var result = IsVerified(token);

            return result;
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
