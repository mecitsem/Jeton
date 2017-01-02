using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ITokenService
    {
        bool IsVerified(string tokenKey);
        bool IsVerified(Token token);
        bool IsExpired(Token token);
        Token Generate(User user, App app);
        bool IsExist(string tokenKey);
        bool IsExistByUser(User user);
        bool IsExistByApp(App app);
        IEnumerable<Token> GetTokens();
        int GetTokensCount();
        IEnumerable<Token> GetActiveTokens();
        int GetActiveTokensCount();
        Token GetTokenById(Guid tokenId);
        Token GetTokenByKey(string tokenKey);
        Token GetTokenByUserId(Guid userId);
        Token GetTokenByUser(User user);
        Token Insert(Token token);
        void Update(Token token);
        void Delete(Token token);

        Task<bool> IsVerifiedAsync(string tokenKey);
        Task<bool> IsVerifiedAsync(Token token);
        Task<bool> IsExpiredAsync(Token token);
        Task<Token> GenerateAsync(User user, App app);
        Task<bool> IsExistAsync(string tokenKey);
        Task<bool> IsExistByUserAsync(User user);
        Task<bool> IsExistByAppAsync(App app);
        Task<IEnumerable<Token>> GetTokensAsync();
        Task<int> GetTokensCountAsync();
        Task<IEnumerable<Token>> GetActiveTokensAsync();
        Task<int> GetActiveTokensCountAsync();
        Task<Token> GetTokenByIdAsync(Guid tokenId);
        Task<Token> GetTokenByKeyAsync(string tokenKey);
        Task<Token> GetTokenByUserIdAsync(Guid userId);
        Task<Token> GetTokenByUserAsync(User user);
        Task<Token> InsertAsync(Token token);
        Task UpdateAsync(Token token);
        Task DeleteAsync(Token token);
    }
}
