using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Entities
{
    public class GeneratedToken : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Expire { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int JetonUserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("JetonUserId")]
        public virtual JetonUser JetonUser { get; set; }

    }
}
