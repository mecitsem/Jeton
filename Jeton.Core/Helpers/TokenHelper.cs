using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <returns></returns>
        public static DateTime CalculateExpire(int timeDuration, TimeType timeType)
        {
            var time = DateTime.Now;
            switch (timeType)
            {
                case TimeType.Hour:
                    time.AddHours(timeDuration);
                    break;
                case TimeType.Minute:
                    time.AddMinutes(timeDuration);
                    break;
                case TimeType.Second:
                    time.AddSeconds(timeDuration);
                    break;
                default:
                    time.AddHours(timeDuration);
                    break;
            }
            return time;
        }
    }
}
