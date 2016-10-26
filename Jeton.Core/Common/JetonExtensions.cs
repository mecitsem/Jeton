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
    }
}
