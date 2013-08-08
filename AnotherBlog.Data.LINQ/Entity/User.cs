using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace TheOffWing.AnotherBlog.Core.Entity
{
    /// <summary>
    /// Extend the LINQ generated User class in case some sort of additional properties or methods
    /// need to be added to it. 
    /// </summary>
    public partial class User : IPrincipal, IIdentity 
    {
        /// <summary>
        /// Keep track on if the user has logged in and been authenticated by the system.  Used with the 
        /// IIdentity implementation.
        /// </summary>
        bool isAuthenticated = false;
        /// <summary>
        /// Implement the IIDentity interface so that it can be used with built in .Net security methods
        /// </summary>
        #region IIdentity

        public bool IsAuthenticated
        {
            get { return this.isAuthenticated; }
            set { this.isAuthenticated = value; }
        }

        public string AuthenticationType
        {
            get { return ""; }
        }

        public string Name
        {
            get { return this.UserName; }
        }

        #endregion
        /// <summary>
        /// Implement the IPrincipal interface so that the current user can be thrown onto the current Threads
        /// principal placeholder and passed around cleanly.
        /// </summary>
        #region IPrincipal

        public IIdentity Identity
        {
            get{ return this;}
        }
        /// <summary>
        /// Is in role is not really used.  Originally I wanted to use the built in .Net security features (so it was used)
        /// however with the multple blogs/user roles implementation that didn't fit this model anymore so its not used.
        /// </summary>
        /// <param name="targetRole"></param>
        /// <returns></returns>
        public bool IsInRole(string targetRole)
        {
            return false;
        }

        #endregion
        /// <summary>
        /// The replacement method for the IPrincipal.IsInRole method.  It determines if the user is in a specific
        /// role for a specific blog
        /// </summary>
        /// <param name="targetRole">What role to check the user against.</param>
        /// <param name="blogSubFolder">What blog to check the user against.</param>
        /// <returns></returns>
        public bool IsInRole(string targetRole, string blogSubFolder)
        {
            bool retVal = false;

            for (int i = 0; i < this.BlogUsers.Count; i++)
            {
                if (this.BlogUsers[i].Blog.SubFolder == blogSubFolder)
                {
                    if (this.BlogUsers[i].Role.Name == targetRole)
                    {
                        retVal = true;
                        break;
                    }
                }
            }

            return retVal;
        }
        /// <summary>
        /// Another version of the IsInRole method.  This one allows the caller to check if the user is in 
        /// any one of the passed in roles.
        /// </summary>
        /// <param name="targetRole"></param>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public bool IsInRole(string[] targetRole, Blog targetBlog)
        {
            bool retVal = false;

            if (targetRole.Contains("SiteAdministrator"))
            {
                if (this.IsSiteAdministrator)
                {
                    retVal = true;
                }
            }

            if (retVal == false)
            {
                if (targetBlog != null)
                {
                    for (int i = 0; i < this.BlogUsers.Count; i++)
                    {
                        if (this.BlogUsers[i].Blog.Name == targetBlog.Name)
                        {
                            if (targetRole.Contains(this.BlogUsers[i].Role.Name))
                            {
                                retVal = true;
                                break;
                            }
                        }
                    }
                }
            }

            return retVal;
        }
    }
}
