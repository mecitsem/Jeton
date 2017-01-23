using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.Models
{
    public class JsonModel
    {
        public bool Result { get; set; }
        public string Error { get; set; }
        public object Data { get; set; }
    }
}