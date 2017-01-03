using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jeton.Core.Entities;

namespace Jeton.Core.Interfaces.Services
{
    public interface IAppService
    {
        //IsActive
        bool IsActive(App app);
        Task<bool> IsActiveAsync(App app);
        
        //IsExist
        bool IsExist(Guid appId);
        Task<bool> IsExistAsync(Guid appId);
        
        //GetApps
        IEnumerable<App> GetApps();
        Task<IEnumerable<App>> GetAppsAsync();
        
        //GetAppById
        App GetAppById(Guid appId);
        Task<App> GetAppByIdAsync(Guid appId);
        
        //GetAppByName
        App GetAppByName(string appName);
        Task<App> GetAppByNameAsync(string appName);
        
        //Insert
        App Insert(App app);
        Task<App> InsertAsync(App app);
        
        //Update
        void Update(App app);
        Task UpdateAsync(App app);
        
        //Delete
        void Delete(Guid appId);
        Task DeleteAsync(Guid appId);

        //Generate AccessKey
        string GenerateAccessKey();

 
    
       
       
  
      
    }
}
