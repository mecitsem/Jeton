using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jeton.Api.Models
{
    public class UserModel
    {
        [Required]
        public string UserNameId { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}