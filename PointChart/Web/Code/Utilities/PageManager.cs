using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.BusinessLayer.Services;
using AlwaysMoveForward.PointChart.BusinessLayer.Utilities;

namespace AlwaysMoveForward.PointChart.Web.Code.Utilities
{
    public class PageManager
    {
        public static bool IsSiteAdministrator()
        {
            bool retVal = false;

            SecurityPrincipal currentPrincipal = HttpContext.Current.User as SecurityPrincipal;

            if (currentPrincipal != null)
            {
                retVal = currentPrincipal.IsInRole(RoleType.Names.SiteAdministrator);
            }

            return retVal;
        }

        public static bool CanAccessAdminTool()
        {
            bool retVal = false;

            SecurityPrincipal currentPrincipal = HttpContext.Current.User as SecurityPrincipal;

            if (currentPrincipal != null)
            {
                if (currentPrincipal.IsInRole(RoleType.Names.SiteAdministrator) ||
                    currentPrincipal.IsInRole(RoleType.Names.Administrator))
                {
                    retVal = true;
                }
            }

            return retVal;
        }
    }
}