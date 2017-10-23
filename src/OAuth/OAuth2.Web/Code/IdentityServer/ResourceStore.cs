﻿using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityModel;
using IdentityServer4;

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
                new IdentityResources.Email(),
                new IdentityResources.Profile()
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

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            ApiResource retVal = null;

            for(int i = 0; i < ResourceStore.resources.Count; i++)
            {
                if(ResourceStore.resources[i].Name == name)
                {
                    retVal = ResourceStore.resources[i];
                }
            }

            return Task.FromResult(retVal);
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
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

            return Task.FromResult(retVal as IEnumerable<ApiResource>);
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(ResourceStore.GetIdentityResources());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            Resources retVal = new Resources();
            retVal.ApiResources = ResourceStore.resources;
            retVal.IdentityResources = ResourceStore.GetIdentityResources().ToArray();
            return Task.FromResult(retVal);
        }
    }
}