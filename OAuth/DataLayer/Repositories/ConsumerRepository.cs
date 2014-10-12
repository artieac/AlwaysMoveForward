using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.OAuth.Framework;
using NHibernate;
using NHibernate.Criterion;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.DataLayer.DataMapper;

namespace VP.Digital.Security.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// The consumer repository implementation
    /// </summary>
    public class ConsumerRepository : RepositoryBase<Consumer, DTO.Consumer, string>, IConsumerRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public ConsumerRepository(UnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override Digital.Common.DataLayer.DataMapperBase<Consumer, DTO.Consumer> DataMapper
        {
            get { return new DataMapper.ConsumerDataMapper(); }
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

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="consumerKey">The consumer key to search for</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.Consumer GetDTOById(string consumerKey)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Consumer>();
            criteria.Add(Expression.Eq(DTO.Consumer.ConsumerKeyFieldName, consumerKey));
            return criteria.UniqueResult<DTO.Consumer>();
        }

        /// <summary>
        /// Get a consumer instance by the consumer key
        /// </summary>
        /// <param name="consumerKey">The consumer key to search for</param>
        /// <returns>An instance of a consumer</returns>
        public Consumer GetByConsumerKey(string consumerKey)
        {
            return this.DataMapper.Map(this.GetDTOById(consumerKey));
        }

        /// <summary>
        /// Get a consumer instance by the contact email
        /// </summary>
        /// <param name="consumerKey">The contact email to search for</param>
        /// <returns>An instance of a consumer</returns>
        public IList<Consumer> GetByContactEmail(string contactEmail)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Consumer>();
            criteria.Add(Expression.Eq(DTO.Consumer.EmailFieldName, contactEmail));
            return this.DataMapper.Map(criteria.List<DTO.Consumer>());
        }

        /// <summary>
        /// Get a consumer instance by a request token
        /// </summary>
        /// <param name="consumerKey">The request token to search for</param>
        /// <returns>An instance of a consumer</returns>
        public Consumer GetByRequestToken(string requestToken)
        {
            Consumer retVal = null;

            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.RequestToken>();
            criteria.Add(Expression.Eq(DTO.RequestToken.TokenFieldName, requestToken));
            DTO.RequestToken token = criteria.UniqueResult<DTO.RequestToken>();

            if (token != null)
            {
                ICriteria consumerCriteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.Consumer>();
                consumerCriteria.Add(Expression.Eq(DTO.Consumer.ConsumerKeyFieldName, token.ConsumerKey));
                retVal = this.DataMapper.Map(consumerCriteria.UniqueResult<DTO.Consumer>());
            }

            return retVal;
        } 
    }
}
