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
        public BlogUserService(IUnitOfWork unitOfWork, BlogService blogService, UserService userService, IBlogUserRepository blogUserRepository) : base(unitOfWork) 
        {
            this.BlogService = blogService;
            this.UserService = userService;
            this.BlogUserRepository = blogUserRepository;
        }

        protected IBlogUserRepository BlogUserRepository { get; private set; }

        private BlogService BlogService { get; set; }
        private UserService UserService { get; set; }

        public BlogUser Create()
        {
            return new BlogUser();
        }

        public BlogUser Save(int userId, int blogId, RoleType.Id roleId)
        {            
            BlogUser retVal = null;

            Blog validBlog = this.BlogService.GetById(blogId);
            User validUser = this.UserService.GetById(userId);
            
            if (validBlog != null && validUser != null)
            {
                retVal = this.BlogUserRepository.GetUserBlog(validUser.UserId, validBlog.BlogId);

                if (retVal == null)
                {
                    retVal = this.Create();
                }

                retVal.User = validUser;
                retVal.Blog = validBlog;
                retVal.Role = roleId;

                retVal = this.BlogUserRepository.Save(retVal);
            }

            return retVal;
        }

        public BlogUser GetUserBlog(int userId, int blogId)
        {
            return this.BlogUserRepository.GetUserBlog(userId, blogId);
        }

        public IList<BlogUser> GetUserBlogs(int userId)
        {
            return this.BlogUserRepository.GetUserBlogs(userId);
        }

        public IList<Blog> GetBlogsByUserId(int userId)
        {
            return this.BlogService.GetByUserId(userId);
        }

        public bool DeleteUserBlog(BlogUser targetUser)
        {
            return this.BlogUserRepository.Delete(targetUser);
        }

        public bool DeleteUserBlog(int blogId, int userId)
        {
            bool retVal = false;

            Blog validBlog = this.BlogService.GetById(blogId);
            User validUser = this.UserService.GetById(userId);

            if (validBlog != null && validUser != null)
            {
                retVal = this.BlogUserRepository.DeleteUserBlog(validUser.UserId, validBlog.BlogId);
            }

            return retVal;
        }
    }
}
