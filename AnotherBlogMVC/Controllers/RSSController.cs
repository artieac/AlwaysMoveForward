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

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.MVC.Models;

namespace AnotherBlog.MVC.Controllers
{
    public class RSSController : PublicController
    {
        public ActionResult Index()
        {
            // Add action logic here
            throw new NotImplementedException();
        }

        public ActionResult Posts(string blogSubFolder)
        {
            RSSModel model = (RSSModel)this.InitializeDataModel(blogSubFolder, new RSSModel());

            model.BlogEntries = new Dictionary<int, IList<BlogPost>>();

            Blog targetBlog = Services.Blogs.GetBySubFolder(blogSubFolder);

            if (targetBlog == null)
            {
                model.BlogList = Services.Blogs.GetAll();

                for (int i = 0; i < model.BlogList.Count; i++)
                {
                    model.BlogEntries[model.BlogList[i].BlogId] = Services.BlogEntries.GetAllByBlog(model.BlogList[i], true, 10);
                }
            }
            else
            {
                model.BlogList = new List<Blog>();
                model.BlogList.Add(targetBlog);
                model.BlogEntries[targetBlog.BlogId] = Services.BlogEntries.GetAllByBlog(targetBlog, true, 10);
            }

            return View(model);
        }

        public ActionResult Atom(string blogSubFolder)
        {
            RSSModel model = (RSSModel)this.InitializeDataModel(blogSubFolder, new RSSModel());

            model.BlogEntries = new Dictionary<int, IList<BlogPost>>();
            model.MostRecentPosts = new Dictionary<int, DateTime>();

            Blog targetBlog = Services.Blogs.GetByName(blogSubFolder);

            if (targetBlog == null)
            {
                model.BlogList = Services.Blogs.GetAll();

                for (int i = 0; i < model.BlogList.Count; i++)
                {
                    IList<BlogPost> blogEntries = Services.BlogEntries.GetAllByBlog(model.BlogList[i], true);
                    model.BlogEntries[model.BlogList[i].BlogId] = blogEntries;

                    DateTime mostRecent = DateTime.MinValue;

                    if(blogEntries!=null)
                    {
                        if(blogEntries.Count > 0)
                        {
                            mostRecent = blogEntries[0].DatePosted;
                        }
                    }

                    model.MostRecentPosts[model.BlogList[i].BlogId] = mostRecent;
                }
            }
            else
            {
                model.BlogList = new List<Blog>();
                model.BlogList.Add(targetBlog);

                IList<BlogPost> blogEntries = Services.BlogEntries.GetAllByBlog(targetBlog, true);
                model.BlogEntries[targetBlog.BlogId] = blogEntries;

                DateTime mostRecent = DateTime.MinValue;

                if (blogEntries != null)
                {
                    if (blogEntries.Count > 0)
                    {
                        mostRecent = blogEntries[0].DatePosted;
                    }
                }

                model.MostRecentPosts[targetBlog.BlogId] = mostRecent;
            }

            return View(model);
        }

        public ActionResult Comments(string blogSubFolder)
        {
            RSSModel model = (RSSModel)this.InitializeDataModel(blogSubFolder, new RSSModel());

            model.Comments = new Dictionary<int, IList<Comment>>();

            Blog targetBlog = Services.Blogs.GetBySubFolder(blogSubFolder);

            if (targetBlog == null)
            {
                model.BlogList = Services.Blogs.GetAll();

                for (int i = 0; i < model.BlogList.Count; i++)
                {
                    model.Comments[model.BlogList[i].BlogId] = Services.EntryComments.GetAllApproved(model.BlogList[i]);
                }
            }
            else
            {
                model.BlogList = new List<Blog>();
                model.BlogList.Add(targetBlog);
                model.Comments[targetBlog.BlogId] = Services.EntryComments.GetAllApproved(targetBlog);
            }

            return View(model);
        }
    }
}
