using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface ITokenRepository: IRepository<Token>
    {
        bool IsExist(string tokenKey);
        IEnumerable<Token> GetTokens();
        IEnumerable<Token> GetLiveTokens();
        Token GetTokenById(Guid tokenId);
        Token GetTokenByUserId(Guid userId);
        Token GetTokenByUser(User user);
        Token GetTokenByKey(string tokenKey);
        //Async
        Task<bool> IsExistAsync(string tokenKey);
        Task<IEnumerable<Token>> GetTokensAsync();
        Task<IEnumerable<Token>> GetLiveTokensAsync();
        Task<Token> GetTokenByIdAsync(Guid tokenId);
        Task<Token> GetTokenByUserIdAsync(Guid userId);
        Task<Token> GetTokenByUserAsync(User user);
        Task<Token> GetTokenByKeyAsync(string tokenKey);
    }
}
