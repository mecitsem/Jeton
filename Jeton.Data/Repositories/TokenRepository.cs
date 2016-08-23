using Jeton.Common.Helpers;
using Jeton.Data.Data;
using Jeton.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeton.Business.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private JetonDBContext _context;

        public TokenRepository()
        {
            if (_context == null) _context = new JetonDBContext();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appAccessKey"></param>
        /// <param name="userLoginName"></param>
        /// <param name="spTokenBinary"></param>
        /// <returns></returns>
        public string GenerateToken(string appAccessKey, string userLoginName, string userNameId, byte[] spTokenBinary)
        {
            string token = string.Empty;
            if (string.IsNullOrEmpty(appAccessKey) || string.IsNullOrEmpty(userLoginName) || spTokenBinary == null)
            {
                throw new ArgumentNullException("Please check paramaters");
            }

            //Check AppAccess Key
            if (!_context.RegisteredApps.Any(a => a.AccessKey.Equals(appAccessKey)))
            {
                throw new ArgumentException("The AppAccessKey is invalid");
            }

            //Global Variables
            var now = DateTime.Now;
            var generatedToken = SecurityHelper.GetBase64String(spTokenBinary);

            //Check User
            if (_context.JetonUsers.Any(u => u.Name.ToLowerInvariant().Equals(userLoginName.ToLowerInvariant()))) //Exist User
            {
                //GetUser
                var user = _context.JetonUsers.FirstOrDefault(u => u.Name.ToLowerInvariant().Equals(u.Name.ToLowerInvariant()));


                //Add Token
                _context.GeneratedTokes.Add(new Data.Entities.GeneratedToken()
                {
                    Created = now,
                    Expire = now.AddHours(1),
                    Token = generatedToken,
                    JetonUser = user,
                    JetonUserId = user.Id
                });

                //Save
                var saveResult = _context.SaveChanges();
                if (saveResult > 0)
                    token = generatedToken;
            }
            else // New User
            {
                //Create User Info
                var newUser = _context.JetonUsers.Add(new Data.Entities.JetonUser()
                {
                    Created = now,
                    Name = userLoginName,
                    NameId = userNameId
                });
                
                //Save User
                _context.SaveChanges();

                //Add Token
                _context.GeneratedTokes.Add(new Data.Entities.GeneratedToken()
                {
                    Created = now,
                    Expire = now.AddHours(1),
                    Token = generatedToken,
                    JetonUser = newUser,
                    JetonUserId = newUser.Id
                });

                //Save
                var saveResult = _context.SaveChanges();
                if (saveResult > 0)
                    token = generatedToken;
            }

            return token;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="appAccessKey"></param>
        /// <param name="jetonToken"></param>
        /// <returns></returns>
        public bool IsValidatedUser(string appAccessKey, string jetonToken)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        private bool _disposed;
        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
