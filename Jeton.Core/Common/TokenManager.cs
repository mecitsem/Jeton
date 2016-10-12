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
            var date = DateTime.UtcNow;
            var time = date.ToString();

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

   

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="token">Token Schema  (Time # NameId # Name # Guid)</param>
        /// <param name="timeType">Hour, Minute, Second</param>
        /// <returns></returns>
        public bool TokenIsLive(string token, int timeDuration, TimeType timeType)
        {
            bool result = false; ;

            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException("Token is null");
            }

            try
            {
                var passPhrase = ConfigHelper.GetPassPhrase();
                var data = CryptoManager.Decrypt(token, passPhrase);
                //Time
                var time = default(DateTime);
    
                if (!DateTime.TryParse(data.Split(SEP)[0], out time))
                {
                    throw new ArgumentException("Datetime is invalid");
                }

                var now = DateTime.UtcNow;
                
                //Calculate Expire
                var expire = GetExpire();
                
                //Check Time
                result = expire > now;

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public DateTime GetExpire()
        {
            return TokenHelper.CalculateExpire(TokenLiveDuration, timeType);
        }
    }
}
