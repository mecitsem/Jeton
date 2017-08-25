using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jeton.Api.Models
{
    public class TokenModel
    {
        [Required]
        public string AccessToken { get; set; }
    }
}