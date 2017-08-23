using System;

namespace Jeton.Core.Helpers
{
    public class TokenHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime CalculateExpire(int timeDuration, DateTime time)
        {
            var expire = time.AddMinutes(timeDuration);

            return expire;
        }
    }
}
