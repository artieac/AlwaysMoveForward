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
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using AlwaysMoveForward.Common.Data.Entities;

using AnotherBlog.Common.Data.Entities;
using AnotherBlog.IntegrationService.BlogPosts.Requests;
using AnotherBlog.IntegrationService.BlogPosts.Responses;
using AnotherBlog.IntegrationService.Models;

namespace AnotherBlog.IntegrationService.BlogPosts
{
    // NOTE: If you change the class name "BlogPostService" here, you must also update the reference to "BlogPostService" in Web.config.
    public class BlogPostService : ServiceBase, IBlogPostService
    {
        public LoginResponse LoginUser(LoginRequest request)
        {
            LoginResponse retVal = new LoginResponse();

            User currentUser = Services.Users.Login(request.UserName, request.UserPassword);

            if (currentUser != null)
            {
                retVal.UserBlogInfo = Services.BlogUsers.GetUserBlogs(currentUser.UserId);
            }

            return retVal;
        }

        public GetBlogsResponse GetBlogs(GetBlogsRequest request)
        {
            GetBlogsResponse retVal = new GetBlogsResponse();
            retVal.Blogs = new List<BlogElement>();

            IList<Blog> foundBlogs = Services.Blogs.GetAll();

            if (foundBlogs != null)
            {
                for (int i = 0; i < foundBlogs.Count; i++)
                {
                    BlogElement newElement = new BlogElement();
                    newElement.BlogId = foundBlogs[i].BlogId;
                    newElement.Name = foundBlogs[i].Name;
                    newElement.Description = foundBlogs[i].Description;
                    newElement.SubFolder = foundBlogs[i].SubFolder;
                    newElement.About = foundBlogs[i].About;
                    newElement.WelcomeMessage = foundBlogs[i].WelcomeMessage;
                    newElement.ContactEmail = foundBlogs[i].ContactEmail;
                    newElement.Theme = foundBlogs[i].Theme;
                    retVal.Blogs.Add(newElement);
                }
            }

            return retVal;
        }

        public GetAllBlogEntriesByBlogResponse GetAllBlogEntriesByBlog(GetAllBlogEntriesByBlogRequest request)
        {
            GetAllBlogEntriesByBlogResponse retVal = new GetAllBlogEntriesByBlogResponse();
            retVal.BlogEntries = new List<BlogPostElement>();

            Blog targetBlog = Services.Blogs.GetById(request.BlogId);

            if (targetBlog != null)
            {
                IList<BlogPost> foundPosts = Services.BlogEntries.GetAllByBlog(targetBlog, true);

                if (foundPosts != null)
                {
                    for (int i = 0; i < foundPosts.Count; i++)
                    {
                        BlogPostElement newElement = new BlogPostElement();
                        newElement.EntryId = foundPosts[i].EntryId;
                        newElement.IsPublished = foundPosts[i].IsPublished;
                        newElement.BlogId = foundPosts[i].Blog.BlogId;

                        User author = this.Services.Users.GetById(foundPosts[i].AuthorId);
                        newElement.AuthorId = author.UserId;
                        newElement.AuthorName = author.DisplayName;
                        newElement.EntryText = foundPosts[i].EntryText;
                        newElement.Title = foundPosts[i].Title;
                        newElement.DatePosted = foundPosts[i].DatePosted;
                        newElement.Tags = new List<TagElement>();

                        IList<Tag> postTags = this.Services.Tags.GetByBlogEntryId(foundPosts[i].EntryId);

                        if (postTags != null)
                        {
                            for (int j = 0; j < postTags.Count; j++)
                            {
                                TagElement newTag = new TagElement();
                                newTag.TagId = postTags[j].Id;
                                newTag.Name = postTags[j].Name;
                                newElement.Tags.Add(newTag);
                            }
                        }

                        retVal.BlogEntries.Add(newElement);
                    }
                }
            }
            return retVal;
        }
    }
}
