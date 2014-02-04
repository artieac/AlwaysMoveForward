using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Service;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;

namespace AlwaysMoveForward.PointChart.Web.Code.Utilities
{
    public class PageManager
    {
        private static IDictionary<int, Role> systemRoles = null;

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
                retVal = currentPrincipal.IsInRole(RoleType.SiteAdministrator.ToString());
            }

            return retVal;
        }

        public static Boolean CanAccessAdminTool()
        {
            Boolean retVal = false;

            SecurityPrincipal currentPrincipal = HttpContext.Current.User as SecurityPrincipal;

            if (currentPrincipal != null)
            {
                if(currentPrincipal.IsInRole(RoleType.SiteAdministrator.ToString()) ||
                   currentPrincipal.IsInRole(RoleType.Administrator.ToString()))
                {
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}