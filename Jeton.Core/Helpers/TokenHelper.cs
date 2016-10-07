using Jeton.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Helpers
{
    public class TokenHelper
    {
        
        public string GenerateToken()
        {
            var time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            var key = Guid.NewGuid().ToByteArray();
            var token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public bool TokenIsLive(string token)
        {
            bool result = false; ;
            try
            {
                var data = Convert.FromBase64String(token);
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
                result = when.AddHours(Constants.TokenLiveHours) < DateTime.UtcNow;
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
