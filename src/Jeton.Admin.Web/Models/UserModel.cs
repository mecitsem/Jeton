using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameId { get; set; }
        public string Created { get; set; }
        public string Modified { get; set; }
    }
}