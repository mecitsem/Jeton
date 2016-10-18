using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.ViewModel
{
    public class AppViewModel
    {
        [DisplayName("AccessKey")]
        public string AccessKey { get; set; }

        [DisplayName("AppName")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [DisplayName("Is Root App?")]
        public bool IsRoot { get; set; }
    }
}