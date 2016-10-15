using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

namespace Jeton.Data.Repositories.TokenRepo
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
    }
}
