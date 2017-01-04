using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface ITokenService
    {

        int GetTokensCount();
        Task<int> GetTokensCountAsync();

        IEnumerable<Token> GetActiveTokens();
        Task<IEnumerable<Token>> GetActiveTokensAsync();

        int GetActiveTokensCount();
        Task<int> GetActiveTokensCountAsync();

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
