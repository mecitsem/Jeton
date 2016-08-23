using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Common.Helpers
{
    public class SecurityHelper
    {
        public static string GetBase64String(byte[] array)
        {
            return Convert.ToBase64String(array);
        }

        public static string GetBase64String(byte[] array, Base64FormattingOptions options)
        {
            return Convert.ToBase64String(array, options);
        }
    }
}
