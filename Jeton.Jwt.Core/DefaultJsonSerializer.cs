using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Jeton.Jwt.Core
{
    /// <summary>
    /// JSON Serializer using JavaScriptSerializer
    /// </summary>
    public class DefaultJsonSerializer : IJsonSerializer
    {
        private readonly JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// Serialize an object to JSON string
        /// </summary>
        /// <param name="obj">object</param>
        /// <returns>JSON string</returns>
        public string Serialize(object obj)
        {
            return serializer.Serialize(obj);
        }

        /// <summary>
        /// Deserialize a JSON string to typed object.
        /// </summary>
        /// <typeparam name="T">type of object</typeparam>
        /// <param name="json">JSON string</param>
        /// <returns>typed object</returns>
        public T Deserialize<T>(string json)
        {
            return serializer.Deserialize<T>(json);
        }
    }
}
