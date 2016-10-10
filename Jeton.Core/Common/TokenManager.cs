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
        private const string SEP = "#";
        private TimeType timeType;

        public TokenManager(TimeType timeType)
        {
            this.timeType = timeType;
        }

        /// <summary>
        ///  Token Schema (Time # NameId # Name # Guid)
        /// </summary>
        /// <param name="nameId">Unique Identity</param>
        /// <param name="name">User login name</param>
        /// <returns></returns>
        public string GenerateToken(string nameId, string name)
        {
            if (string.IsNullOrWhiteSpace(nameId))
            {
                throw new ArgumentNullException("NameId parameter is null!");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("Name parameter is null!");
            }


            //NameId
            var userKey = Encoding.ASCII.GetBytes(nameId);

            //LoginName
            var userName = Encoding.ASCII.GetBytes(name);

            //Now
            var now = DateTime.Now;
            var time = BitConverter.GetBytes(now.ToBinary());

            //Unique Key
            var guid = Guid.NewGuid();
            var key = guid.ToByteArray();

            var seperator = Encoding.ASCII.GetBytes(SEP);

            //Token Binary Array
            var tokenArray = time.Concat(seperator)     // Now #
                                 .Concat(userKey)       // NameId
                                 .Concat(seperator)     // #
                                 .Concat(userName)      // Login Name
                                 .Concat(seperator)     // #
                                 .Concat(key)           // Guid
                                 .ToArray();
            //Create Token
            var token = Convert.ToBase64String(tokenArray);

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

                var tokenData = Convert.FromBase64String(token);

                //Time
                var time = DateTime.FromBinary(BitConverter.ToInt64(tokenData, 0));
            
                var now = DateTime.Now;
                
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
