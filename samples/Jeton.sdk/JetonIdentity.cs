using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Sdk
{
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.5.0.0")]
    public partial class JetonIdentity
    {
        [Newtonsoft.Json.JsonProperty("UserNameId", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string UserNameId { get; set; }

        [Newtonsoft.Json.JsonProperty("UserName", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        public string UserName { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static JetonIdentity FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<JetonIdentity>(data);
        }
    }
}
