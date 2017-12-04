using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Dapper;
using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// The consumer repository implementation
    /// </summary>
    public class ConsumerRepository : EntityFrameworkRepositoryBase<Consumer, Models.Consumers, Models.AMFOAuthDbContext, string>, IConsumerRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ConsumerRepository(NewUnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }

        protected override string IdPropertyName
        {
            get { return "Id"; }
        }
        protected override DbSet<Consumers> GetEntityInstance()
        {
            return this.UnitOfWork.DataContext.Consumers;
        }
        public Consumer GetById(Consumer target)
        {
            return this.GetDataMapper().Map(this.GetDTOById(target.ConsumerKey));
        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<Consumer, Models.Consumers> GetDataMapper()
        {
            return new DataMapper.ConsumerDataMapper(); 
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.Consumers GetDTOById(Consumer idSource)
        {
            return this.GetDTOById(idSource.ConsumerKey);
        }

        protected override Models.Consumers GetDTOById(string id)
        {
            Models.Consumers retVal = this.UnitOfWork.DataContext.Consumers.Where(c => c.ConsumerKey == id).FirstOrDefault();

            return retVal;
        }

        public IList<Consumer> GetAll(int pageIndex, int pageSize)
        {
            return this.GetAll();
        }

        /// <summary>
        /// Get a consumer instance by the consumer key
        /// </summary>
        /// <param name="consumerKey">The consumer key to search for</param>
        /// <returns>An instance of a consumer</returns>
        public Consumer GetByConsumerKey(string consumerKey)
        {
            return this.GetDataMapper().Map(this.GetDTOById(consumerKey));
        }

        /// <summary>
        /// Get a consumer instance by the contact email
        /// </summary>
        /// <param name="consumerKey">The contact email to search for</param>
        /// <returns>An instance of a consumer</returns>
        public IList<Consumer> GetByContactEmail(string contactEmail)
        {
            IList<Models.Consumers> retVal = this.UnitOfWork.DataContext.Consumers.Where(c => c.ContactEmail == contactEmail).ToList();
            
            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Get a consumer instance by a request token
        /// </summary>
        /// <param name="consumerKey">The request token to search for</param>
        /// <returns>An instance of a consumer</returns>
        public Consumer GetByRequestToken(string requestToken)
        {
            Consumer retVal = null;


            return retVal;
        } 
    }
}
