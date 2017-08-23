using System;

namespace Jeton.Core.Common
{
    public static class JetonExtensions
    {
        public static bool TryGetValue<T>(string fieldValue, out T value)
        {
            value = default(T);
            try
            {
                value = (T)Convert.ChangeType(fieldValue, typeof(T));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static DateTime BeginDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static DateTime EndDateTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
        }
    }
}
