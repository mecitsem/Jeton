using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Sdk
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.5.0.0")]
    public partial class JetonToken
    {
        [Newtonsoft.Json.JsonProperty("AccessToken", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string AccessToken { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static JetonToken FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JetonToken>(data);
        }
    }
}
