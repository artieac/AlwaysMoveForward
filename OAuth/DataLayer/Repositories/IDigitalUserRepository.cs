using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.Common.DataLayer.NHibernate;
using AlwaysMoveForward.OAuth.Common.DomainModel;
using AlwaysMoveForward.OAuth.DataLayer.DataMapper;

namespace AlwaysMoveForward.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// A repository for retrieving Digital Users
    /// </summary>
    public interface IDigitalUserRepository : INHibernateRepository<AMFUserLogin, int>
    {
        /// <summary>
        /// Get a DigitalUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        AMFUserLogin GetByEmail(string emailAddress);
    }
}
