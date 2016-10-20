using Jeton.Core.Entities;
using System;
using System.Collections.Generic;

namespace Jeton.Services.TokenService
{
    public interface ITokenService
    {
        bool IsActiveByTokenKey(string tokenKey);
        bool IsActive(Token token);
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
    }
}
