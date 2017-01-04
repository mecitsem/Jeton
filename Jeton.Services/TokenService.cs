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
        /// <summary>
        /// Create token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public Token Create(Token token)
        {
            if (string.IsNullOrWhiteSpace(token?.TokenKey))
                throw new ArgumentNullException(nameof(token));

            if (_tokenRepository.IsExist(token.TokenKey))
                throw new ArgumentException("Tokendkey is already exist");

            return _tokenRepository.Insert(token);
        }
        /// <summary>
        /// Crate token async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Token> CreateAsync(Token token)
        {
            if (string.IsNullOrWhiteSpace(token?.TokenKey))
                throw new ArgumentNullException(nameof(token));

            if (await _tokenRepository.IsExistAsync(token.TokenKey))
                throw new ArgumentException("Tokendkey is already exist");

            return await _tokenRepository.InsertAsync(token);
        }
        #endregion

        #region READ
        /// <summary>
        /// Get token by Id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public Token GetTokenById(Guid tokenId)
        {
            if (tokenId == default(Guid))
                throw new ArgumentNullException(nameof(tokenId));

            return _tokenRepository.GetById(tokenId);
        }
        /// <summary>
        /// Get token by Id async
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByIdAsync(Guid tokenId)
        {
            if (tokenId == default(Guid))
                throw new ArgumentNullException(nameof(tokenId));

            return await _tokenRepository.GetByIdAsync(tokenId);
        }
        /// <summary>
        /// Get token by tokenkey
        /// </summary>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public virtual Token GetTokenByKey(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                return null;

            if (!_tokenRepository.IsExist(tokenKey))
                throw new ArgumentException("Token is not exist");

            return _tokenRepository.GetTokenByKey(tokenKey);
        }
        /// <summary>
        /// Get token by tokenkey async
        /// </summary>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByKeyAsync(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                return null;

            if (!_tokenRepository.IsExist(tokenKey))
                throw new ArgumentException("Token is not exist");

            return await _tokenRepository.GetTokenByKeyAsync(tokenKey);
        }
        /// <summary>
        /// Get token by user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Token GetTokenByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return _tokenRepository.GetTokenByUser(user);
        }
        /// <summary>
        /// Get token by user async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _tokenRepository.GetTokenByUserAsync(user);
        }
        /// <summary>
        /// Get token by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Token GetTokenByUserId(Guid userId)
        {
            return _tokenRepository.GetTokenByUserId(userId);
        }
        /// <summary>
        /// Get token by userId async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByUserIdAsync(Guid userId)
        {
            return await _tokenRepository.GetTokenByUserIdAsync(userId);
        }
        /// <summary>
        /// Get all tokens
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Token> GetAllTokens()
        {
            return _tokenRepository.GetAllTokens();
        }
        /// <summary>
        /// Get all tokens async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> GetAllTokensAsync()
        {
            return await _tokenRepository.GetAllTokensAsync();
        }
        /// <summary>
        /// Get active tokens
        /// </summary>
        /// <returns></returns>
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

            return _tokenRepository.Table.Where(t => !tokenManager.TokenIsExpired(t)).ToList();
        }
        /// <summary>
        /// Get active tokens async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> GetActiveTokensAsync()
        {
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

            return await _tokenRepository.Table.Where(t => !tokenManager.TokenIsExpired(t)).ToListAsync();
        }

        #endregion

        #region UPDATE
        /// <summary>
        /// Update token
        /// </summary>
        /// <param name="token"></param>
        public void Update(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (!_tokenRepository.IsExist(token))
                throw new ArgumentException("Token is not exist");

            _tokenRepository.Update(token);
        }
        /// <summary>
        /// Update token async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (!await _tokenRepository.IsExistAsync(token))
                throw new ArgumentException("Token is not exist");

            await _tokenRepository.UpdateAsync(token);
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Delete token
        /// </summary>
        /// <param name="token"></param>
        public void Delete(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (!_tokenRepository.IsExist(token))
                throw new ArgumentException("Token is not exist");

            _tokenRepository.Delete(token);
        }
        /// <summary>
        /// Delete token async
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Token token)
        {
            if (token == null)
                throw new ArgumentNullException(nameof(token));

            if (!await _tokenRepository.IsExistAsync(token))
                throw new ArgumentException("Token is not exist");

            await _tokenRepository.DeleteAsync(token);
        }



        #endregion

        public bool IsVerified(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            var token = _tokenRepository.GetTokenByKey(tokenKey);

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            //Check Token expired by Payload in TokenKey
            var result = this.IsVerified(token);

            return result;
        }
        public async Task<bool> IsVerifiedAsync(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            var token = await _tokenRepository.GetTokenByKeyAsync(tokenKey);

            if (token == null)
                throw new ArgumentNullException(nameof(token));

            //Check Token expired by Payload in TokenKey
            var result = await this.IsVerifiedAsync(token);

            return result;
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

        public Token Generate(User user, App app)
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

            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            Token token;
            if (IsExistByUser(user) && IsExistByApp(app)) //Token isExist Update Token
            {
                token = _tokenRepository.GetTokenByUser(user);
                if (token == null) return null;
                token.TokenKey = tokenKey;
                token.Expire = expire;
                _tokenRepository.Update(token);
            }
            else //Create Token
            {

                var newToken = new Token
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
        public async Task<Token> GenerateAsync(User user, App app)
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

            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));


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

        public bool IsExist(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            return _tokenRepository.IsExist(tokenKey);
        }
        public async Task<bool> IsExistAsync(string tokenKey)
        {
            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            return await _tokenRepository.IsExistAsync(tokenKey);
        }

        public bool IsExistByUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return _tokenRepository.TableNoTracking.Any(t => t.UserId.Equals(user.Id));
        }
        public async Task<bool> IsExistByUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return await _tokenRepository.TableNoTracking.AnyAsync(t => t.UserId.Equals(user.Id));
        }

        public bool IsExistByApp(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return _tokenRepository.TableNoTracking.Any(t => t.Id.Equals(app.Id));
        }
        public async Task<bool> IsExistByAppAsync(App app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            return await _tokenRepository.TableNoTracking.AnyAsync(t => t.AppId.Equals(app.Id));
        }


        public int GetTokensCount()
        {
            return _tokenRepository.TableNoTracking.Count();
        }
        public async Task<int> GetTokensCountAsync()
        {
            return await _tokenRepository.TableNoTracking.CountAsync();
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

            return _tokenRepository.TableNoTracking.Count(t => !tokenManager.TokenIsExpired(t));
        }
        public async Task<int> GetActiveTokensCountAsync()
        {
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

            return await _tokenRepository.TableNoTracking.CountAsync(t => !tokenManager.TokenIsExpired(t));
        }











    }
}
