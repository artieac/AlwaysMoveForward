using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using AlwaysMoveForward.OAuth2.DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;

namespace AlwaysMoveForward.OAuth2.BusinessLayer.Services
{
    public class ApiResourceService : IApiResourcesService
    {
        public ApiResourceService(IApiResourceRepository apiResourceRepository)
        {
            this.ApiResourceRepository = apiResourceRepository;
        }

        public IApiResourceRepository ApiResourceRepository { get; private set; }

        public IList<ApiResources> GetAll()
        {
            return this.ApiResourceRepository.GetAll();
        }

        public ApiResources GetById(long id)
        {
            return this.ApiResourceRepository.GetById(id);
        }

        public ApiResources GetByName(string name)
        {
            return this.ApiResourceRepository.GetByName(name);
        }

        public IList<ApiResources> GetByScopes(IList<string> scopes)
        {
            IList<ApiResources> retVal = new List<ApiResources>();

            if(scopes!=null && scopes.Count > 0)
            {
                retVal = this.ApiResourceRepository.GetByScopes(scopes);
            }

            return retVal;
        }
        public ApiResources Add(string name, string displayName, string description, bool enabled)
        {
            ApiResources newResource = new ApiResources();
            newResource.Name = name;
            newResource.DisplayName = displayName;
            newResource.Description = description;
            newResource.Enabled = enabled;

            return this.ApiResourceRepository.Save(newResource);
        }

        public ApiResources Update(long id, string name, string displayName, string description, bool enabled)
        {
            ApiResources targetResource = this.ApiResourceRepository.GetById(id);

            if(targetResource != null)
            {
                targetResource.Name = name;
                targetResource.DisplayName = displayName;
                targetResource.Description = description;
                targetResource.Enabled = enabled;

                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public ApiResources AddSecret(long id, string secret)
        {
            ApiResources targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.AddSecret(secret);
                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public ApiResources AddClaim(long id, string claim)
        {
            ApiResources targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.AddClaim(claim);
                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }

        public ApiResources AddScope(long id, string name, string description)
        {
            ApiResources targetResource = this.ApiResourceRepository.GetById(id);

            if (targetResource != null)
            {
                targetResource.AddScope(name, description);
                targetResource = this.ApiResourceRepository.Save(targetResource);
            }

            return targetResource;
        }
    }
}
