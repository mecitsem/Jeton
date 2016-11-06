using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Jwt.Core
{
    public class TokenExpiredException : Exception
    {
        public TokenExpiredException(string message)
            : base(message)
        {
        }
    }
}
