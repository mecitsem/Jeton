using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Jeton.Core.Common;

namespace Jeton.Admin.Web.ViewModel
{
    public class SettingViewModel
    {
        [ScaffoldColumn(false)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(5000)]
        public string Value { get; set; }

        [Required]
        public Constants.ValueType ValueType { get; set; }

        public bool IsEssential { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
    }
}

