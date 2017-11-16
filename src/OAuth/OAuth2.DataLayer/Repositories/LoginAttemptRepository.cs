using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.DataLayer.DataMapper;
using Dapper;
using AlwaysMoveForward.Core.Common.DataLayer.Dapper;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public class LoginAttemptRepository: DapperRepositoryBase<LoginAttempt, DTO.LoginAttempt, long>, ILoginAttemptRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public LoginAttemptRepository(UnitOfWork unitOfWork) : base(unitOfWork, "LoginAttempts") 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.Core.Common.DataLayer.DataMapBase<LoginAttempt, DTO.LoginAttempt> GetDataMapper()
        {
            return new DataMapper.LoginAttemptDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.LoginAttempt GetDTOById(LoginAttempt idSource)
        {
            return this.GetDTOById(idSource.Id);
        }


        /// <summary>
        /// Get a AMFUserLogin by the attempted login name
        /// </summary>
        /// <param name="userName">The users email address used to attempt to login</param>
        /// <returns>The found domain object instance</returns>
        public IList<LoginAttempt> GetByUserName(string userName)
        {
            var query = "Select * FROM " + this.TableName + " WHERE UserName = @userName";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("userName", userName);

            IEnumerable<DTO.LoginAttempt> retVal = this.UnitOfWork.CurrentSession.Query<DTO.LoginAttempt>(query, dynamicParameters).ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
