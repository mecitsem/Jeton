﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Models
{
    public class Payload
    {
        public string UserName { get; set; }
        public string UserNameId { get; set; }
        public int Time { get; set; }
        public Guid RootAppId { get; set; }
        public int Expire { get; set; }
    }
}
