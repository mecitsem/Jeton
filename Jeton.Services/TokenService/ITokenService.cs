using Jeton.Core.Entities;
using System;
using System.Collections.Generic;

namespace Jeton.Services.TokenService
{
    public interface ITokenService
    {
        bool IsLiveByTokenKey(string tokenKey);
        bool IsLive(Token token);
        Token Generate(User user);
        bool IsExist(string tokenKey);
        bool IsExistByUser(User user);
        IEnumerable<Token> GetTokens();
        IEnumerable<Token> GetLiveTokens();
        Token GetTokenById(Guid tokenId);
        Token GetTokenByKey(string tokenKey);
        Token GetTokenByUserId(Guid userId);
        Token GetTokenByUser(User user);
        Token Insert(Token token);
        void Update(Token token);
        void Delete(Token token);
    }
}
