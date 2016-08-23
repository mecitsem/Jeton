using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Interfaces
{
    public interface ITokenRepository : IDisposable
    {
        string GenerateToken(string appAccessKey, string userLoginName, string userNameId, byte[] spTokenBinary);
        bool IsValidatedUser(string appAccessKey, string jetonToken);
    }
}
