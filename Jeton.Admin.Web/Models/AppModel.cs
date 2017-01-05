using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.Models
{
    public class AppModel
    {
        public Guid Id { get; set; }
        public string AccessKey { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public bool IsRoot { get; set; }
        public bool IsDeleted { get; set; }
    }
}