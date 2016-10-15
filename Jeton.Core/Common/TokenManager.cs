using Jeton.Core.Helpers;
using System;
using System.Globalization;
using System.Text;
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

        private string PassPhrase => ConfigHelper.GetPassPhrase();

        public DateTime Now => DateTime.UtcNow;

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
                throw new ArgumentNullException(nameof(nameId));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }


            //Now
            var time = Now.ToString(CultureInfo.InvariantCulture);

            //Random Uniqe Key
            var guid = Guid.NewGuid();
            var key = guid.ToString();


            //Token Binary Array
            var sb = new StringBuilder();
            sb.Append(time);
            sb.Append(Sep);
            sb.Append(nameId);
            sb.Append(Sep);
            sb.Append(name);
            sb.Append(Sep);
            sb.Append(key);
            //Create Token



            var token = CryptoHelper.Encrypt(sb.ToString(), PassPhrase);

            return token;
        }

        public bool TokenIsLive(DateTime expire)
        {
            return expire > Now;
        }

        public bool TokenIsLive(string tokenKey)
        {
            return TokenIsLive(tokenKey, TokenLiveDuration, _timeType);
        }

        /// <summary>
        /// Check token alive
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="timeType">Hour, Minute, Second</param>
        /// <param name="tokenKey"></param>
        /// <returns></returns>
        public bool TokenIsLive(string tokenKey, int timeDuration, TimeType timeType)
        {
            bool result = false;

            if (string.IsNullOrWhiteSpace(tokenKey))
            {
                throw new ArgumentNullException(nameof(tokenKey));
            }

            try
            {

                var data = CryptoHelper.Decrypt(tokenKey, PassPhrase);
                //Time
                DateTime time;

                if (!DateTime.TryParse(data.Split(Sep)[0], out time))
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
