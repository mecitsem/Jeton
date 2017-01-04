using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Jeton.Core.Common;
using Jeton.Core.Entities;
using Jeton.Core.Interfaces.Repositories;
using Jeton.Data.Infrastructure;
using Jeton.Data.Infrastructure.Interfaces;

namespace Jeton.Data.Repositories
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        #region Ctor
        public TokenRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        #endregion

        #region INSERT
        #endregion

        #region UPDATE

        #endregion

        #region DELETE
        #endregion

        #region SELECT
        /// <summary>
        /// Get token by Id
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public Token GetTokenById(Guid tokenId)
        {
            return base.GetById(tokenId);
        }
        /// <summary>
        /// Get token by Id async
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByIdAsync(Guid tokenId)
        {
            return await base.GetByIdAsync(tokenId);
        }
        /// <summary>
        /// Get all tokens
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Token> GetAllTokens()
        {
            return Table.ToList();
        }
        /// <summary>
        /// Get all tokens async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> GetAllTokensAsync()
        {
            return await Table.ToListAsync();
        }

        /// <summary>
        /// Get live tokens by db
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Token> GetLiveTokens()
        {
            return Table.Where(t => t.Expire > DateTime.UtcNow).ToList();
        }
        /// <summary>
        /// Get live tokens by db async
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Token>> GetLiveTokensAsync()
        {
            return await Table.Where(t => t.Expire > DateTime.UtcNow).ToListAsync();
        }
        /// <summary>
        /// Get token by tokenKey
        /// </summary>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public Token GetTokenByKey(string tokenKey)
        {
            return Table.FirstOrDefault(t => string.Equals(tokenKey, t.TokenKey));
        }
        /// <summary>
        ///  Get token by tokenKey async
        /// </summary>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByKeyAsync(string tokenKey)
        {
            return await Table.FirstOrDefaultAsync(t => string.Equals(tokenKey, t.TokenKey));
        }
        /// <summary>
        /// Get token by UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Token GetTokenByUserId(Guid userId)
        {
            return Table.FirstOrDefault(t => t.Id.Equals(userId));
        }
        /// <summary>
        /// Get token by UserId async
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByUserIdAsync(Guid userId)
        {
            return await Table.FirstOrDefaultAsync(t => t.UserId.Equals(userId));
        }
        /// <summary>
        /// Get token by User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Token GetTokenByUser(User user)
        {
            return Table.FirstOrDefault(t => t.User.Equals(user));
        }
        /// <summary>
        /// Get token by User async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<Token> GetTokenByUserAsync(User user)
        {
            return await Table.FirstOrDefaultAsync(t => t.User.Equals(user));
        }


        #endregion

        public bool IsExist(string tokenKey)
        {
            return TableNoTracking.Any(t => string.Equals(t.TokenKey, tokenKey));
        }

        public async Task<bool> IsExistAsync(string tokenKey)
        {
            return await TableNoTracking.AnyAsync(t => string.Equals(tokenKey, t.TokenKey));
        }

    }
}
