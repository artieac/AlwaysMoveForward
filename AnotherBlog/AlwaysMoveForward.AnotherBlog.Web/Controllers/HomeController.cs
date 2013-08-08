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
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Reflection;
using System.IO;

using AlwaysMoveForward.Common.DataLayer.Entities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;
using AlwaysMoveForward.AnotherBlog.Web.Models;
using AlwaysMoveForward.AnotherBlog.Web.Models.Home;
using AlwaysMoveForward.AnotherBlog.Web.Models.BlogModels;
using AlwaysMoveForward.AnotherBlog.Web.Code.Filters;

namespace AlwaysMoveForward.AnotherBlog.Web.Controllers
{
    public class HomeController : PublicController
    {
        public ActionResult About(string blogSubFolder)
        {
            SiteModel model = new SiteModel();
            model.Common = this.InitializeCommonModel();
            model.Common.Calendar = this.InitializeCalendarModel(model.Common.TargetMonth);
            
            model.SiteInfo = Services.SiteInfo.GetSiteInfo();

            if (model.SiteInfo == null)
            {
                model.SiteInfo = new SiteInfo();
            }

            return View(model);
        }


        public ActionResult Index()
        {
            IndexModel model = new IndexModel();
            model.Common = this.InitializeCommonModel();

            IList<Blog> allBlogs = Services.BlogService.GetAll();
            IList<BlogPost> foundBlogEntries = Services.BlogEntryService.GetMostRecent(10);

            model.BlogEntries = this.PopulateBlogPostInfo(foundBlogEntries);
            model.Common.Calendar = this.InitializeCalendarModel(model.Common.TargetMonth);

            return View(model);
        }

        public ActionResult Month(int yearFilter, int monthFilter)
        {
            IndexModel model = new IndexModel();
            model.Common = this.InitializeCommonModel();

            IList<Blog> allBlogs = Services.BlogService.GetAll();
            IList<BlogPost> foundBlogEntries = null;

            DateTime filterDate = new DateTime(yearFilter, monthFilter, 1);
            foundBlogEntries = Services.BlogEntryService.GetByMonth(filterDate, true);
            model.Common.ContentTitle = "Blog entries for " + filterDate.ToString("MMMM") + " " + filterDate.ToString("yyyy");
            model.Common.TargetMonth = filterDate;

            model.BlogEntries = this.PopulateBlogPostInfo(foundBlogEntries);
            model.Common.Calendar = this.InitializeCalendarModel(model.Common.TargetMonth);

            return View("Index", model);
        }

        public ActionResult Day(int yearFilter, int monthFilter, int dayFilter)
        {
            IndexModel model = new IndexModel();
            model.Common = this.InitializeCommonModel();

            IList<Blog> allBlogs = Services.BlogService.GetAll();
            IList<BlogPost> foundBlogEntries = null;

            DateTime filterDate = new DateTime(yearFilter, monthFilter, dayFilter);
            foundBlogEntries = Services.BlogEntryService.GetByDate(filterDate, true);
            model.Common.ContentTitle = "Blog entries for " + filterDate.ToString("D");
            model.Common.TargetMonth = filterDate;

            model.BlogEntries = this.PopulateBlogPostInfo(foundBlogEntries);
            model.Common.Calendar = this.InitializeCalendarModel(model.Common.TargetMonth);

            return View("Index", model);
        }

        [CustomAuthorization(RequiredRoles = RoleType.SiteAdministrator)]
        public ActionResult ConfigureExtension(string blogSubFolder, int? extensionId)
        {
            SiteModel model = new SiteModel();
            model.Common = this.InitializeCommonModel();
            model.Common.Calendar = this.InitializeCalendarModel(model.Common.TargetMonth);

            if (extensionId.HasValue)
            {
                BlogExtensionDefinition blogExtension = BlogExtensionService.GetExtensionInstance(extensionId.Value);

                if (blogExtension != null)
                {
                    blogExtension.AdminDisplay.HandleSubmission(-1, this.ControllerContext.RequestContext.HttpContext.Request.Params);
                }
            }

            return View(model);
        }

    //    public ActionResult DisplayListControl(string blogSubFolder, String targetBlogListName)
    //    {
    //        ListControlModel model = new ListControlModel();
    //        Blog targetBlog = Services.BlogService.GetBySubFolder(blogSubFolder);

    //        if (targetBlog != null)
    //        {
    //            BlogList blogList = Services.BlogListService.GetByName(targetBlog, targetBlogListName);

    //            if (blogList != null)
    //            {
    //                model.Title = blogList.Name;
    //                model.ShowOrdered = blogList.ShowOrdered;
    //                model.ListItems = blogList.Items;
    //            }
    //        }

    //        return View("_ListControl", model);
    //    }
    }
}
