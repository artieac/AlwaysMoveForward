using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class AuthorizationCodeRepository : IdentityServer4.Stores.IAuthorizationCodeStore
    {
        public Task<AuthorizationCode> GetAuthorizationCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAuthorizationCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreAuthorizationCodeAsync(AuthorizationCode code)
        {
            throw new NotImplementedException();
        }
    }
}
