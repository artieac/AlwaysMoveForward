using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// This class contains all the code to extract Role data from the repository using LINQ
    /// </summary>
    /// <param name="dataContext"></param>
    public class RoleGateway : GatewayBase
    {
        public RoleGateway(DataContextManager dataContext)
            : base(dataContext)
        {

        }
        /// <summary>
        /// Get all roles allowed by the site.
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAll()
        {
            IQueryable<Role> retVal = from foundItem in this.DataContext.Roles select foundItem;

            return retVal.ToList();
        }
        /// <summary>
        /// Get a specific role by Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public Role GetById(int roleId)
        {
            Role retVal = null;

            try
            {
                retVal = (from foundItem in this.DataContext.Roles where foundItem.RoleId == roleId select foundItem).Single();
            }
            catch (Exception e)
            {
                this.Logger.Warn(e.Message, e);
            }

            return retVal;
        }

    }
}
