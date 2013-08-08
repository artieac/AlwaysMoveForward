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

using AnotherBlog.Common;
using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    public class BlogController : PublicController
    {
        public ActionResult About(string blogSubFolder)
        {
            BlogModel model = (BlogModel)this.InitializeDataModel(blogSubFolder, new BlogModel());
            model.ContentTitle = "About " + model.TargetBlog.Name;

            if (model.TargetBlog != null)
            {
                model.BlogWriters = Services.Users.GetBlogWriters(model.TargetBlog);
            }
            else
            {
                model.BlogWriters = new List<User>();
            }

            return View(model);
        }

        public ActionResult Index(string blogSubFolder, string filterType, string filterValue, int? page)
        {
            BlogModel model = (BlogModel)this.InitializeDataModel(blogSubFolder, new BlogModel());

            if (model.TargetBlog != null)
            {
                int currentPageIndex = 0;

                if (page.HasValue == true)
                {
                    currentPageIndex = page.Value - 1;
                }

                if (model.TargetBlog != null)
                {
                    if (filterType == "tag")
                    {
                        model.BlogEntries = Pagination.ToPagedList(Services.BlogEntries.GetByTag(model.TargetBlog, filterValue, true), currentPageIndex, Constants.PageSize);
                        model.ContentTitle = "Blog entries for " + filterValue;
                    }
                    else if (filterType == "month")
                    {
                        DateTime filterDate = DateTime.ParseExact(filterValue, "MM-dd-yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
                        model.TargetMonth = filterDate;

                        model.BlogEntries = Pagination.ToPagedList(Services.BlogEntries.GetByMonth(model.TargetBlog, filterDate, true), currentPageIndex, Constants.PageSize);
                        model.ContentTitle = "Blog entries for " + filterDate.ToString("MMMM") + " " + filterDate.ToString("yyyy");
                    }
                    else if (filterType == "day")
                    {
                        DateTime filterDate = DateTime.ParseExact(filterValue, "MM-dd-yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
                        model.TargetMonth = filterDate;

                        model.BlogEntries = Pagination.ToPagedList(Services.BlogEntries.GetByDate(model.TargetBlog, filterDate, true), currentPageIndex, Constants.PageSize);
                        model.ContentTitle = "Blog entries for " + filterDate.ToString("D");
                    }
                    else
                    {
                        model.BlogEntries = Pagination.ToPagedList(Services.BlogEntries.GetAllByBlog(model.TargetBlog, true), currentPageIndex, Constants.PageSize);
                    }

                    model.CurrentMonthBlogDates = this.GetBlogDatesForMonth2(model.TargetBlog, model.TargetMonth);
                }
            }
            else
            {
                model.BlogEntries = new PagedList<BlogPost>();
                model.ContentTitle = "";
            }

            return View(model);
        }

        public ActionResult ViewBlogRoll(string blogSubFolder)
        {
            IList<BlogRollLink> retVal = null;

            Blog targetBlog = this.GetTargetBlog(blogSubFolder);

            if (targetBlog != null)
            {
                retVal = Services.BlogLinks.GetAllByBlog(targetBlog);
            }
            else
            {
                retVal = new List<BlogRollLink>();
            }

            ViewData["blogRollLinks"] = retVal;

            return View("BlogRoll");
        }

        public ActionResult ViewArchive(string blogSubFolder)
        {
            Dictionary<DateTime, int> retVal = new Dictionary<DateTime, int>();

            Blog targetBlog = this.GetTargetBlog(blogSubFolder);

            if (targetBlog != null)
            {
                IList<BlogPost> blogDates = Services.BlogEntries.GetAllByBlog(targetBlog, true);

                for (int i = 0; i < blogDates.Count; i++)
                {
                    DateTime linkDate = new DateTime(blogDates[i].DatePosted.Year, blogDates[i].DatePosted.Month, 1);

                    if (retVal.ContainsKey(linkDate) == true)
                    {
                        retVal[linkDate] = retVal[linkDate] + 1;
                    }
                    else
                    {
                        retVal[linkDate] = 1;
                    }
                }
            }

            ViewData["blogDates"] = retVal;

            return View("BlogArchive");
        }

        public ActionResult ViewTags(string blogSubFolder)
        {
            IList<string> retVal = new List<string>();

            Blog targetBlog = this.GetTargetBlog(blogSubFolder);

            if (targetBlog != null)
            {
                IList<Tag> blogTags = Services.Tags.GetAll(targetBlog);

                for (int i = 0; i < blogTags.Count; i++)
                {
                    retVal.Add(blogTags[i].Name);
                }
            }

            ViewData["blogTags"] = retVal;

            return View("BlogTags");
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult AdministerBlog(string blogSubFolder)
        {
            BlogModel model = (BlogModel)this.InitializeDataModel(blogSubFolder, new BlogModel());

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ConfigureExtension(string blogSubFolder, int? extensionId)
        {
            BlogModel model = (BlogModel)this.InitializeDataModel(blogSubFolder, new BlogModel());

            if (extensionId.HasValue)
            {
                BlogExtensionDefinition blogExtension = BlogExtensionService.GetExtensionInstance(extensionId.Value);

                if (blogExtension != null)
                {
                    blogExtension.AdminDisplay.HandleSubmission(model.TargetBlog.BlogId, this.ControllerContext.RequestContext.HttpContext.Request.Params);
                }
            }

            return View(model);
        }

        public ActionResult ViewComments(string blogSubFolder, string entryId, string authorName, string authorEmail, string commentText, string commentLink, string savingComment)
        {
            EntryCommentModel model = (EntryCommentModel)this.InitializeDataModel(blogSubFolder, new EntryCommentModel());

            int blogEntryId = 0;

            if (entryId != "")
            {
                blogEntryId = int.Parse(entryId);
            }

            BlogPost targetEntry = Services.BlogEntries.GetById(model.TargetBlog, int.Parse(entryId));

            if (model.TargetBlog != null)
            {
                if (savingComment != null)
                {
                    if (authorName == "")
                    {
                        ViewData.ModelState.AddModelError("authorName", "Please enter an author name.");
                    }

                    if (authorEmail == "")
                    {
                        ViewData.ModelState.AddModelError("authorEmail", "Author email.");
                    }

                    if (ViewData.ModelState.IsValid)
                    {
                        Comment savedComment = Services.EntryComments.Save(model.TargetBlog, targetEntry, authorName, authorEmail, commentText, commentLink, this.CurrentPrincipal.CurrentUser);
                    }
                }
            }

            model.CommentList = Services.EntryComments.GetByEntry(model.TargetBlog, targetEntry);

            return View("ViewComments", model);
        }

        public ActionResult SaveComment(string blogSubFolder, string entryId, string authorName, string authorEmail, string commentText, string commentLink)
        {
            if (authorName == "" || authorEmail == "")
            {
                // validation failed.
            }

            Blog targetBlog = this.GetTargetBlog(blogSubFolder);

            BlogPost targetEntry = Services.BlogEntries.GetById(targetBlog, int.Parse(entryId));

            if (targetEntry != null)
            {
                Comment savedComment = Services.EntryComments.Save(targetBlog, targetEntry, authorName, authorEmail, commentText, commentLink, ((User)this.HttpContext.User));
                ViewData["EntryComments"] = Services.EntryComments.GetByEntry(targetBlog, targetEntry);
            }

            return View("ViewComments");
        }

        public ActionResult Post(string blogSubFolder, string year, string month, string day, string title)
        {
            BlogModel model = (BlogModel)this.InitializeDataModel(blogSubFolder, new BlogModel());
            model.ContentTitle = "View blog entry";

            if (model.TargetBlog != null)
            {
                DateTime postDate = DateTime.Parse(month + "/" + day + "/" + year);
                model.BlogEntry = Services.BlogEntries.GetByDateAndTitle(model.TargetBlog, postDate, HttpUtility.UrlDecode(title.Replace("_", " ")));
                model.EntryTags = model.BlogEntry.Tags;
                model.Comments = model.BlogEntry.Comments;
                model.PreviousEntry = Services.BlogEntries.GetPreviousEntry(model.TargetBlog, model.BlogEntry);
                model.NextEntry = Services.BlogEntries.GetNextEntry(model.TargetBlog, model.BlogEntry);
            }
            else
            {
                model.BlogEntry = new BlogPost();
                model.EntryTags = new List<Tag>();
                model.Comments = new PagedList<Comment>();
            }

            return View(model);
        }
    }
}
