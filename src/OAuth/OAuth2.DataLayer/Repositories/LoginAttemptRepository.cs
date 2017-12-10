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
    public class LoginAttemptRepository: EntityFrameworkRepositoryBase<LoginAttempt, Models.LoginAttempts, Models.AMFOAuthDbContext, long>, ILoginAttemptRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public LoginAttemptRepository(NewUnitOfWork unitOfWork) : base(unitOfWork) 
        { 
        
        }

        protected override string IdPropertyName
        {
            get { return "Id"; }
        }
        protected override DbSet<LoginAttempts> GetEntityInstance()
        {
            return this.UnitOfWork.DataContext.LoginAttempts;
        }
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<LoginAttempt, Models.LoginAttempts> GetDataMapper()
        {
            return new DataMapper.LoginAttemptDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override Models.LoginAttempts GetDTOById(LoginAttempt idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        protected override Models.LoginAttempts GetDTOById(long id)
        {
            Models.LoginAttempts retVal = this.UnitOfWork.DataContext.LoginAttempts.Where(c => c.Id == id).FirstOrDefault();

            return retVal;
        }
        /// <summary>
        /// Get a AMFUserLogin by the attempted login name
        /// </summary>
        /// <param name="userName">The users email address used to attempt to login</param>
        /// <returns>The found domain object instance</returns>
        public IList<LoginAttempt> GetByUserName(string userName)
        {
            IEnumerable<Models.LoginAttempts> retVal = this.UnitOfWork.DataContext.LoginAttempts.Where(la => la.UserName == userName);
            
            return this.GetDataMapper().Map(retVal);
        }
    }
}
