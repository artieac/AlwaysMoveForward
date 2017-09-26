using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlwaysMoveForward.OAuth2.Common.DomainModel;

namespace AlwaysMoveForward.OAuth2.Common.Security
{
    /// <summary>
    /// A default DefaultSecurityPrincipal that works with a User
    /// </summary>
    public class DefaultSecurityPrincipal : SecurityPrincipalBase<User>
    {
        /// <summary>
        /// The base class constructor override.
        /// </summary>
        /// <param name="user">A AlwaysMoveForwardUser instance</param>
        public DefaultSecurityPrincipal(User user) : base(user) { }

        /// <summary>
        /// Gets the name value returned by the IIdentity interface
        /// </summary>
        public override string Name
        {
            get 
            {
                string retVal = string.Empty;

                if (this.User != null)
                {
                    retVal = this.User.FirstName + " " + this.User.LastName;
                }

                return retVal;
            }
        }
    }
}
