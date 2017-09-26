using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    class ClientRepository : IdentityServer4.Stores.IClientStore
    {
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }
    }
}
