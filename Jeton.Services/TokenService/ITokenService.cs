using Jeton.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Services.TokenService
{
    public interface ITokenService
    {
        Token Generate(User user);
        bool IsExist(string tokenKey);
        bool IsExistByUser(User user);
        IEnumerable<Token> GetTokens();
        IEnumerable<Token> GetLiveTokens();
        Token GetTokenById(Guid tokenId);
        Token GetTokenByKey(string tokenKey);
        Token GetTokenByUserID(Guid userId);
        Token GetTokenByUser(User user);
        Token Insert(Token token);
        void Update(Token token);
        void Delete(Token token);
    }
}
