using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Common;

namespace Jeton.Core.Entities
{
    public class Setting : BaseEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public Constants.ValueType ValueType { get; set; }
        public bool IsEssential { get; set; }
        public string Description { get; set; }
    }


}
