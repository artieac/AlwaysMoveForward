using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityModel;

namespace AlwaysMoveForward.OAuth2.Web.Code
{
    public class AMFResourceStore : IResourceStore
    {
        static IList<ApiResource> resources = new List<ApiResource>();

        static AMFResourceStore()
        {
            AMFResourceStore.resources = new List<ApiResource>();

            ApiResource newItem = new ApiResource
            {
                Name = "api1",

                // secret for using introspection endpoint
                ApiSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // include the following using claims in access token (in addition to subject id)
                UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email },

                // this API defines two scopes
                Scopes =
                {
                    new Scope()
                    {
                        Name = "api1.full_access",
                        DisplayName = "Full access to API 2",
                    },
                    new Scope
                    {
                        Name = "api1.read_only",
                        DisplayName = "Read only access to API 2"
                    }
                }
            };

            AMFResourceStore.resources.Add(newItem);
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource retVal = null;

            for(int i = 0; i < AMFResourceStore.resources.Count; i++)
            {
                if(AMFResourceStore.resources[i].Name == name)
                {
                    retVal = AMFResourceStore.resources[i];
                }
            }

            return Task.FromResult(retVal);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IList<ApiResource> retVal = new List<ApiResource>();

            foreach(string scopeName in scopeNames)
            {
                for (int i = 0; i < AMFResourceStore.resources.Count; i++)
                {
                    foreach(Scope scope in AMFResourceStore.resources[i].Scopes)
                    {
                        if (scope.Name == scopeName)
                        {
                            retVal.Add(AMFResourceStore.resources[i]);
                        }
                    }
                }
            }

            return Task.FromResult(retVal as IEnumerable<ApiResource>);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(Config.GetIdentityResources());
        }

        public Task<Resources> GetAllResources()
        {
            Resources retVal = new Resources();
            retVal.ApiResources = AMFResourceStore.resources;
            retVal.IdentityResources = Config.GetIdentityResources().ToArray();
            return Task.FromResult(retVal);
        }
    }
}
