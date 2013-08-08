using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnotherBlog.MVC.Views.Shared
{
    public partial class UserLogin : System.Web.Mvc.ViewUserControl<AnotherBlog.MVC.Models.ModelBase>
    {
        public bool UserIsAuthenticated
        {
            get
            {
                bool retVal = false;

                AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = this.Context.User as AnotherBlog.Core.Utilities.SecurityPrincipal;

                if (currentPrincipal != null)
                {
                    retVal = currentPrincipal.Identity.IsAuthenticated;
                }

                return retVal;
            }
        }

        public string UserName
        {
            get
            {
                string retVal = "";

                AnotherBlog.Core.Utilities.SecurityPrincipal currentPrincipal = this.Context.User as AnotherBlog.Core.Utilities.SecurityPrincipal;

                if (currentPrincipal != null)
                {
                    retVal = currentPrincipal.Identity.Name;
                }

                return retVal;
            }
        }
    }
}
