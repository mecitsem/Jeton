using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Helpers
{
    public class JetonEnumHelper
    {
        public static T GetEnumValue<T>(string stringValue) where T : struct
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
                throw new Exception("T must be an enum type.");

            T val;
            return Enum.TryParse(stringValue, true, out val) ? val : default(T);
        }

        public static T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            var enumType = typeof(T);

            if(!enumType.IsEnum)
                throw new Exception("T must be an enum type.");

            return (T) Enum.ToObject(enumType, intValue);
        }

    }
}
