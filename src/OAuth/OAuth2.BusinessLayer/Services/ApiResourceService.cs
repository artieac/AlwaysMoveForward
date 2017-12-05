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
    }
}
