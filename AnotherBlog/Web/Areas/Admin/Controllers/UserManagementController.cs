﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;
using AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Models;
using AlwaysMoveForward.AnotherBlog.Web.Code.Utilities;
using AlwaysMoveForward.AnotherBlog.Web.Code.Filters;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Controllers
{
    [CustomAuthorization(RequiredRoles = RoleType.Names.SiteAdministrator + "," + RoleType.Names.Administrator)]
    public class UserManagementController : AdminBaseController
    {
        private const int UserPageSize = 25;

        public ActionResult Index(int? page)
        {
            UserModel model = new UserModel();
            int currentPageIndex = 0;

            if (page.HasValue == true)
            {
                currentPageIndex = page.Value - 1;
            }

            model.Users = Pagination.ToPagedList(Services.UserService.GetAll(), currentPageIndex, UserPageSize);

            return this.View(model);
        }

        public ActionResult Edit(bool? performSave, string userName, string password, string email, string id, bool? isSiteAdmin, bool? approvedCommenter, bool? isActive, string userAbout, string displayName, string twitterId)
        {
            UserModel model = new UserModel();
            model.Roles = RoleType.Roles;
            IList<Blog> blogs = Services.BlogService.GetAll();
            model.Blogs = new Dictionary<int, Blog>();

            for (int i = 0; i < blogs.Count; i++)
            {
                model.Blogs.Add(blogs[i].BlogId, blogs[i]);
            }

            int targetUserId = 0;

            if (!string.IsNullOrEmpty(id))
            {
                targetUserId = int.Parse(id);
                model.CurrentUser = Services.UserService.GetById(targetUserId);
            }

            if (model.CurrentUser == null)
            {
                model.CurrentUser = new AnotherBlogUser();
            }

            if (performSave.HasValue)
            {
                if (performSave.Value == true)
                {
                    if (string.IsNullOrEmpty(userName))
                    {
                        ViewData.ModelState.AddModelError("userName", "User name required.");
                    }

                    if (string.IsNullOrEmpty(email))
                    {
                        ViewData.ModelState.AddModelError("email", "Email required.");
                    }

                    if (string.IsNullOrEmpty(displayName))
                    {
                        ViewData.ModelState.AddModelError("displayName", "Display Name required.");
                    }

                    if (ViewData.ModelState.IsValid)
                    {
                        using (this.Services.UnitOfWork.BeginTransaction())
                        {
                            try
                            {
                                model.CurrentUser = Services.UserService.Save(userName, password, email, targetUserId, isSiteAdmin.Value, approvedCommenter.Value, isActive.Value, userAbout, displayName);
                                this.Services.UnitOfWork.EndTransaction(true);
                            }
                            catch (Exception e)
                            {
                                LogManager.GetLogger().Error(e);
                                this.Services.UnitOfWork.EndTransaction(false);
                            }
                        }
                    }
                    else
                    {
                        model.CurrentUser = Services.UserService.GetById(targetUserId);

                        if (model.CurrentUser == null)
                        {
                            model.CurrentUser = new AnotherBlogUser();
                            model.CurrentUser.UserName = userName;
                            model.CurrentUser.Email = email;
                        }
                    }
                }
            }

            return this.View(model);
        }

        public ActionResult Delete(string userId)
        {
            int targetUserId = Int32.Parse(userId);
            Services.UserService.Delete(targetUserId);

            UserModel model = new UserModel();
            model.Roles = RoleType.Roles;
            IList<Blog> blogs = Services.BlogService.GetAll();
            model.Blogs = new Dictionary<int, Blog>();

            for (int i = 0; i < blogs.Count; i++)
            {
                model.Blogs.Add(blogs[i].BlogId, blogs[i]);
            }


            return this.RedirectToAction("Index");
        }

        public ActionResult ManageBlogs(string userId)
        {
            UserModel model = new UserModel();

            int targetUser = int.Parse(userId);

            IList<Blog> blogs = Services.BlogService.GetAll();
            model.Blogs = new Dictionary<int, Blog>();

            for (int i = 0; i < blogs.Count; i++)
            {
                model.Blogs.Add(blogs[i].BlogId, blogs[i]);
            }

            model.CurrentUser = Services.UserService.GetById(targetUser);
            model.BlogsUserCanAccess = this.Services.BlogService.GetByUserId(this.CurrentPrincipal.CurrentUser.UserId);
            model.Roles = RoleType.Roles;

            return this.View(model);
        }

        public ActionResult AddBlog(string userId, string targetBlog, int blogRole)
        {
            UserModel model = new UserModel();

            int targetUserId = int.Parse(userId);
            int blogId = int.Parse(targetBlog);
            RoleType.Id roleId = (RoleType.Id)blogRole;

            model.CurrentUser = this.Services.UserService.AddBlogRole(targetUserId, blogId, roleId);

            IList<Blog> blogs = Services.BlogService.GetAll();
            model.Blogs = new Dictionary<int, Blog>();

            for (int i = 0; i < blogs.Count; i++)
            {
                model.Blogs.Add(blogs[i].BlogId, blogs[i]);
            }

            model.BlogsUserCanAccess = this.Services.BlogService.GetByUserId(model.CurrentUser.UserId);

            return this.RedirectToAction("ManageBlogs", new { userId = userId });
        }

        public ActionResult DeleteRole(int blogId, int userId)
        {
            this.Services.UserService.RemoveBlogRole(userId, blogId);
            return this.Redirect("/Admin/UserManagement/Edit?userId=" + userId.ToString());
        }
    }
}
