namespace Jeton.Core.Common
{
    public class Constants
    {
        public const int TokenLiveDuration = 24;
        public const string AccessKey = "AccessKey";
        public const string UserName = "UserName";
        public const string UserNameId = "UserNameId";


        public class Settings
        {
            public const string SecretKey = "secret_key";
            public const string CheckExpireFrom = "check_expire_from";
            public const string TokenDuration = "token_duration";
        }


        public enum AppSettings
        {
            PassPhrase
        }

        public enum TimeType
        {
            Second = 1,
            Minute = 2,
            Hour = 3
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
    }
}
