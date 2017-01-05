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
            CheckExpireFrom = Constants.CheckExpireFrom.Database;
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

        public Constants.CheckExpireFrom CheckExpireFrom { get; set; }

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
            var token = JsonWebToken.Encode(extraheaders, payload, SecretKey, JwtHashAlgorithm.HS256); //JWT

            return token;
        }

        public DateTime GetExpire(DateTime time)
        {
            return TokenHelper.CalculateExpire(TokenDuration, time);
        }

        public string GenerateAccessKey()
        {
            return CryptoHelper.Encrypt(Guid.NewGuid().ToString(), SecretKey);
        }



       
    }
}
