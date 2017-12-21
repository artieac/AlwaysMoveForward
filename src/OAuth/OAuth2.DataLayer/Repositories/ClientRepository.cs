using System;
using System.Collections.Generic;
using System.Text;
using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.Common.DomainModel.APIManagement;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AlwaysMoveForward.OAuth2.Common.DomainModel.ConsumerManagement;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class ClientRepository : EntityFrameworkRepositoryBase<Client, Models.Clients, Models.AMFOAuthDbContext, long>, IClientRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ClientRepository(NewUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        protected override DbSet<Models.Clients> GetEntityInstance()
        {
            return this.UnitOfWork.DataContext.Clients;
        }

        protected override string IdPropertyName
        {
            get { return "Id"; }
        }
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<Client, Models.Clients> GetDataMapper()
        {
            return new DataMapper.ClientDataMapper();
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.Clients GetDTOById(Client idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        protected override Models.Clients GetDTOById(long id)
        {
            Models.Clients retVal = this.UnitOfWork.DataContext.Clients
                .Where(client => client.Id == id)
                .Include(client => client.ClientRedirectUris)
                .Include(client => client.ClientScopes)
                .Include(client => client.ClientSecrets)
                .FirstOrDefault();

            return retVal;
        }
    }
}
