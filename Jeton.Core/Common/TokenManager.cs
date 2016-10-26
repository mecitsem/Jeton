using Jeton.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Jeton.Core.Entities;
using Jeton.Core.Models;
using JWT;
using static Jeton.Core.Common.Constants;

namespace Jeton.Core.Common
{
    public class TokenManager
    {

        public TokenManager(TimeType timeType)
        {
            TimeType = timeType;
        }

        public TokenManager()
        {
            TimeType = TimeType.Minute;
            CheckExpireFrom = CheckExpireFrom.Database;
            TokenDuration = TokenLiveDuration;
        }

        #region Properties
        public TimeType TimeType { get; set; }
        public static string SecretKey => ConfigHelper.GetPassPhrase();
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private int _tokenDuration;
        public int TokenDuration
        {
            get { return _tokenDuration; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException($"Token Duration must be greather than 0");
                _tokenDuration = value;
            }
        }

        public CheckExpireFrom CheckExpireFrom { get; set; }

        public DateTime Now => DateTime.UtcNow;

        #endregion



        /// <summary>
        ///  Token Schema (Time # NameId # Name # Guid) Time is Utc!
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public string GenerateTokenKey(Payload payload)
        {
            if (payload == null)
                throw new ArgumentNullException(nameof(payload));

            //Now
            var now = Now;
            var time = now.ToString(CultureInfo.InvariantCulture);
            var expire = GetExpire(now);
            //Random Uniqe Key
            var guid = Guid.NewGuid();
            var key = guid.ToString();



            var extraheaders = new Dictionary<string, object>()
            {
                {"time",time },
                {"guid", key},
                {"rootappId", payload.RootAppId },
                {"exp",expire }
            };
            //Create Token

            //var token = CryptoHelper.Encrypt(sb.ToString(), SecretKey);
            var token = JsonWebToken.Encode(extraheaders, payload, SecretKey, JwtHashAlgorithm.HS256); //JWT

            return token;
        }


        public bool TokenIsExpired(DateTime expire)
        {
            var nowUnixTimestamp = GetUnixTimeStamp(Now);
            var expireUnixTimestamp = GetUnixTimeStamp(expire);
            return nowUnixTimestamp > expireUnixTimestamp;
        }

        public bool TokenIsExpired(Token token)
        {
            if(token == null)
                throw new ArgumentNullException(nameof(token));

            if(!token.Expire.HasValue)
                throw new ArgumentException("Token expire is null");

            bool result;

            switch (CheckExpireFrom)
            {
                case CheckExpireFrom.Database:
                    result = TokenIsExpired(token.Expire.Value);
                    break;
                case CheckExpireFrom.Token:
                    result = TokenIsExpired(token.TokenKey, TokenDuration, TimeType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="timeType">Hour, Minute, Second</param>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public bool TokenIsExpired(string tokenKey, int timeDuration, TimeType timeType)
        {
            var result = false;

            if (string.IsNullOrWhiteSpace(tokenKey))
                throw new ArgumentNullException(nameof(tokenKey));


            try
            {
                var payload = JsonWebToken.DecodeToObject<Payload>(tokenKey, SecretKey);

                if (payload == null)
                    throw new ArgumentException("Datetime is invalid");

                var nowUnixTimestamp = GetUnixTimeStamp(Now);

                result = nowUnixTimestamp > payload.Expire;
            }
            catch (Exception ex)
            {
                // ignored
            }
            return result;
        }

        public DateTime GetExpire(DateTime time)
        {
            return TokenHelper.CalculateExpire(TokenDuration, TimeType, time);
        }

        public string GenerateAccessKey()
        {
            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), SecretKey);
        }

        public int GetUnixTimeStamp(DateTime dateTime)
        {
           return (int)Math.Round((dateTime - UnixEpoch).TotalSeconds);
        }
    }
}
