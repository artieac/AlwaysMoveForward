using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Utilities;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;

namespace AlwaysMoveForward.AnotherBlog.Web.Code.Utilities
{
    public class PageManager
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

        public static Boolean IsSiteAdministrator()
        {
            Boolean retVal = false;

            SecurityPrincipal currentPrincipal = HttpContext.Current.User as SecurityPrincipal;

            if (currentPrincipal != null)
            {
                retVal = currentPrincipal.IsInRole(RoleType.SiteAdministrator);
            }

            return retVal;
        }

        public static Boolean CanAccessAdminTool()
        {
            Boolean retVal = false;

            SecurityPrincipal currentPrincipal = HttpContext.Current.User as SecurityPrincipal;

            if (currentPrincipal != null)
            {
                if(currentPrincipal.IsInRole(RoleType.SiteAdministrator) ||
                   currentPrincipal.IsInRole(RoleType.Administrator))
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        public static String GetCurrentTheme(AlwaysMoveForward.AnotherBlog.Web.Models.CommonModel commonModel)
        {
            String retVal = "default";

            SiteInfo siteInfo = MvcApplication.SiteInfo;

            if (siteInfo != null)
            {
                if (!String.IsNullOrEmpty(siteInfo.DefaultTheme))
                {
                    retVal = siteInfo.DefaultTheme;
                }
            }

            return retVal;
        }
    }
}