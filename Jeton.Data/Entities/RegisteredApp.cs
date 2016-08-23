using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Entities
{
    public class RegisteredApp : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appName"></param>
        public RegisteredApp(string appName)
        {
            AppName = appName;
        }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(200)]
        public string AccessKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [MaxLength(100)]
        public string AppName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime Created { get; set; }
    }
}
