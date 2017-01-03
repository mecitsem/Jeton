using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Repositories
{
    public interface ITokenRepository: IRepository<Token>
    {
        //Check isexist by tokenkey
        bool IsExist(string tokenKey);
        Task<bool> IsExistAsync(string tokenKey);

        //Get all tokens
        IEnumerable<Token> GetAllTokens();
        Task<IEnumerable<Token>> GetAllTokensAsync();

        //Get all live tokens
        IEnumerable<Token> GetLiveTokens();
        Task<IEnumerable<Token>> GetLiveTokensAsync();

        //Get token by Id
        Token GetTokenById(Guid tokenId);
        Task<Token> GetTokenByIdAsync(Guid tokenId);

        //Get token by userId
        Token GetTokenByUserId(Guid userId);
        Task<Token> GetTokenByUserIdAsync(Guid userId);

        //Get token by user
        Token GetTokenByUser(User user);
        Task<Token> GetTokenByUserAsync(User user);

        //Get token by tokenkey
        Token GetTokenByKey(string tokenKey);
        Task<Token> GetTokenByKeyAsync(string tokenKey);
   
    }
}
