/**
 * Copyright (c) 2009 Arthur Correa.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Common Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/cpl1.0.php
 *
 * Contributors:
 *    Arthur Correa – initial contribution
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;

namespace AnotherBlog.Core.Utilities
{
    public class SecurityPrincipal : IPrincipal, IIdentity
    {
        bool isAuthenticated = false;
        User currentUser;
        ServiceManager serviceManager = null;

        public SecurityPrincipal(User _currentUser) : this(_currentUser, false){}

        public SecurityPrincipal(User _currentUser, bool _isAuthenticated)
        {
            isAuthenticated = _isAuthenticated;
            currentUser = _currentUser;
        }

        public User CurrentUser
        {
            get { return this.currentUser; }
        }

        private ServiceManager ServiceManager
        {
            get
            {
                if (this.serviceManager == null)
                {
                    serviceManager = new ServiceManager();
                    serviceManager.RepositoryManager = ServiceManager.CreateRepositoryManager();
                }

                return this.serviceManager;
            }
        }
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
            get 
            {
                string retVal = "";
                
                if(this.currentUser!=null)
                    retVal = this.currentUser.UserName;

                return retVal;
            }
        }

        #endregion
        /// <summary>
        /// Implement the IPrincipal interface so that the current user can be thrown onto the current Threads
        /// principal placeholder and passed around cleanly.
        /// </summary>
        #region IPrincipal

        public IIdentity Identity
        {
            get { return this; }
        }
        /// <summary>
        /// Is in role is not really used.  Originally I wanted to use the built in .Net security features (so it was used)
        /// however with the multple blogs/user roles implementation that didn't fit this model anymore so its not used.
        /// </summary>
        /// <param name="targetRole"></param>
        /// <returns></returns>
        public bool IsInRole(string targetRole)
        {
            bool retVal = false;

            if (this.currentUser != null)
            {
                if (targetRole == Role.SiteAdministrator)
                {
                    retVal = this.currentUser.IsSiteAdministrator;
                }
            }

            return retVal;
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

            if (this.currentUser != null)
            {
                if (targetRole.Contains(Role.SiteAdministrator))
                {
                    if (this.currentUser.IsSiteAdministrator)
                    {
                        retVal = true;
                    }
                }

                if (retVal == false)
                {
                    IList<BlogUser> userBlogs = this.ServiceManager.BlogUsers.GetUserBlogs(this.currentUser.UserId);

                    if (userBlogs != null)
                    {
                        for (int i = 0; i < userBlogs.Count; i++)
                        {
                            if (userBlogs[i].Blog.SubFolder == blogSubFolder)
                            {
                                if (userBlogs[i].UserRole.Name == targetRole)
                                {
                                    retVal = true;
                                    break;
                                }
                            }
                        }
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

            if (this.currentUser != null)
            {
                if (targetRole != null)
                {
                    if (targetRole.Contains(Role.SiteAdministrator))
                    {
                        if (this.currentUser.IsSiteAdministrator)
                        {
                            retVal = true;
                        }
                    }
                }

                if (retVal == false)
                {
                    if (targetBlog != null)
                    {
                        IList<BlogUser> userBlogs = this.ServiceManager.BlogUsers.GetUserBlogs(this.currentUser.UserId);

                        if (userBlogs != null)
                        {
                            for (int i = 0; i < userBlogs.Count; i++)
                            {
                                if (userBlogs[i].Blog.BlogId == targetBlog.BlogId)
                                {
                                    if (targetRole.Contains(userBlogs[i].UserRole.Name))
                                    {
                                        retVal = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return retVal;
        }
    }
}
