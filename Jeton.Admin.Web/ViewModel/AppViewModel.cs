using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.ViewModel
{
    public class AppViewModel
    {
        public string AccessKey { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; }
    }
}