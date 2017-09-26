using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    class SomeRepository : IdentityServer4.Stores.I
    {
        public Task<Token> GetReferenceTokenAsync(string handle)
        {
            throw new NotImplementedException();
        }

        public Task<Consent> GetUserConsentAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReferenceTokenAsync(string handle)
        {
            throw new NotImplementedException();
        }

        public Task RemoveReferenceTokensAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveUserConsentAsync(string subjectId, string clientId)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreReferenceTokenAsync(Token token)
        {
            throw new NotImplementedException();
        }

        public Task StoreUserConsentAsync(Consent consent)
        {
            throw new NotImplementedException();
        }
    }
}
