using AlwaysMoveForward.Core.Common.DataLayer.Dapper;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class ProtectedResourceRepository : DapperRepositoryBase<ProtectedResource, DTO.ProtectedResource, long>, IProtectedResourceRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ProtectedResourceRepository(UnitOfWork unitOfWork)
            : base(unitOfWork, "ProtectedResource") 
        {

        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<ProtectedResource, DTO.ProtectedResource> GetDataMapper()
        {
            return new DataMapper.ProtectedResourceDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.ProtectedResource GetDTOById(ProtectedResource idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        public override IList<ProtectedResource> GetAll()
        {
            return null;
        }
    }
}
