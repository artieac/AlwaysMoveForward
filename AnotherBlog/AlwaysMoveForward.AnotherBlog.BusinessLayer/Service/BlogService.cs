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
using System.Collections.Generic;
using System.Linq;

using AlwaysMoveForward.Common.DataLayer;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.Common.DataLayer.Repositories;
using AlwaysMoveForward.AnotherBlog.Common.DataLayer.Repositories;

namespace AlwaysMoveForward.AnotherBlog.BusinessLayer.Service
{
    /// <summary>
    /// A managing class for a blog objects business rules.
    /// </summary>
    public class BlogService : AnotherBlogService
    {
        public BlogService(IServiceDependencies dependencies, IAnotherBlogRepositoryManager repositoryManager) : base(dependencies.UnitOfWork, repositoryManager) { }
        /// <summary>
        /// Initialize and instantiate a Blog object instance.
        /// </summary>
        /// <returns></returns>
        public Blog Create()
        {
            Blog retVal = this.AnotherBlogRepositories.Blogs.Create();
            retVal.BlogId = this.AnotherBlogRepositories.Blogs.UnsavedId;
            return retVal;
        }
        /// <summary>
        /// Get the default blog for the site (the first one created)
        /// </summary>
        /// <returns></returns>
        public Blog GetDefaultBlog()
        {
            return AnotherBlogRepositories.Blogs.GetById(1);
        }
        /// <summary>
        /// Get all blogs configured in the system.
        /// </summary>
        /// <returns></returns>
        public IList<Blog> GetAll()
        {
            return AnotherBlogRepositories.Blogs.GetAll();
        }
        /// <summary>
        /// Get all blogs that a user is associated with (via different security roles)
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<Blog> GetByUserId(int userId)
        {
            return AnotherBlogRepositories.Blogs.GetByUserId(userId);
        }
        /// <summary>
        /// Get a particular blog by an id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Blog GetById(int id)
        {
            return AnotherBlogRepositories.Blogs.GetById(id);
        }
        /// <summary>
        /// Delete a blog entry.
        /// </summary>
        /// <param name="blogId"></param>
        public void Delete(int blogId)
        {
            Blog targetBlog = AnotherBlogRepositories.Blogs.GetById(blogId);

            if (targetBlog != null)
            {
                AnotherBlogRepositories.Blogs.Delete(targetBlog);
            }
        }
        /// <summary>
        /// Get a particular blog by its name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Blog GetByName(string name)
        {
            return AnotherBlogRepositories.Blogs.GetByName(name);
        }
        /// <summary>
        /// Get a particular blog by its site subfolder
        /// </summary>
        /// <param name="subFolder"></param>
        /// <returns></returns>
        public Blog GetBySubFolder(string subFolder)
        {
            return AnotherBlogRepositories.Blogs.GetBySubFolder(subFolder);
        }
        /// <summary>
        /// Save a blog instance and its configuration settings.
        /// </summary>
        /// <param name="blogId"></param>
        /// <param name="name"></param>
        /// <param name="subFolder"></param>
        /// <param name="description"></param>
        /// <param name="about"></param>
        /// <param name="blogWelcome"></param>
        /// <returns></returns>
        public Blog Save(int blogId, string name, string subFolder, string description, string about, string blogWelcome, string blogTheme)
        {
            Blog itemToSave = null;

            if (blogId <= 0)
            {
                itemToSave = this.Create();
            }
            else
            {
                itemToSave = AnotherBlogRepositories.Blogs.GetById(blogId);
            }

            itemToSave.Name = name;
            itemToSave.SubFolder = subFolder;
            itemToSave.Description = description;
            itemToSave.About = about;
            itemToSave.WelcomeMessage = blogWelcome;
            itemToSave.Theme = blogTheme;

            return AnotherBlogRepositories.Blogs.Save(itemToSave);
        }
    }
}
