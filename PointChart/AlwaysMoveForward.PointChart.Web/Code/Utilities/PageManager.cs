using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.BusinessLayer.Service;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;

namespace AlwaysMoveForward.PointChart.Web.Code.Utilities
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

                    ServiceManager serviceManager = ServiceManager.BuildServiceManager() as ServiceManager;
                    IList<Role> roles = serviceManager.Roles.GetAll();

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
    }
}