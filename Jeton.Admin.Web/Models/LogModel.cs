using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.Models
{
    public class LogModel
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Machine { get; set; }
        public string RequestIpAddress { get; set; }
        public string RequestMethod { get; set; }
        public int ResponseStatusCode { get; set; }
    }
}