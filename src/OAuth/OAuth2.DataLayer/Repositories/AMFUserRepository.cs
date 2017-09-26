using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.OAuth2.DataLayer.DataMapper;
using Dapper;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    /// <summary>
    /// A repository that retrieves a AMFUserLogin
    /// </summary>
    public class AMFUserRepository : DapperRepositoryBase<AMFUserLogin, DTO.AMFUser, long>, IAMFUserRepository
    {
        /// <summary>
        /// The constructor, it takes a unit of work
        /// </summary>
        /// <param name="unitOfWork">A unit of Work instance</param>
        public AMFUserRepository(UnitOfWork unitOfWork)
            : base(unitOfWork, "AMFUsers") 
        { 
        
        }
       
        /// <summary>
        /// A data mapper instance to assist the base class
        /// </summary>
        /// <returns>The data mapper</returns>
        protected override AlwaysMoveForward.OAuth2.Common.DataLayer.DataMapBase<AMFUserLogin, DTO.AMFUser> GetDataMapper()
        {
            return new DataMapper.AMFUserLoginDataMapper(); 
        }
        
        /// <summary>
        /// Get an instance of the dto by the domains id value
        /// </summary>
        /// <param name="idSource">The domain object to pull the id from</param>
        /// <returns>An instance of the DTO</returns>
        protected override DTO.AMFUser GetDTOById(AMFUserLogin idSource)
        {
            return this.GetDTOById(idSource.Id);
        }

        /// <summary>
        /// Get a AMFUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        public AMFUserLogin GetByEmail(string emailAddress)
        {
            var query = "Select * FROM " + this.TableName + " WHERE Email = @propertyValue";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@propertyValue", emailAddress);

            DTO.AMFUser retVal = this.UnitOfWork.CurrentSession.QueryFirstOrDefault<DTO.AMFUser>(query, dynamicParameters);

            return this.GetDataMapper().Map(retVal);
        }

        /// <summary>
        /// Search for a user by its email
        /// </summary>
        /// <param name="email">Search the email field for similar strings</param>
        /// <returns>The user if one is found</returns>
        public IList<AMFUserLogin> SearchByEmail(string emailAddress)
        {
            var query = "Select * FROM " + this.TableName + " WHERE Email = @propertyValue";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("propertyValue", emailAddress);

            IEnumerable<DTO.AMFUser> retVal = this.UnitOfWork.CurrentSession.Query<DTO.AMFUser>(query, dynamicParameters).ToList();

            return this.GetDataMapper().Map(retVal);
        }
    }
}
