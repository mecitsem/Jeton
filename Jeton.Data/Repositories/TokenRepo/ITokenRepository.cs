using Jeton.Core.Entities;
using Jeton.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Repositories.TokenRepo
{
    public interface ITokenRepository: IRepository<Token>
    {
        IEnumerable<Token> GetTokens();
        IEnumerable<Token> GetLiveTokens();
        Token GetTokenById(Guid tokenId);
        Token GetTokenByUserId(Guid userId);
        Token GetTokenByUser(User user);
    }
}
