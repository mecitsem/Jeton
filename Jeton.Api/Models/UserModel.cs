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
        public string NameId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}