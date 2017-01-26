using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Sdk.ServiceModels
{
    public class TokenResponse<T>
    {
        public bool Status { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
