using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Models;
using Jeton.Jwt.Core;

namespace Jeton.Core.Common
{
    public static class JetonUtility
    {
        public static int GetUnixTimeStamp(this DateTime dateTime)
        {
            return (int)Math.Round((dateTime - Constants.UnixEpoch).TotalSeconds);
        }

        public static bool IsExpired(this DateTime expire)
        {
            var nowUnixTimestamp = GetUnixTimeStamp(Constants.Now);
            var expireUnixTimestamp = GetUnixTimeStamp(expire);
            return nowUnixTimestamp > expireUnixTimestamp;
        }

       
    }
}
