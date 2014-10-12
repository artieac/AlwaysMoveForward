using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VP.Digital.Common.DataLayer.NHibernate;
using VP.Digital.Security.OAuth.Common.DomainModel;
using VP.Digital.Security.OAuth.DataLayer.DataMapper;

namespace VP.Digital.Security.OAuth.DataLayer.Repositories
{
    /// <summary>
    /// A repository for retrieving Digital Users
    /// </summary>
    public interface IDigitalUserRepository : INHibernateRepository<DigitalUserLogin, int>
    {
        /// <summary>
        /// Get a DigitalUserLogin by the email address.
        /// </summary>
        /// <param name="emailAddress">The users email address</param>
        /// <returns>The found domain object instance</returns>
        DigitalUserLogin GetByEmail(string emailAddress);
    }
}
