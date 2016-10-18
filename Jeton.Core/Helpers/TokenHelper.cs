using System;
using static Jeton.Core.Common.Constants;

namespace Jeton.Core.Helpers
{
    public class TokenHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeDuration"></param>
        /// <param name="timeType"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime CalculateExpire(int timeDuration, TimeType timeType, DateTime time)
        {
            DateTime expire;
            switch (timeType)
            {
                case TimeType.Hour:
                    expire = time.AddHours(timeDuration);
                    break;
                case TimeType.Minute:
                    expire = time.AddMinutes(timeDuration);
                    break;
                case TimeType.Second:
                    expire = time.AddSeconds(timeDuration);
                    break;
                default:
                    expire = time.AddMinutes(timeDuration);
                    break;
            }
            return expire;
        }

       
    }
}
