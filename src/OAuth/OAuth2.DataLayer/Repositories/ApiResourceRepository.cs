using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class ApiResourceRepository : EntityFrameworkRepositoryBase<ProtectedApiResource, Models.ApiResources, Models.AMFOAuthDbContext, long>, IApiResourceRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ApiResourceRepository(NewUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DbSet<Models.ApiResources> GetEntityInstance()
        {
            return this.UnitOfWork.DataContext.ApiResources;
        }

        protected override string IdPropertyName
        {
            get { return "Id"; }
        }
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<ProtectedApiResource, Models.ApiResources> GetDataMapper()
        {
            return new DataMapper.ApiResourceDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.ApiResources GetDTOById(ProtectedApiResource idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        protected override Models.ApiResources GetDTOById(long id)
        {
            Models.ApiResources retVal = this.UnitOfWork.DataContext.ApiResources
                .Where(apiResource => apiResource.Id == id)
                .Include(apiResource => apiResource.ApiClaims)
                .Include(apiResource => apiResource.ApiScopes)
                .Include(apiResource => apiResource.ApiSecrets)
                .FirstOrDefault();

            return retVal;
        }

        public ProtectedApiResource GetByName(string name)
        {
            Models.ApiResources retVal = this.UnitOfWork.DataContext.ApiResources
                .Where(apiResource => apiResource.Name == name)
                .Include(apiResource => apiResource.ApiClaims)
                .Include(apiResource => apiResource.ApiScopes)
                .Include(apiResource => apiResource.ApiSecrets)
                .FirstOrDefault();

            return this.GetDataMapper().Map(retVal);
        }

        public IList<ProtectedApiResource> GetByScopes(IList<string> scopeNames)
        {
            IQueryable<Models.ApiResources> retVal = from apiResource in this.UnitOfWork.DataContext.ApiResources
                                                        .Include(apiResource => apiResource.ApiScopes)
                                                        .Include(apiResource => apiResource.ApiClaims)
                                                        .Include(apiResource => apiResource.ApiScopes)
                                                        .Include(apiResource => apiResource.ApiSecrets)
                                                        where apiResource.ApiScopes.Any(scope => scopeNames.Contains(scope.Name))
                                                        select apiResource;

            return this.GetDataMapper().Map(retVal);
        }

        public IList<ProtectedApiScope> GetAvailableScopes()
        {
            IList<ProtectedApiScope> retVal = new List<ProtectedApiScope>();
            IQueryable<Models.ApiScopes> foundItems = from apiScopes in this.UnitOfWork.DataContext.ApiScopes select apiScopes;

            if (foundItems != null)
            {
                foreach(Models.ApiScopes scope in foundItems)
                {
                    retVal.Add(AutoMapper.Mapper.Map<ProtectedApiScope>(scope));
                }
            }

            return retVal;
        }
    }
}
