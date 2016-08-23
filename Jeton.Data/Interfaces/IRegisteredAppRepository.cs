using Jeton.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Data.Interfaces
{
    public interface IRegisteredAppRepository : IDisposable
    {
        IEnumerable<RegisteredApp> GetRegisteredApps();
        RegisteredApp GetRegistedAppById(int id);
        RegisteredApp GetRegistedAppByName(string appName);
        RegisteredApp GetRegistedAppByAccessKey(string accessKey);
        RegisteredApp Add(RegisteredApp registeredApp);
        RegisteredApp Update(int id, RegisteredApp registeredApp);
        void Delete(int id);
        void SaveChanges();
    }
}
