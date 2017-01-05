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
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [DisplayName("Access Key")]
        public string AccessKey { get; set; }

        [DisplayName("Name")]
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [DisplayName("Is Root?")]
        public bool IsRoot { get; set; }
    }
}