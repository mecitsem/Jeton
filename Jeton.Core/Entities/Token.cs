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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTime? Expire { get; set; }

        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}
