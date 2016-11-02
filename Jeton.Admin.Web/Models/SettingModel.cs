using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jeton.Core.Common;

namespace Jeton.Admin.Web.Models
{
    public class SettingModel
    {
        public Guid SettingID { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Constants.ValueType ValueType { get; set; }
        public bool IsEssential { get; set; }
        public string Description { get; set; }
    }
}