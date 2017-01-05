using System;
using System.Text;
using Jeton.Core.Common;
using Jeton.Core.Helpers;

namespace Jeton.Core.Managers
{
    public class AccessKeyManager
    {
        private static readonly Constants.HashAlgorithm DefaultHashAlgorithm;
        private static readonly byte[] SaltBytes = Encoding.UTF8.GetBytes(Constants.Security.SaltString);

        static AccessKeyManager()
        {
            DefaultHashAlgorithm = Constants.HashAlgorithm.SHA256;
        }

        public static string Generate(string text)
        {
            if (SaltBytes == null)
                throw new ArgumentNullException(nameof(SaltBytes));

            return CryptoHelper.EncodeBase64(CryptoHelper.ComputeHash(text, DefaultHashAlgorithm, SaltBytes));
        }
    }
}
