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

using AnotherBlog.Common;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    public class HomeController : PublicController
    {
        public ActionResult About(string blogSubFolder)
        {
            SiteModel model = (SiteModel)this.InitializeDataModel(blogSubFolder, new SiteModel());

            model.SiteInfo = Services.SiteInfo.GetSiteInfo();

            if (model.SiteInfo == null)
            {
                model.SiteInfo = Services.SiteInfo.Create();
            }

            return View(model);
        }


        public ActionResult Index(string filterType, string filterValue)
        {
            HomeModel model = (HomeModel)this.InitializeDataModel("", new HomeModel());

            IList<Blog> allBlogs = Services.Blogs.GetAll();

            if (filterType != null)
            {
                if (filterType == "month")
                {
                    DateTime filterDate = DateTime.ParseExact(filterValue, "MM-dd-yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
                    model.BlogEntries = Services.BlogEntries.GetByMonth(filterDate, true);
                    model.ContentTitle = "Blog entries for " + filterDate.ToString("MMMM") + " " + filterDate.ToString("yyyy");
                    model.TargetMonth = filterDate;
                    model.CurrentMonthBlogDates = this.GetBlogDatesForMonth2(model.TargetBlog, model.TargetMonth);
                }
                else if (filterType == "day")
                {
                    DateTime filterDate = DateTime.ParseExact(filterValue, "MM-dd-yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
                    model.BlogEntries = Services.BlogEntries.GetByDate(filterDate, true);
                    model.ContentTitle = "Blog entries for " + filterDate.ToString("D");
                    model.TargetMonth = filterDate;
                    model.CurrentMonthBlogDates = this.GetBlogDatesForMonth2(model.TargetBlog, model.TargetMonth);
                }
            }
            else
            {
                model.BlogEntries = Services.BlogEntries.GetMostRecent(10);
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult AdministerSite()
        {
            SiteModel model = (SiteModel)this.InitializeDataModel("", new SiteModel());
            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult ConfigureExtension(string blogSubFolder, int? extensionId)
        {
            SiteModel model = (SiteModel)this.InitializeDataModel("", new SiteModel());
            model.ContentTitle = "Configure Extension";

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

        public ActionResult DisplayListControl(string blogSubFolder, String targetBlogListName)
        {
            ListControlModel model = new ListControlModel();
            Blog targetBlog = Services.Blogs.GetBySubFolder(blogSubFolder);

            if (targetBlog != null)
            {
                BlogList blogList = Services.BlogLists.GetByName(targetBlog, targetBlogListName);

                if (blogList != null)
                {
                    model.Title = blogList.Name;
                    model.ShowOrdered = blogList.ShowOrdered;
                    model.ListItems = Services.BlogLists.GetListItems(blogList);
                }
            }

            return View("ListControl", model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult ManageExtensions(string[] blogExtensions)
        {
            SiteModel model = (SiteModel)this.InitializeDataModel("", new SiteModel());

            List<string> extensionList = new List<String>();

            if (blogExtensions != null)
            {
                extensionList = blogExtensions.ToList<String>();
            }

            Services.BlogExtensions.UpdateExtensionInformation(extensionList);

            model.RegisteredExtensions = Services.BlogExtensions.GetAll();
            model.FoundExtensions = BlogExtensionService.FindExtensions(HttpRuntime.BinDirectory);

            return View(model);
        }
    }
}
