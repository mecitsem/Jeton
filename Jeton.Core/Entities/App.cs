using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Entities
{
    /// <summary>
    /// Registered Apps
    /// </summary>
    public class App : BaseEntity
    {
        public string AccessKey { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; }

    }
}
