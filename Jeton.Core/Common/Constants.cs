using System;

namespace Jeton.Core.Common
{
    public class Constants
    {
        public const int TokenLiveDuration = 24;
        public const string AccessKey = "AccessKey";
        public const string UserName = "UserName";
        public const string UserNameId = "UserNameId";
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public static readonly DateTime Now = DateTime.UtcNow;

        public class Settings
        {
            public const string SecretKey = "secret_key";
            public const string CheckExpireFrom = "check_expire_from";
            public const string TokenDuration = "token_duration";
        }


        public class Security
        {
            public static readonly string SaltString = "Jeton";
        }

        public enum AppSettings
        {
            PassPhrase
        }

        public enum CheckExpireFrom
        {
            Database = 0,
            Token = 1
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
