using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ITokenService
    {
        Token Create(Token token);
        Task<Token> CreateAsync(Token token);

        void Update(Token token);
        Task UpdateAsync(Token token);

        void Delete(Token token);
        Task DeleteAsync(Token token);

        IEnumerable<Token> GetAllTokens();
        Task<IEnumerable<Token>> GetAllTokensAsync();

        int GetTokensCount();
        Task<int> GetTokensCountAsync();

        IEnumerable<Token> GetActiveTokens();
        Task<IEnumerable<Token>> GetActiveTokensAsync();

        int GetActiveTokensCount();
        Task<int> GetActiveTokensCountAsync();

        Token GetTokenById(Guid tokenId);
        Task<Token> GetTokenByIdAsync(Guid tokenId);

        Token GetTokenByKey(string tokenKey);
        Task<Token> GetTokenByKeyAsync(string tokenKey);

        Token GetTokenByUserId(Guid userId);
        Task<Token> GetTokenByUserIdAsync(Guid userId);

        Token GetTokenByUser(User user);
        Task<Token> GetTokenByUserAsync(User user);

        bool IsVerified(string tokenKey);
        Task<bool> IsVerifiedAsync(string tokenKey);

        bool IsVerified(Token token);
        Task<bool> IsVerifiedAsync(Token token);

        bool IsExpired(Token token);
        Task<bool> IsExpiredAsync(Token token);

        Token Generate(User user, App app);
        Task<Token> GenerateAsync(User user, App app);

        bool IsExist(string tokenKey);
        Task<bool> IsExistAsync(string tokenKey);

        bool IsExistByUser(User user);
        Task<bool> IsExistByUserAsync(User user);

        bool IsExistByApp(App app);
        Task<bool> IsExistByAppAsync(App app);

    }
}
