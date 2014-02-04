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
using System.Text;

using AlwaysMoveForward.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.Common.Business;

using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    public class BlogUserService : AnotherBlogService
    {
        public interface IDependencies : IServiceDependencies
        {
            BlogService BlogService { get; }
            AnotherBlogUserService UserService { get; }
            RoleService RoleService { get; }
        }

        public BlogUserService(IDependencies dependencies, IAnotherBlogRepositoryManager repositoryManager) : base(dependencies.UnitOfWork, repositoryManager) 
        {
            this.BlogService = dependencies.BlogService;
            this.UserService = dependencies.UserService;
            this.RoleService = dependencies.RoleService;
        }

        private BlogService BlogService { get; set; }
        private AnotherBlogUserService UserService { get; set; }
        public RoleService RoleService { get; set; }

        public BlogUser Create()
        {
            BlogUser retVal = this.AnotherBlogRepositories.BlogUsers.Create();
            retVal.BlogUserId = this.AnotherBlogRepositories.BlogUsers.UnsavedId;
            return retVal;
        }

        public BlogUser Save(int userId, int blogId, int roleId)
        {            
            BlogUser retVal = null;

            Blog validBlog = this.BlogService.GetById(blogId);
            User validUser = this.UserService.GetById(userId);
            Role validRole = this.RoleService.GetById(roleId);

            if (validBlog != null && validUser != null && validRole != null)
            {
                retVal = AnotherBlogRepositories.BlogUsers.GetUserBlog(validUser.UserId, validBlog.BlogId);

                if (retVal == null)
                {
                    retVal = this.Create();
                }

                retVal.User = validUser;
                retVal.Blog = validBlog;
                retVal.Role = validRole;

                retVal = AnotherBlogRepositories.BlogUsers.Save(retVal);
            }

            return retVal;
        }

        public BlogUser GetUserBlog(int userId, int blogId)
        {
            return AnotherBlogRepositories.BlogUsers.GetUserBlog(userId, blogId);
        }

        public IList<BlogUser> GetUserBlogs(int userId)
        {
            return AnotherBlogRepositories.BlogUsers.GetUserBlogs(userId);
        }

        public IList<Blog> GetBlogsByUserId(int userId)
        {
            return AnotherBlogRepositories.Blogs.GetByUserId(userId);
        }

        public bool DeleteUserBlog(BlogUser targetUser)
        {
            return this.AnotherBlogRepositories.BlogUsers.Delete(targetUser);
        }

        public bool DeleteUserBlog(int blogId, int userId)
        {
            bool retVal = false;

            Blog validBlog = this.BlogService.GetById(blogId);
            User validUser = this.UserService.GetById(userId);

            if (validBlog != null && validUser != null)
            {
                retVal = AnotherBlogRepositories.BlogUsers.DeleteUserBlog(validUser.UserId, validBlog.BlogId);
            }

            return retVal;
        }
    }
}
