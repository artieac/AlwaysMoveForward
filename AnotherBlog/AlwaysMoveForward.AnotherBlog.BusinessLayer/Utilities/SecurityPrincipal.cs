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

using AlwaysMoveForward.Common.DataLayer.Entities;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities
{
    public class SecurityPrincipal : IPrincipal, IIdentity
    {
        static IDictionary<int, Role> systemRoles = null;

        public static IDictionary<int, Role> Roles
        {
            get
            {
                if (systemRoles == null)
                {
                    systemRoles = new Dictionary<int, Role>();

                    ServiceManager serviceManager = ServiceManagerBuilder.BuildServiceManager();
                    IList<Role> roles = serviceManager.RoleService.GetAll();

                    for (int i = 0; i < roles.Count; i++)
                    {
                        systemRoles.Add(roles[i].RoleId, roles[i]);
                    }
                }

                return systemRoles;
            }
        }

        bool isAuthenticated = false;
        User currentUser;
        ServiceManager serviceManager = null;
        static Dictionary<int, Role> userRoles;

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
                    serviceManager = ServiceManagerBuilder.BuildServiceManager();
                }

                return this.serviceManager;
            }
        }

        private Dictionary<int, Role> UserRoles
        {
            get
            {
                if (userRoles == null)
                {
                    userRoles = new Dictionary<int, Role>();

                    IList<Role> allRoles = this.ServiceManager.RoleService.GetAll();

                    for (int i = 0; i < allRoles.Count; i++)
                    {
                        userRoles.Add(allRoles[i].RoleId, allRoles[i]);
                    }
                }

                return userRoles;
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
                if (targetRole == RoleType.SiteAdministrator)
                {
                    retVal = this.currentUser.IsSiteAdministrator;
                }

                if (retVal == false)
                {
                    IList<BlogUser> userBlogs = this.ServiceManager.BlogUserService.GetUserBlogs(this.currentUser.UserId);

                    if (userBlogs != null)
                    {
                        for (int i = 0; i < userBlogs.Count; i++)
                        {
                            if (Roles[userBlogs[i].Role.RoleId].Name == targetRole)
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
                if (targetRole.Contains(RoleType.SiteAdministrator))
                {
                    if (this.currentUser.IsSiteAdministrator)
                    {
                        retVal = true;
                    }
                }

                if (retVal == false)
                {
                    IList<BlogUser> userBlogs = this.ServiceManager.BlogUserService.GetUserBlogs(this.currentUser.UserId);

                    if (userBlogs != null)
                    {
                        for (int i = 0; i < userBlogs.Count; i++)
                        {
                            if (userBlogs[i].Blog.SubFolder == blogSubFolder)
                            {
                                if (Roles[userBlogs[i].Role.RoleId].Name == targetRole)
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
                    if (targetRole.Contains(RoleType.SiteAdministrator))
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
                        IList<BlogUser> userBlogs = this.ServiceManager.BlogUserService.GetUserBlogs(this.currentUser.UserId);

                        if (userBlogs != null)
                        {
                            for (int i = 0; i < userBlogs.Count; i++)
                            {
                                if (userBlogs[i].Blog.BlogId == targetBlog.BlogId)
                                {
                                    if (targetRole.Contains(Roles[userBlogs[i].Role.RoleId].Name))
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
