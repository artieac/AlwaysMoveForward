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

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class BlogListService : AnotherBlogService
    {
        public BlogListService(IUnitOfWork unitOfWork, IBlogListRepository blogListRepository) : base(unitOfWork) 
        {
            this.BlogListRepository = blogListRepository;
        }

        protected IBlogListRepository BlogListRepository { get; private set; }

        public BlogList Create(Blog targetBlog)
        {
            BlogList retVal = new BlogList();
            retVal.Blog = targetBlog;

            return retVal;
        }

        public BlogListItem CreateListItem(BlogList blogList)
        {
            BlogListItem retVal = new BlogListItem();
            retVal.Id = -1;
            return retVal;
        }

        public BlogList GetById(int blogListId)
        {
            return this.BlogListRepository.GetById(blogListId);
        }

        public IList<BlogList> GetByBlog(Blog targetBlog)
        {
            return this.BlogListRepository.GetByBlog(targetBlog.BlogId);
        }

        public bool DeleteBlogList(BlogList targetBlogList)
        {
            return this.BlogListRepository.Delete(targetBlogList);
        }

        public BlogList GetByName(Blog targetBlog, string listName)
        {
            BlogList retVal = null;

            if (targetBlog != null)
            {
                retVal = this.BlogListRepository.GetByNameAndBlogId(listName, targetBlog.BlogId);
            }

            return retVal;
        }

        public IList<string> GetListNames(Blog targetBlog)
        {
            IList<string> retVal = new List<string>();

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

        public BlogList Save(Blog targetBlog, int blogListId, string name, bool showOrdered)
        {
            BlogList itemToSave = null;

            if (blogListId <= 0)
            {
                itemToSave = this.Create(targetBlog);
            }
            else
            {
                itemToSave = this.BlogListRepository.GetByIdAndBlogId(blogListId, targetBlog.BlogId);
            }

            itemToSave.Name = name;
            itemToSave.ShowOrdered = showOrdered;
            itemToSave.Blog = targetBlog;

            itemToSave = this.BlogListRepository.Save(itemToSave);
            return itemToSave;
        }

        public BlogList AddItem(BlogList blogList, string itemName, string relatedLink, int displayOrder)
        {
            return this.UpdateItem(blogList, -1, itemName, relatedLink, displayOrder);
        }

        public BlogList UpdateItem(BlogList blogList, int blogListItemId, string itemName, string relatedLink, int displayOrder)
        {
            BlogList retVal = blogList;

            BlogListItem targetItem = retVal.Items.FirstOrDefault(t => t.Id == blogListItemId);

            if (targetItem == null)
            {
                targetItem = this.CreateListItem(blogList);
                retVal.Items.Add(targetItem);
            }

            targetItem.Name = itemName;
            targetItem.RelatedLink = relatedLink;
            targetItem.DisplayOrder = displayOrder;

            retVal = this.BlogListRepository.Save(retVal);
            return retVal;
        }

        public bool Delete(BlogList blogList)
        {
            blogList.Items.Clear();
            return this.BlogListRepository.Delete(blogList);
        }

        public BlogList DeleteItem(int blogListId, int listItemId)
        {
            return this.DeleteItem(this.GetById(blogListId), listItemId);
        }

        public BlogList DeleteItem(BlogList blogList, int listItemId)
        {
            BlogList retVal = blogList;

            if (retVal != null)
            {
                if (retVal.RemoveListItem(listItemId))
                {
                    retVal = this.BlogListRepository.Save(retVal);
                }
            }

            return retVal;
        }
    }
}
