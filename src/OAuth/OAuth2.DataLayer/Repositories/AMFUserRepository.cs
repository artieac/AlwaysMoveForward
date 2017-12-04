using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.DataLayer.DataMapper;
using Dapper;
using AlwaysMoveForward.Core.DataLayer.EntityFramework;
using AlwaysMoveForward.OAuth2.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// A repository that retrieves a AMFUserLogin
    /// </summary>
    public class AMFUserRepository : EntityFrameworkRepositoryBase<AMFUserLogin, Models.Amfusers, Models.AMFOAuthDbContext, long>, IAMFUserRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public AMFUserRepository(NewUnitOfWork unitOfWork)
            : base(unitOfWork) 
        { 
        
        }

        protected override DbSet<Amfusers> GetEntityInstance()
        {
            return this.UnitOfWork.DataContext.Amfusers;
        }

        protected override string IdPropertyName
        {
            get { return "Id"; }
        }
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<AMFUserLogin, Models.Amfusers> GetDataMapper()
        {
            return new DataMapper.AMFUserLoginDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.Amfusers GetDTOById(AMFUserLogin idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        protected override Models.Amfusers GetDTOById(long id)
        {
            Models.Amfusers retVal = this.UnitOfWork.DataContext.Amfusers.Where(c => c.Id == id).FirstOrDefault();

            return retVal;
        }

        /// <summary>
        /// Get a AMFUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        public AMFUserLogin GetByEmail(string emailAddress)
        {
            Models.Amfusers retVal = this.UnitOfWork.DataContext.Amfusers.Where(u => u.Email == emailAddress).FirstOrDefault();
            
            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        public IList<AMFUserLogin> SearchByEmail(string emailAddress)
        {            
            IEnumerable<Models.Amfusers> retVal = this.UnitOfWork.DataContext.Amfusers.Where(u => u.Email == emailAddress);

            return this.GetDataMapper().Map(retVal);
        }
    }
}
