using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityModel;
using IdentityServer4;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;

namespace AlwaysMoveForward.OAuth2.Web.Code.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        static IList<ApiResource> resources = null;
        static IList<IdentityResource> identityResources = new List<IdentityResource>();

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityServer4.Models.IdentityResources.OpenId(),
                new IdentityServer4.Models.IdentityResources.Email(),
                new IdentityServer4.Models.IdentityResources.Profile()
            };
        }

        static ResourceStore()
        {
            ResourceStore.resources = new List<ApiResource>();

            ApiResource newItem = new ApiResource
            {
                Name = "api1",

                // secret for using introspection endpoint
                ApiSecrets =
                {
                    new Secret("abcd".Sha256())
                },

                // include the following using claims in access token (in addition to subject id)
                UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Email, JwtClaimTypes.Role },

                // this API defines two scopes
                Scopes =
                {
                    new Scope("api1.full_access", "Full access to the API"),
                    new Scope("api1.read_only", "Read Only access to the API")
                }
            };

            ResourceStore.resources.Add(newItem);
        }

        public ResourceStore(ServiceManagerBuilder serviceManagerBuilder)
        {
            this.ServiceManagerBuilder = serviceManagerBuilder;
        }

        private ServiceManagerBuilder ServiceManagerBuilder { get; set; }

        private IServiceManager serviceManager = null;

        private IServiceManager ServiceManager
        {
            get
            {
                if (this.serviceManager == null)
                {
                    this.serviceManager = this.ServiceManagerBuilder.Create();
                }

                return this.serviceManager;
            }
        }
        private ApiResource MapApiResource(ApiResources storedApiResource)
        {
            ApiResource retVal = new ApiResource();
            retVal.Name = storedApiResource.Name;

            retVal.ApiSecrets = new List<Secret>();

            foreach(ApiSecrets secret in storedApiResource.ApiSecrets)
            {
                retVal.ApiSecrets.Add(new Secret(secret.Value));
            }

            retVal.UserClaims = new List<string>();

            foreach (ApiClaims claim in storedApiResource.ApiClaims)
            {
                retVal.UserClaims.Add(claim.Type);
            }

            retVal.Scopes = new List<Scope>();

            foreach (ApiScopes scope in storedApiResource.ApiScopes)
            {
                retVal.Scopes.Add(new Scope(scope.Name, scope.Description));
            }

            return retVal;
        }
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource retVal = null;
            ApiResources targetResource = this.ServiceManager.ApiResourceService.GetByName(name);

            if(targetResource!=null)
            {
                retVal = this.MapApiResource(targetResource);
            }

            return Task.FromResult(retVal);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            IList<ApiResource> retVal = new List<ApiResource>();

            IList<ApiResources> foundItems = this.ServiceManager.ApiResourceService.GetByScopes(scopeNames.ToList());

            if(foundItems != null)
            {
                foreach(ApiResources apiResource in foundItems)
                {
                    retVal.Add(this.MapApiResource(apiResource));
                }
            }

            return Task.FromResult(retVal as IEnumerable<ApiResource>);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(ResourceStore.GetIdentityResources());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            Resources retVal = new Resources();

            IList<ApiResources> foundItems = this.ServiceManager.ApiResourceService.GetAll();

            retVal.ApiResources = new List<ApiResource>();

            foreach (ApiResources apiResource in foundItems)
            {
                retVal.ApiResources.Add(this.MapApiResource(apiResource));
            }

            retVal.IdentityResources = ResourceStore.GetIdentityResources().ToArray();
            return Task.FromResult(retVal);
        }
    }
}
