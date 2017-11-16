using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using Dapper;
using AlwaysMoveForward.Core.Common.DataLayer.Dapper;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// The consumer repository implementation
    /// </summary>
    public class ConsumerRepository : DapperRepositoryBase<Consumer, DTO.Consumer, string>, IConsumerRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ConsumerRepository(UnitOfWork unitOfWork) : base(unitOfWork, "Consumers") 
        { 
        
        }
       
        public Consumer GetById(Consumer target)
        {
            return this.GetDataMapper().Map(this.GetDTOById(target.ConsumerKey));
        }

        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<Consumer, DTO.Consumer> GetDataMapper()
        {
            return new DataMapper.ConsumerDataMapper(); 
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Consumer GetDTOById(Consumer idSource)
        {
            return this.GetDTOById(idSource.ConsumerKey);
        }

        protected override DTO.Consumer GetDTOById(string id)
        {
            DTO.Consumer retVal = this.UnitOfWork.CurrentSession.QueryFirstOrDefault<DTO.Consumer>("Select * FROM " + this.TableName + " WHERE ConsumerKey = @Id",
                        new { id });

            return retVal;
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
            var query = "Select * FROM " + this.TableName + " WHERE ContactEmail = @contactEmail";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("contactEmail", contactEmail);

            IList<DTO.Consumer> retVal = this.UnitOfWork.CurrentSession.Query<DTO.Consumer>(query, dynamicParameters).ToList();

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
