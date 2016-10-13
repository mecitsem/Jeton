using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Api.DTOs
{
    public class TokenActiveDTO
    {
        public bool IsActive { get; set; }
        public string UserName { get; set; }
        public string UserNameId { get; set; }
    }
}