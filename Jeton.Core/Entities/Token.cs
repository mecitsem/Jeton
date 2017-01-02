using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Entities
{
    public class Token : BaseEntity
    {
        public string TokenKey { get; set; }
        public DateTime? Expire { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid AppId { get; set; }
        public virtual App App { get; set; }
    }
}
