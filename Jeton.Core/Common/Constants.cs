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

        public enum TimeType : int
        {
            Second = 1,
            Minute = 2,
            Hour = 3
        }
    }
}
