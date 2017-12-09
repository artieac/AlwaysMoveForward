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
                new IdentityResources.OpenId(),
                new IdentityResources.Address(),
                new IdentityResources.Phone(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        static ResourceStore()
        {
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
                    new Scope("api1.read_only", "Read Only access to the API"),
                }
            };
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
        private ApiResource MapApiResource(ProtectedApiResource storedApiResource)
        {
            ApiResource retVal = new ApiResource();
            retVal.Name = storedApiResource.Name;

            retVal.ApiSecrets = new List<Secret>();

            foreach(ProtectedApiSecret secret in storedApiResource.ApiSecrets)
            {
                retVal.ApiSecrets.Add(new Secret(secret.Value));
            }

            retVal.UserClaims = new List<string>();

            foreach (ProtectedApiClaim claim in storedApiResource.ApiClaims)
            {
                retVal.UserClaims.Add(claim.Type);
            }

            retVal.Scopes = new List<Scope>();

            foreach (ProtectedApiScope scope in storedApiResource.ApiScopes)
            {
                retVal.Scopes.Add(new Scope(scope.Name, scope.Description));
            }

            return retVal;
        }
        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource retVal = null;
            ProtectedApiResource targetResource = this.ServiceManager.ApiResourceService.GetByName(name);

            if(targetResource!=null)
            {
                retVal = this.MapApiResource(targetResource);
            }

            return Task.FromResult(retVal);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            if (ResourceStore.resources == null)
            {
                ResourceStore.resources = new List<ApiResource>();

                IList<ProtectedApiResource> foundItems = this.ServiceManager.ApiResourceService.GetByScopes(scopeNames.ToList());

                if (foundItems != null)
                {
                    foreach (ProtectedApiResource apiResource in foundItems)
                    {
                        ResourceStore.resources.Add(this.MapApiResource(apiResource));
                    }
                }
            }

            IDictionary<string, bool> addedResources = new Dictionary<string, bool>();
            IList<ApiResource> retVal = new List<ApiResource>();

            foreach (string scopeName in scopeNames)
            {
                for (int i = 0; i < ResourceStore.resources.Count; i++)
                {
                    foreach (Scope scope in ResourceStore.resources[i].Scopes)
                    {
                        if (scope.Name == scopeName)
                        {
                            if (!addedResources.ContainsKey(ResourceStore.resources[i].Name))
                            {
                                retVal.Add(ResourceStore.resources[i]);
                                addedResources.Add(ResourceStore.resources[i].Name, true);
                            }
                        }
                    }
                }
            }

            //IList<ApiResources> foundItems = this.ServiceManager.ApiResourceService.GetByScopes(scopeNames.ToList());

            //if(foundItems != null)
            //{
            //    foreach(ApiResources apiResource in foundItems)
            //    {
            //        retVal.Add(this.MapApiResource(apiResource));
            //    }
            //}

            return Task.FromResult(retVal as IEnumerable<ApiResource>);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(ResourceStore.GetIdentityResources());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            Resources retVal = new Resources();

            IList<ProtectedApiResource> foundItems = this.ServiceManager.ApiResourceService.GetAll();

            retVal.ApiResources = new List<ApiResource>();

            foreach (ProtectedApiResource apiResource in foundItems)
            {
                retVal.ApiResources.Add(this.MapApiResource(apiResource));
            }

            retVal.IdentityResources = ResourceStore.GetIdentityResources().ToArray();
            return Task.FromResult(retVal);
        }
    }
}
