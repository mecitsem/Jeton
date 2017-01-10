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

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="tokenKey"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public static bool TokenIsExpired(string tokenKey, int timeDuration, string secretKey)
        {
            bool result;

            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            try
            {
                //Verify Token
                if (!IsVerified(tokenKey, secretKey)) return true;

                var payload = JsonWebToken.DecodeToObject<Payload>(tokenKey, secretKey);

                if (payload == null)
                    throw new ArgumentNullException(nameof(payload));

                var nowUnixTimestamp = Constants.Now.GetUnixTimeStamp();

                result = nowUnixTimestamp > payload.Expire;
            }
            catch (Exception ex)
            {
                //TODO:Log
                //Token is not verified
                result = true;
            }
            return result;
        }

        public static bool IsVerified(string tokenKey, string secretKey)
        {
            bool result;
            try
            {
                JsonWebToken.DecodeToObject<Payload>(tokenKey, secretKey);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
