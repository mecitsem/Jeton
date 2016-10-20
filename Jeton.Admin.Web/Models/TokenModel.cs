using Jeton.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Jeton.Admin.Web.Models
{
    public class TokenModel
    {
        public Guid TokenID { get; set; }
        public string TokenKey { get; set; }
        public DateTime Expire { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public Guid UserID { get; set; }
        public Guid AppID { get; set; }
        public int Status
        {
            get
            {
                if (Expire <= DateTime.UtcNow)
                    return 0;

                var time = Expire - DateTime.UtcNow;

                return time.Minutes * 100 / Constants.TokenLiveDuration;
            }
        }
    }
}