using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using System.Web.Security;

using AnotherBlog;
using AnotherBlog.Core;
using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.MVC.Views.Shared
{
    public partial class Site : System.Web.Mvc.ViewMasterPage
    {
        public string BlogSubFolder
        {
            get
            {
                string retVal = "All";

                if (ViewData.ContainsKey("blogSubFolder"))
                {
                    if (ViewData["blogSubFolder"] != null)
                    {
                        retVal = ViewData["blogSubFolder"].ToString();
                    }
                }

                return retVal;
            }
        }

        public AnotherBlog.Common.Data.Entities.Blog TargetBlog
        {
            get
            {
                AnotherBlog.Common.Data.Entities.Blog retVal = null;

                AnotherBlog.MVC.Models.ModelBase modelData = ViewData.Model as AnotherBlog.MVC.Models.ModelBase;

                if (modelData != null)
                {
                    if (modelData.TargetBlog != null)
                    {
                        retVal = modelData.TargetBlog;
                    }
                }

                return retVal;
            }
        }

        protected void RenderBlogLinkBar()
        {
            string retVal =  "";
            string currentBlog = ViewData["blogSubFolder"].ToString();

            if(currentBlog!="All")
            {
                retVal += "<div class='guestLinkSection'>";
                retVal += "<div class='guestLinkSectionTitle'>" + ViewData["blogName"] + " Blog</div>";
                retVal += "<ul class='guestLinkList'>";
                retVal += "<li class='guestLinkItem'>";
                retVal += "<a href='/" + currentBlog + "/Blog/Index'>Blog Posts</a>";
                retVal += "</li><li class='guestLinkItem'>";
                retVal += "<a href='/" + currentBlog + "/Blog/About'>About</a> ";
                retVal += "</li>";

                if (AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, currentBlog, Role.Administrator) == true || AnotherBlog.MVC.Utilities.Utils.IsUserInRole(this.Context.User, currentBlog, Role.SiteAdministrator))
                {
                    retVal += "</li><li class='guestLinkItem'>";
                    retVal += "<a href='/Admin/Index'>Manage</a> ";
//                    retVal += "<a href='/" + currentBlog + "/Blog/AdministerBlog'>Manage</a> ";
                    retVal += "</li>";
                }

                retVal += "</ul>";
                retVal += "</div>";
            }
        
            Response.Write(retVal);
        }
    }
}
