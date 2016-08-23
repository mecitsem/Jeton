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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IJetonService
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        string UserLogin(string userName, byte[] spTokenBinaryArray);

        [OperationContract]
        bool IsValidateUserByHeader();

        [OperationContract]
        bool IsValidateUser(string appId, string userToken);
        // TODO: Add your service operations here
    }


}
