using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class ApiResourceRepository : EntityFrameworkRepositoryBase<ApiResources, Models.ApiResources, Models.AMFOAuthDbContext, long>, IApiResourceRepository
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
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<ApiResources, Models.ApiResources> GetDataMapper()
        {
            return new DataMapper.ApiResourceDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.ApiResources GetDTOById(ApiResources idSource)
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
    }
}
