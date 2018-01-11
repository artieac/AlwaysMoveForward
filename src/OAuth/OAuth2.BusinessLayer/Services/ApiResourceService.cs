using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public class ApiResourceService : IApiResourceService
    {
        public ApiResourceService(IApiResourceRepository apiResourceRepository)
        {
            this.ApiResourceRepository = apiResourceRepository;
        }

        public IApiResourceRepository ApiResourceRepository { get; private set; }

        public IList<ProtectedApiResource> GetAll()
        {
            return this.ApiResourceRepository.GetAll();
        }

        public ProtectedApiResource GetById(long id)
        {
            return this.ApiResourceRepository.GetById(id);
        }

        public ProtectedApiResource GetByName(string name)
        {
            return this.ApiResourceRepository.GetByName(name);
        }

        public IList<ProtectedApiResource> GetByScopes(IList<string> scopes)
        {
            IList<ProtectedApiResource> retVal = new List<ProtectedApiResource>();

            if (scopes != null && scopes.Count > 0)
            {
                retVal = this.ApiResourceRepository.GetByScopes(scopes);
            }

            return retVal;
        }
        public ProtectedApiResource Add(string name, string displayName, string description, bool enabled)
        {
            ProtectedApiResource newResource = new ProtectedApiResource();
            newResource.Name = name;
            newResource.DisplayName = displayName;
            newResource.Description = description;
            newResource.Enabled = enabled;

            return this.ApiResourceRepository.Save(newResource);
        }

        public ProtectedApiResource Update(long id, string name, string displayName, string description, bool enabled)
        {
            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.Name = name;
                targetResource.DisplayName = displayName;
                targetResource.Description = description;
                targetResource.Enabled = enabled;

                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public ProtectedApiResource AddSecret(long id, string secret, string encryptionType)
        {
            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.AddSecret(secret, encryptionType);
                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public bool DeleteSecret(long resourceId, long secretId)
        {
            bool retVal = false;

            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(resourceId);

            if (targetResource != null)
            {
                if (targetResource.ApiSecrets != null)
                {
                    ProtectedApiSecret targetSecret = targetResource.ApiSecrets.Where(secret => secret.Id == secretId).FirstOrDefault();

                    if (targetSecret != null)
                    {
                        targetResource.ApiSecrets.Remove(targetSecret);
                        this.ApiResourceRepository.Save(targetResource);
                        retVal = true;
                    }
                }
            }

            return retVal;
        }

        public ProtectedApiResource AddClaim(long id, string claim)
        {
            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                if (targetResource.ApiClaims != null)
                {
                    ProtectedApiClaim targetClaim = targetResource.ApiClaims.Where(claimInstance => claimInstance.Type == claim).FirstOrDefault();

                    if (targetClaim == null)
                    {
                        targetResource.AddClaim(claim);
                        targetResource = this.ApiResourceRepository.Save(targetResource);
                    }
                }
            }

            return targetResource;
        }

        public bool DeleteClaim(long resourceId, string claim)
        {
            bool retVal = false;

            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(resourceId);

            if (targetResource != null)
            {
                if (targetResource.ApiClaims != null)
                {
                    ProtectedApiClaim targetClaim = targetResource.ApiClaims.Where(claimInstance => claimInstance.Type == claim).FirstOrDefault();

                    if (targetClaim != null)
                    {
                        targetResource.ApiClaims.Remove(targetClaim);
                        this.ApiResourceRepository.Save(targetResource);
                        retVal = true;
                    }
                }
            }

            return retVal;
        }

        public ProtectedApiResource AddScope(long id, string name, string description)
        {
            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.AddScope(name, description);
                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public bool DeleteScope(long resourceId, long scopeId)
        {
            bool retVal = false;

            ProtectedApiResource targetResource = this.ApiResourceRepository.GetById(resourceId);

            if (targetResource != null)
            {
                if (targetResource.ApiScopes != null)
                {
                    ProtectedApiScope targetScope = targetResource.ApiScopes.Where(scope => scope.Id == scopeId).FirstOrDefault();

                    if(targetScope!=null)
                    {
                        targetResource.ApiScopes.Remove(targetScope);
                        this.ApiResourceRepository.Save(targetResource);
                        retVal = true;
                    }
                }
            }

            return retVal;
        }

        public IList<ProtectedApiScope> GetAvailableScopes()
        {
            return this.ApiResourceRepository.GetAvailableScopes();
        }
    }
}
