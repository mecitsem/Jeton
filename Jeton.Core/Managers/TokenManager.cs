using System;
using System.Collections.Generic;
using System.Globalization;
using Jeton.Core.Common;
using Jeton.Core.Helpers;
using Jeton.Core.Models;
using Jeton.Jwt.Core;

namespace Jeton.Core.Managers
{
    /// <summary>
    ///  If you implement System.IdentityModel.Tokens.Jwt or different crypto system, you should chgange only this place
    /// </summary>
    public class TokenManager
    {

        public TokenManager(string secretKey)
        {
            TokenDuration = Constants.TokenLiveDuration;
            SecretKey = secretKey;
        }

        #region Properties

        public string SecretKey { get; private set; }
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

            if (string.IsNullOrWhiteSpace(SecretKey))
                throw new ArgumentNullException(nameof(SecretKey));

            //Now
            var now = Constants.Now;
            var time = now.ToString(CultureInfo.InvariantCulture);
            var expire = CalculateExpireDateTime(now);

            var extraheaders = new Dictionary<string, object>()
            {
                {"time",time },
                {"exp",expire }
            };


            //Create Token
            var token = JsonWebToken.Encode(extraheaders, payload, SecretKey, JwtHashAlgorithm.HS256); //JWT

            return token;
        }

        public DateTime CalculateExpireDateTime(DateTime time)
        {
            return TokenHelper.CalculateExpire(TokenDuration, time);
        }

        public string GenerateAccessKey()
        {
            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), SecretKey);
        }


        public bool IsExpired(DateTime expireDateTime)
        {
            return expireDateTime.IsExpired();
        }

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="tokenKey"></param>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public bool IsExpired(string tokenKey, int timeDuration, string secretKey)
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

        public bool IsVerified(string tokenKey, string secretKey)
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
