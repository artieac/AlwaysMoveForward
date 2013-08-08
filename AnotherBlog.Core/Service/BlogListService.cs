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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

using AnotherBlog.Common.Data.Entities;

namespace AnotherBlog.Core.Service
{
    public class BlogListService : ServiceBase
    {
        internal BlogListService(ServiceManager serviceManager)
            : base(serviceManager)
        {

        }

        public BlogList Create(Blog targetBlog)
        {
            BlogList retVal = this.Repositories.BlogLists.Create();
            retVal.Id = -1;
            retVal.Blog = targetBlog;

            return retVal;
        }

        public BlogListItem CreateBlogListItem(BlogList blogList)
        {
            BlogListItem retVal = this.Repositories.BlogListItems.Create();
            retVal.Id = -1;
            retVal.BlogList = blogList;
            return retVal;
        }

        public IList<BlogListItem> GetAll()
        {
            return Repositories.BlogListItems.GetAll();
        }

        public BlogList GetById(int blogListId)
        {
            return Repositories.BlogLists.GetById(blogListId);
        }

        public IList<BlogList> GetByBlog(Blog targetBlog)
        {
            return Repositories.BlogLists.GetByBlog(targetBlog.BlogId);
        }

        public bool DeleteBlogList(BlogList targetBlogList)
        {
            return Repositories.BlogLists.Delete(targetBlogList);
        }

        public BlogList GetByName(Blog targetBlog, String listName)
        {
            return Repositories.BlogLists.GetByProperty("Name", listName, targetBlog.BlogId);
        }

        public IList<String> GetListNames(Blog targetBlog)
        {
            IList<String> retVal = new List<String>();

            if (targetBlog != null)
            {
                IList<BlogList> lists = this.GetByBlog(targetBlog);

                if (lists != null)
                {
                    for (int i = 0; i < lists.Count(); i++)
                    {
                        retVal.Add(lists[i].Name);
                    }
                }
            }

            return retVal;
        }

        public BlogList Save(Blog targetBlog, int blogListId, String name, Boolean showOrdered)
        {
            BlogList itemToSave = null;

            if (blogListId <= 0)
            {
                itemToSave = this.Create(targetBlog);
            }
            else
            {
                itemToSave = Repositories.BlogLists.GetById(blogListId, targetBlog.BlogId);
            }

            itemToSave.Name = name;
            itemToSave.ShowOrdered = showOrdered;
            itemToSave.Blog = targetBlog;

            itemToSave = Repositories.BlogLists.Save(itemToSave);
            return itemToSave;
        }

        public IList<BlogListItem> GetListItems(BlogList blogList)
        {
            IList<BlogListItem> retVal = new List<BlogListItem>();

            if (blogList != null)
            {
                retVal = this.Repositories.BlogListItems.GetByBlogList(blogList.Id);
            }

            return retVal;
        }

        public BlogListItem SaveBlogListItem(Blog targetBlog, BlogList blogList, int blogListItemId, String itemName, String relatedLink, int displayOrder)
        {
            BlogListItem itemToSave = null;
           
            if (blogListItemId <= 0)
            {
                itemToSave = this.CreateBlogListItem(blogList);
            }
            else
            {
                itemToSave = Repositories.BlogListItems.GetById(blogListItemId);
            }

            itemToSave.Name = itemName;
            itemToSave.RelatedLink = relatedLink;
            itemToSave.DisplayOrder = displayOrder;
            itemToSave.BlogList = blogList;

            itemToSave = Repositories.BlogListItems.Save(itemToSave);
            return itemToSave;
        }

        public BlogListItem GetListItemById(int listItemId)
        {
            return Repositories.BlogListItems.GetById(listItemId);
        }

        public bool Delete(BlogList blogList)
        {
            return Repositories.BlogLists.Delete(blogList);
        }

        public bool DeleteListItem(BlogListItem listItem)
        {
            return Repositories.BlogListItems.Delete(listItem);
        }
    }
}
