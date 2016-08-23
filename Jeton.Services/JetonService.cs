using Jeton.Common.Helpers;
using Jeton.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class JetonService : IJetonService
    {
        string UserToken = string.Empty;

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public bool IsValidateUser(string appId, string userToken)
        {
            throw new NotImplementedException();
        }

        public bool IsValidateUserByHeader()
        {
            if (OperationContext.Current.IncomingMessageHeaders.FindHeader("TokenHeader", "TokenNameSpace") == -1)
            {
                return false;
            }

            string userIdentityToken = Convert.ToString(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace"));

            return userIdentityToken == UserToken;

        }

        public string UserLogin(string userName, byte[] spTokenBinaryArray)
        {
            if (!string.IsNullOrEmpty(userName) && spTokenBinaryArray != null)
            {
                UserToken = SecurityHelper.GetBase64String(spTokenBinaryArray);
            }
            else
                UserToken = string.Empty;

            return UserToken;
        }


    }
}
