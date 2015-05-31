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

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;

namespace AlwaysMoveForward.PointChart.BusinessLayer.Utilities
{
    public class SecurityPrincipal : IPrincipal, IIdentity
    {       
        public SecurityPrincipal(PointChartUser currentUser) : this(currentUser, false) { }

        public SecurityPrincipal(PointChartUser currentUser, bool isAuthenticated)
        {
            this.IsAuthenticated = isAuthenticated;
            this.CurrentUser = currentUser;
        }

        public PointChartUser CurrentUser { get; private set; }

        /// <summary>
        /// Implement the IIDentity interface so that it can be used with built in .Net security methods
        /// </summary>
        #region IIdentity

        public bool IsAuthenticated { get; set; }

        public string AuthenticationType
        {
            get { return string.Empty; }
        }

        public string Name
        {
            get 
            {
                string retVal = string.Empty;

                if (this.CurrentUser != null)
                {
                    retVal = this.CurrentUser.GetDisplayName();
                }

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

            if (this.CurrentUser != null)
            {                           
                if (targetRole.Contains(RoleType.Names.SiteAdministrator))
                {
                    if (this.CurrentUser.IsSiteAdministrator)
                    {
                        retVal = true;
                    }
                }

                //if (retVal == false)
                //{
                //    RoleType.Id targetRoleEnum = (RoleType.Id)Enum.Parse(typeof(RoleType.Id), targetRole);

                //    if(this.CurrentUser.Roles.ContainsKey((int)targetRoleEnum))
                //    {
                //        retVal = true;
                //    }
                //}
            }

            return retVal;
        }

        #endregion
        /// <summary>
        /// Another version of the IsInRole method.  This one allows the caller to check if the user is in 
        /// any one of the passed in roles.
        /// </summary>
        /// <param name="targetRole"></param>
        /// <param name="targetBlog"></param>
        /// <returns></returns>
        public bool IsInRole(string[] targetRole)
        {
            bool retVal = false;

            if (this.CurrentUser != null)
            {
                if (targetRole != null)
                {
                    for(int i = 0; i < targetRole.Count(); i++)
                    {
                        if(retVal == false)
                        {
                            retVal = this.IsInRole(targetRole[i]);
                        }
                    }
                }
            }

            return retVal;
        }
    }
}
