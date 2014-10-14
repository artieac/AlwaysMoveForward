using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.DataMapper;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// A repository that retrieves a DigitalUserLogin
    /// </summary>
    public class DigitalUserRepository : RepositoryBase<AMFUserLogin, DTO.DigitalUser, int>, IDigitalUserRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public DigitalUserRepository(UnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Common.DataLayer.DataMapBase<AMFUserLogin, DTO.DigitalUser> GetDataMapper()
        {
            return new DataMapper.DigitalUserDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.DigitalUser GetDTOById(AMFUserLogin idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.DigitalUser GetDTOById(int id)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.DigitalUser>();
            criteria.Add(Expression.Eq(DTO.DigitalUser.IdFieldName, id));
            return criteria.UniqueResult<DTO.DigitalUser>();
        }

        /// <summary>
        /// Get a DigitalUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        public AMFUserLogin GetByEmail(string emailAddress)
        {
            ICriteria criteria = ((UnitOfWork)this.UnitOfWork).CurrentSession.CreateCriteria<DTO.DigitalUser>();
            criteria.Add(Expression.Eq(DTO.DigitalUser.EmailFieldName, emailAddress));
            return this.GetDataMapper().Map(criteria.UniqueResult<DTO.DigitalUser>());
        }
    }
}
