using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Core.Common
{
    public class Constants
    {
        public const int TokenLiveDuration = 24;
        public const string AccessKey = "AccessKey";
        public const string UserName = "UserName";
        public const string UserNameId = "UserNameId";



        public enum AppSettings
        {
            PassPhrase
        }

        public enum TimeType : int
        {
            Second = 1,
            Minute = 2,
            Hour = 3
        }
    }
}
