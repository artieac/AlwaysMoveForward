using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;
using AlwaysMoveForward.Core.Common.DataLayer;

namespace AlwaysMoveForward.OAuth2.DataLayer.Repositories
{
    public interface ILoginAttemptRepository : IRepository<LoginAttempt, long>
    {
        /// <summary>
        /// Get a AMFUserLogin by the attempted login name
        /// </summary>
        /// <param name="userName">The users email address used to attempt to login</param>
        /// <returns>The found domain object instance</returns>
        IList<LoginAttempt> GetByUserName(string userName);
    }
}
