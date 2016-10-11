using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jeton.Core.Entities;
using Jeton.Data.Repositories.TokenRepo;
using Jeton.Data.Infrastructure.Interfaces;
using Jeton.Core.Helpers;
using Jeton.Core.Common;

namespace Jeton.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IUnitOfWork unitOfWork;

        public TokenService(ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
        {
            this.tokenRepository = tokenRepository;
            this.unitOfWork = unitOfWork;
        }

        #region CREATE
        public void Insert(Token token)
        {
            tokenRepository.Add(token);
        }
        #endregion

        #region READ
        public Token GetTokenById(Guid tokenId)
        {
            return tokenRepository.GetTokenById(tokenId);
        }

        public Token GetTokenByKey(string tokenKey)
        {
            return tokenRepository.GetTokenByKey(tokenKey);
        }

        public Token GetTokenByUser(User user)
        {
            return tokenRepository.GetTokenByUser(user);
        }

        public Token GetTokenByUserID(Guid userId)
        {
            return tokenRepository.GetTokenByUserId(userId);
        }

        public IEnumerable<Token> GetTokens()
        {
            return tokenRepository.GetTokens();
        }

        public IEnumerable<Token> GetLiveTokens()
        {
            return tokenRepository.GetLiveTokens();
        }
        #endregion

        #region UPDATE
        public void Update(Token token)
        {
            tokenRepository.Update(token);
        }
        #endregion

        #region DELETE
        public void Delete(Token token)
        {
            tokenRepository.Delete(token);
        }
        #endregion


        public void Save()
        {
            unitOfWork.Commit();
        }

        public bool IsExist(string tokenKey)
        {
            return tokenRepository.IsExist(tokenKey);
        }

        public bool IsExistByUser(User user)
        {
            return tokenRepository.GetTokenByUser(user) != null;
        }

        
    }
}
