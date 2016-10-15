using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Jeton.Api.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetHeaderValue(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> values;
            var found = request.Headers.TryGetValues(key, out values);
            return found ? values.FirstOrDefault() : null;
        }

        public static bool HeaderKeyIsExist(this HttpRequestMessage request, string key)
        {
            IEnumerable<string> values;
            return request.Headers.TryGetValues(key, out values);
        }
    }
}