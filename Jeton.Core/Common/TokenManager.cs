using Jeton.Core.Common;
using Jeton.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jeton.Core.Common.Constants;

namespace Jeton.Core.Common
{
    public class TokenManager
    {
        private const char SEP = '#';
        private TimeType timeType;

        public TokenManager(TimeType timeType)
        {
            this.timeType = timeType;
        }

        public TokenManager()
        {
            this.timeType = TimeType.Minute;
        }

        public DateTime Now { get { return DateTime.UtcNow; } }

        /// <summary>
        ///  Token Schema (Time # NameId # Name # Guid) Time is Utc!
        /// </summary>
        /// <param name="nameId">Unique Identity</param>
        /// <param name="name">User login name</param>
        /// <returns></returns>
        public string GenerateTokenKey(string nameId, string name)
        {
            if (string.IsNullOrWhiteSpace(nameId))
            {
                throw new ArgumentNullException("NameId parameter is null!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name parameter is null!");
            }


            //Now
            var time = Now.ToString();

            //Random Uniqe Key
            var guid = Guid.NewGuid();
            var key = guid.ToString();


            //Token Binary Array
            var sb = new StringBuilder();
            sb.Append(time);
            sb.Append(SEP);
            sb.Append(nameId);
            sb.Append(SEP);
            sb.Append(name);
            sb.Append(SEP);
            sb.Append(key);
            //Create Token

            var passPhrase = ConfigHelper.GetPassPhrase();

            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentNullException("passPhrase");

            var token = CryptoManager.Encrypt(sb.ToString(), passPhrase);

            return token;
        }

        public bool TokenIsLive(DateTime expire)
        {
            return expire > Now;
        }

        public bool TokenIsLive(string tokenKey)
        {
            return TokenIsLive(tokenKey, Constants.TokenLiveDuration, timeType);
        }

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="token">Token Schema  (Time # NameId # Name # Guid)</param>
        /// <param name="timeType">Hour, Minute, Second</param>
        /// <returns></returns>
        public bool TokenIsLive(string tokenKey, int timeDuration, TimeType timeType)
        {
            bool result = false; ;

            if (string.IsNullOrWhiteSpace(tokenKey))
            {
                throw new ArgumentNullException("Token is null");
            }

            try
            {
                var passPhrase = ConfigHelper.GetPassPhrase();
                var data = CryptoManager.Decrypt(tokenKey, passPhrase);
                //Time
                var time = default(DateTime);

                if (!DateTime.TryParse(data.Split(SEP)[0], out time))
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

            }
            return result;
        }

        public DateTime GetExpire(DateTime time)
        {
            return TokenHelper.CalculateExpire(TokenLiveDuration, timeType, time);
        }
    }
}
