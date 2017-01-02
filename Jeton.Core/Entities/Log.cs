using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Entities
{
    public class Log
    {
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
