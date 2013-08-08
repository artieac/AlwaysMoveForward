using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Security.Permissions;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.Core.Utilities;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    public class PublicController : BaseController
    {
        public PublicController()
            : base()
        {

        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            string blogSubFolder = "All";

            if (this.ControllerContext.RouteData.Values.ContainsKey("blogSubFolder"))
            {
                blogSubFolder = this.ControllerContext.RouteData.Values["blogSubFolder"].ToString();
            }
        }

        public Blog GetTargetBlog()
        {
            Blog retVal = null;

            if (this.ControllerContext.RouteData.Values.ContainsKey("blogSubFolder"))
            {
                retVal = this.GetTargetBlog(this.ControllerContext.RouteData.Values["blogSubFolder"].ToString());
            }

            return retVal;
        }

        public Blog GetTargetBlog(string blogSubFolder)
        {
            return Services.Blogs.GetBySubFolder(blogSubFolder);
        }

        public ModelBase InitializeDataModel(string blogSubFolder, ModelBase modelBase)
        {
            modelBase.TargetBlog = this.Services.Blogs.GetBySubFolder(blogSubFolder);

            if (modelBase.TargetBlog != null)
            {
                modelBase.BlogName = modelBase.TargetBlog.Name;
                modelBase.BlogSubFolder = modelBase.TargetBlog.SubFolder;
                ViewData["blogSubFolder"] = modelBase.TargetBlog.SubFolder;
                ViewData["blogName"] = modelBase.TargetBlog.Name;
            }
            else
            {
                modelBase.BlogSubFolder = "All";
                ViewData["blogSubFolder"] = "All";
                ViewData["blogName"] = "";
            }

            modelBase.BlogList = Services.Blogs.GetAll();
            modelBase.BlogDates = Services.BlogEntries.GetArchiveDates(modelBase.TargetBlog);
            modelBase.BlogTags = this.GetBlogTags(modelBase.TargetBlog);
            modelBase.BlogRoll = this.GetBlogRoll(modelBase.TargetBlog);
            modelBase.RegisteredExtensions = Services.BlogExtensions.GetAll();
            modelBase.TargetMonth = DateTime.Now;
            modelBase.CurrentMonthBlogDates = this.GetBlogDatesForMonth2(modelBase.TargetBlog, modelBase.TargetMonth);

            return modelBase;
        }

        public IList GetBlogTags(Blog targetBlog)
        {
            IList retVal = new ArrayList();

            if (targetBlog != null)
            {
                retVal = Services.Tags.GetAllWithCount(targetBlog);
            }

            return retVal;
        }

        public IList<BlogRollLink> GetBlogRoll(Blog targetBlog)
        {
            IList<BlogRollLink> retVal = null;

            if (targetBlog != null)
            {
                retVal = Services.BlogLinks.GetAllByBlog(targetBlog);
            }
            else
            {
                retVal = new List<BlogRollLink>();
            }

            return retVal;
        }

        public IList<DateTime> GetBlogDatesForMonth(Blog targetBlog, DateTime targetMonth)
        {
            IList<DateTime> retVal = new List<DateTime>();

            if (targetBlog != null)
            {
                IList<BlogPost> blogDates = Services.BlogEntries.GetByMonth(targetBlog, targetMonth, true);

                for (int i = 0; i < blogDates.Count; i++)
                {
                    retVal.Add(blogDates[i].DatePosted);
                }
            }

            return retVal;
        }

        public IList<DateTime> GetBlogDatesForMonth2(Blog targetBlog, DateTime targetMonth)
        {
            IList<DateTime> retVal = new List<DateTime>();

            if (targetBlog != null)
            {
                IList<BlogPost> blogDates = Services.BlogEntries.GetByMonth(targetBlog, targetMonth, true);

                for (int i = 0; i < blogDates.Count; i++)
                {
                    retVal.Add(blogDates[i].DatePosted.Date);
                }
            }
            else
            {
                IList<DateTime> blogDates = Services.BlogEntries.GetPublishedDatesByMonth(targetMonth);

                for (int i = 0; i < blogDates.Count; i++)
                {
                    retVal.Add(blogDates[i].Date);
                }
            }

            return retVal;
        }
    }
}
