using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Entities
{
    public class JetonUser : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [MaxLength(400)]
        public string NameId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual List<GeneratedToken> Tokens { get; set; }
    }
}
