using Jeton.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Jeton.Core.Models;
using JWT;
using static Jeton.Core.Common.Constants;

namespace Jeton.Core.Common
{
    public class TokenManager
    {
        private const char Sep = '#';
        private readonly TimeType _timeType;

        public TokenManager(TimeType timeType)
        {
            _timeType = timeType;
        }

        public TokenManager()
        {
            _timeType = TimeType.Minute;
        }

        private string secretKey => ConfigHelper.GetPassPhrase();

        public DateTime Now => DateTime.UtcNow;

        /// <summary>
        ///  Token Schema (Time # NameId # Name # Guid) Time is Utc!
        /// </summary>
        /// <param name="nameId">Unique Identity</param>
        /// <param name="name">User login name</param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public string GenerateTokenKey(string nameId, string name, string appId)
        {
            if (string.IsNullOrWhiteSpace(nameId))
                throw new ArgumentNullException(nameof(nameId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(appId))
                throw new ArgumentNullException(nameof(appId));

            //Now
            var time = Now.ToString(CultureInfo.InvariantCulture);
            var expire = GetExpire(Now);
            //Random Uniqe Key
            var guid = Guid.NewGuid();
            var key = guid.ToString();



            var extraheaders = new Dictionary<string, object>()
            {
                {"time", time},
                {"guid", key},
                {"appId", appId },
                {"exp",expire }
            };

            var payload = new Payload()
            {
                UserName = name,
                UserNameId = nameId
            };

            //Create Token



            var token = JsonWebToken.Encode(extraheaders, payload, secretKey, JwtHashAlgorithm.HS256); //CryptoHelper.Encrypt(sb.ToString(), PassPhrase);

            return token;
        }

        public bool TokenIsActive(DateTime expire)
        {
            return expire > Now;
        }

        public bool TokenIsActive(string tokenKey)
        {
            return TokenIsActive(tokenKey, TokenLiveDuration, _timeType);
        }

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="timeType">Hour, Minute, Second</param>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public bool TokenIsActive(string tokenKey, int timeDuration, TimeType timeType)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(tokenKey))
            {
                throw new ArgumentNullException(nameof(tokenKey));
            }

            try
            {

                var payload = JsonWebToken.Decode(tokenKey, secretKey);

                //Time
                DateTime time

                if (!DateTime.TryParse(data., out time))
                {
                    throw new ArgumentException("Datetime is invalid");
                }
                //Calculate Expire
                var expire = GetExpire(time);

                //Check Time
                result = expire > Now;

            }
            catch (Exception ex)
            {
                // ignored
            }
            return result;
        }

        public DateTime GetExpire(DateTime time)
        {
            return TokenHelper.CalculateExpire(TokenLiveDuration, _timeType, time);
        }

        public string GenerateAccessKey()
        {
            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), PassPhrase);
        }


    }
}
