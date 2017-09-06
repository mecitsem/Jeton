using System;

namespace Jeton.Core.Common
{
    public class Constants
    {
        public const int TokenLiveDuration = 24;
        public const string ApiKey = "apiKey";
        public const string UserName = "UserName";
        public const string UserNameId = "UserNameId";
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Now = DateTime.UtcNow;
        public static readonly string Application = "Jeton";
        public class Settings
        {
            public const string SecretKey = "secretKey";
            public const string TokenDuration = "tokenDuration";
        }


        public class Security
        {
            public static readonly string SaltString = "Jeton";
        }

        public enum AppSettings
        {
            PassPhrase
        }

        public enum ValueType
        {
            Integer = 0,
            String = 1,
            Datetime = 2,
            Boolean = 3,
        }

        public enum HashAlgorithm
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }
    }
}
