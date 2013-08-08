using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AnotherBlog.Common.Utilities;
using AnotherBlog.Common.Data.Entities;
using AnotherBlog.Core.Service;
using AnotherBlog.MVC.Models.Admin;
using AnotherBlog.MVC.Utilities;

namespace AnotherBlog.MVC.Controllers
{
    public class AdminController : BaseController
    {
        public AdminModel InitializeModel(string targetBlog, AdminModel targetModel)
        {
            targetModel.UserBlogs = new List<Blog>();

            for (int i = 0; i < this.CurrentPrincipal.CurrentUser.UserBlogs.Count; i++)
            {
                targetModel.UserBlogs.Add(this.CurrentPrincipal.CurrentUser.UserBlogs[i].Blog);
            }

            if(targetBlog!=null)
            {
                targetModel.TargetBlog = Services.Blogs.GetBySubFolder(targetBlog);
            }
            else
            {
                targetModel.TargetBlog = null;
            }

            if (targetModel.TargetBlog == null)
            {
                if (targetModel.UserBlogs.Count > 0)
                {
                    targetModel.TargetBlog = targetModel.UserBlogs[0];
                }
            }

            return targetModel;
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult Index(string targetBlog)
        {
            return View(this.InitializeModel(targetBlog, new BlogAdminModel()));
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult EditSiteInfo(string blogSubFolder, string siteName, string siteUrl, string siteAbout, string siteContact, string defaultTheme, string siteAnalyticsId)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());

            if (siteName != null && siteUrl != null && siteAbout != null)
            {
                if (siteName == "")
                {
                    ViewData.ModelState.AddModelError("siteName", "Please enter a name for your site.");
                }

                if (siteUrl == "")
                {
                    ViewData.ModelState.AddModelError("siteUrl", "Please enter your sites url.");
                }

                if (siteAbout == "")
                {
                    ViewData.ModelState.AddModelError("siteAbout", "Please enter an about message for your site.");
                }

                if (ViewData.ModelState.IsValid)
                {
                    Services.SiteInfo.Save(siteName, siteUrl, siteAbout, siteContact, defaultTheme, siteAnalyticsId);
                }
            }

            model.SiteInfo = Services.SiteInfo.GetSiteInfo();

            if (model.SiteInfo == null)
            {
                model.SiteInfo = Services.SiteInfo.Create();
            }

            MvcApplication.SiteInfo = model.SiteInfo;

            return View(model);
        }

        #region Blog Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult ManageBlogs()
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());
            model.Blogs = Services.Blogs.GetAll();

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult BlogPreferences(string blogSubFolder, string description, string about, string blogWelcome, bool? performSave, string blogTheme)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                if (performSave.HasValue)
                {
                    if (performSave.Value == true)
                    {
                        if (description == "")
                        {
                            ViewData.ModelState.AddModelError("description", "Please enter a description.");
                        }

                        if (ViewData.ModelState.IsValid)
                        {
                            model.TargetBlog = Services.Blogs.Save(model.TargetBlog.BlogId, model.TargetBlog.Name, model.TargetBlog.SubFolder, description, about, blogWelcome, blogTheme);
                        }
                    }
                }
            }
            else
            {
                model.TargetBlog = model.UserBlogs[0];
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator)]
        public ActionResult EditBlog(string blogId, string blogName, string blogAbout, string blogDescription, string targetSubFolder, string blogWelcome, string savingBlog, string blogTheme)
        {
            SiteAdminModel model = new SiteAdminModel();

            model.UserBlogs = new List<Blog>();

            for (int i = 0; i < this.CurrentPrincipal.CurrentUser.UserBlogs.Count; i++)
            {
                model.UserBlogs.Add(this.CurrentPrincipal.CurrentUser.UserBlogs[i].Blog);
            }

            if (savingBlog != null)
            {
                if (blogName == "")
                {
                    ViewData.ModelState.AddModelError("blogName", "Blog name is required.");
                }

                if (targetSubFolder == "")
                {
                    ViewData.ModelState.AddModelError("targetSubFolder", "Sub folder is required.");
                }

                int targetBlogId = int.Parse(blogId);

                if (ViewData.ModelState.IsValid)
                {
                    model.TargetBlog = Services.Blogs.Save(targetBlogId, blogName, targetSubFolder, blogDescription, blogAbout, blogWelcome, blogTheme);
                }
                else
                {
                    model.TargetBlog = Services.Blogs.GetById(targetBlogId);

                    if (model.TargetBlog == null)
                    {
                        model.TargetBlog = Services.Blogs.Create();
                        model.TargetBlog.Name = blogName;
                        model.TargetBlog.About = blogAbout;
                        model.TargetBlog.Description = blogDescription;
                        model.TargetBlog.SubFolder = targetSubFolder;
                        model.TargetBlog.WelcomeMessage = blogWelcome;
                    }
                }
            }
            else
            {
                if (blogId != null)
                {
                    model.TargetBlog = Services.Blogs.GetById(Convert.ToInt32(blogId));
                }
                else
                {
                    model.TargetBlog = Services.Blogs.Create();
                }
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult BlogManageLinks(string blogSubFolder, bool? performSave, string linkName, string url)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                if (performSave.HasValue)
                {
                    if (performSave.Value == true)
                    {
                        if (linkName == "")
                        {
                            ViewData.ModelState.AddModelError("linkName", "Please enter a url name.");
                        }

                        if (url == "")
                        {
                            ViewData.ModelState.AddModelError("url", "Please enter a valid url.");
                        }

                        if (!url.Contains("http://"))
                            url = "http://" + url;

                        if (ViewData.ModelState.IsValid)
                        {
                            Services.BlogLinks.Save(model.TargetBlog, linkName, url);
                        }
                    }
                }
            }

            model.BlogRoll = Services.BlogLinks.GetAllByBlog(model.TargetBlog);

            return View(model);
        }

        #endregion

        #region Blog Post Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogManagePosts(string blogSubFolder, string filterType, string filterValue, int? page)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                int currentPageIndex = 0;

                if (page.HasValue == true)
                {
                    currentPageIndex = page.Value - 1;
                }

                if (filterType == "tag")
                {
                    model.EntryList = Pagination.ToPagedList(Services.BlogEntries.GetByTag(model.TargetBlog, filterValue, false), currentPageIndex, Constants.PageSize);
                }
                else if (filterType == "month")
                {
                    DateTime filterDate = DateTime.ParseExact(filterValue, "MM-dd-yyyy", System.Threading.Thread.CurrentThread.CurrentCulture);
                    model.EntryList = Pagination.ToPagedList(Services.BlogEntries.GetByMonth(model.TargetBlog, filterDate, false), currentPageIndex, Constants.PageSize);
                }
                else
                {
                    model.EntryList = Pagination.ToPagedList(Services.BlogEntries.GetAllByBlog(model.TargetBlog, false), currentPageIndex, Constants.PageSize);
                }
            }
            else
            {
                model.EntryList = new PagedList<BlogPost>();
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult EditBlogPost(string blogSubFolder, bool? performSave, int? entryId, string title, string entryText, string tagInput, string isPublished)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());
            
            if (model.TargetBlog != null)
            {
                int blogEntryId = 0;

                if (entryId.HasValue)
                {
                    blogEntryId = entryId.Value;
                }

                if (performSave.HasValue)
                {
                    if (performSave.Value == true)
                    {
                        bool isEntryPublished = false;

                        if (isPublished != null)
                        {
                            if (isPublished == "on")
                            {
                                isEntryPublished = true;
                            }
                        }

                        model.PostTags = Services.Tags.AddTags(model.TargetBlog, tagInput.Split(','));
                        model.BlogPost = Services.BlogEntries.Save(model.TargetBlog, title, entryText, blogEntryId, isEntryPublished, true);
                        Services.BlogEntryTags.AssociateTags(model.BlogPost, model.PostTags);
                    }
                }
                else
                {
                    model.BlogPost = Services.BlogEntries.GetById(model.TargetBlog, blogEntryId);

                    if (model.BlogPost == null)
                    {
                        model.BlogPost = Services.BlogEntries.Create(model.TargetBlog);
                        model.PostTags = new List<Tag>();
                    }
                    else
                    {
                        model.PostTags = model.BlogPost.Tags;
                    }
                }
            }
            else
            {
                RedirectToAction("Index");
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public JsonResult AjaxBlogPostSave(string blogSubFolder, string ajaxTitle, string ajaxEntryText, string ajaxEntryId, string ajaxTagInput, string ajaxIsPublished)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                int blogEntryId = 0;

                if (ajaxEntryId != "")
                {
                    blogEntryId = int.Parse(ajaxEntryId);
                }

                bool isEntryPublished = false;

                if (ajaxIsPublished != null)
                {
                    if (ajaxIsPublished == "true")
                    {
                        isEntryPublished = true;
                    }
                }

                model.PostTags = Services.Tags.AddTags(model.TargetBlog, ajaxTagInput.Split(','));
                model.BlogPost = Services.BlogEntries.Save(model.TargetBlog, ajaxTitle, ajaxEntryText, blogEntryId, isEntryPublished, true);
                Services.BlogEntryTags.AssociateTags(model.BlogPost, model.PostTags);
            }

            AjaxSaveModel retVal = new AjaxSaveModel();
            retVal.EntryId = model.BlogPost.EntryId;
            retVal.BlogSubFolder = model.TargetBlog.SubFolder;

            return Json(retVal);
        }

        #endregion

        #region User Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ManageUsers(string blogSubFolder, int? page)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());
            int currentPageIndex = 0;

            if (page.HasValue == true)
            {
                currentPageIndex = page.Value - 1;
            }

            model.Users = Pagination.ToPagedList(Services.Users.GetAll(), currentPageIndex, Constants.PageSize);

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult EditUser(bool? performSave, string userName, string password, string email, string userId, bool? isSiteAdmin, bool? approvedCommenter, bool? isActive, string userAbout, string displayName, string twitterId)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());
            model.Roles = Services.Roles.GetAll();
            model.Blogs = Services.Blogs.GetAll();

            int targetUserId = 0;

            if (userId != null && userId != "")
            {
                targetUserId = int.Parse(userId);
                model.CurrentUser = Services.Users.GetById(targetUserId);
            }

            if (model.CurrentUser == null)
            {
                model.CurrentUser = Services.Users.Create();
            }

            if (performSave.HasValue)
            {
                if (performSave.Value == true)
                {
                    if (userName == "")
                    {
                        ViewData.ModelState.AddModelError("userName", "User name required.");
                    }

                    if (email == "")
                    {
                        ViewData.ModelState.AddModelError("email", "Email required.");
                    }

                    if (displayName == "")
                    {
                        ViewData.ModelState.AddModelError("displayName", "Display Name required.");
                    }

                    if (ViewData.ModelState.IsValid)
                    {
                        model.CurrentUser = Services.Users.Save(userName, password, email, targetUserId, isSiteAdmin.Value, approvedCommenter.Value, isActive.Value, userAbout, displayName);
                    }
                    else
                    {
                        model.CurrentUser = Services.Users.GetById(targetUserId);

                        if (model.CurrentUser == null)
                        {
                            model.CurrentUser = Services.Users.Create();
                            model.CurrentUser.UserName = userName;
                            model.CurrentUser.Email = email;
                        }
                    }
                }
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ManageUserBlogs(string userId)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());

            int targetUser = int.Parse(userId);

            model.Blogs = Services.Blogs.GetAll();
            model.CurrentUser = Services.Users.GetById(targetUser);

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult AddUserBlog(string blogSubFolder, string userId, string targetBlog, string blogRole)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());

            int targetUser = int.Parse(userId);
            int blogId = int.Parse(targetBlog);
            int roleId = int.Parse(blogRole);

            Services.BlogUsers.Save(targetUser, blogId, roleId);
            model.Blogs = Services.Blogs.GetAll();
            model.CurrentUser = Services.Users.GetById(targetUser);

            return View("ManageUserBlogs", model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult DeleteUserRole(string blogSubFolder, int? blogId, int? userId)
        {
            if (blogId != null && userId != null)
            {
                int targetBlog = Convert.ToInt32(blogId);
                int targetUser = Convert.ToInt32(userId);

                Services.BlogUsers.DeleteUserBlog(targetBlog, targetUser);
            }

            return Redirect("/Admin/EditUser?userId=" + userId.ToString());
        }

        #endregion

        #region Comment Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogUnapprovedComments(string blogSubFolder)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                model.Comments = Services.EntryComments.GetAllUnapproved(model.TargetBlog);
            }
            else
            {
                model.Comments = new List<Comment>();
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogApproveComment(string blogSubFolder, int id)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                Services.EntryComments.SetStatus(model.TargetBlog, id, Comment.CommentStatus.Approved);
            }

            return RedirectToAction("BlogManageComments", new { blogSubFolder = blogSubFolder });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogDeleteComment(string blogSubFolder, int id)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (model.TargetBlog != null)
            {
                Services.EntryComments.SetStatus(model.TargetBlog, id, Comment.CommentStatus.Deleted);
            }

            return RedirectToAction("BlogManageComments", new { blogSubFolder = blogSubFolder });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogManageComments(string blogSubFolder, string commentFilter)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());
            model.CommentFilter = commentFilter;

            if (model.TargetBlog != null)
            {
                if (commentFilter != null && commentFilter != "")
                {
                    if (commentFilter == "Approved")
                    {
                        model.Comments = Services.EntryComments.GetAllApproved(model.TargetBlog);
                    }
                    else if (commentFilter == "Unapproved")
                    {
                        model.Comments = Services.EntryComments.GetAllUnapproved(model.TargetBlog);
                    }
                    else if (commentFilter == "Deleted")
                    {
                        model.Comments = Services.EntryComments.GetAllDeleted(model.TargetBlog);
                    }
                    else
                    {
                        model.Comments = Services.EntryComments.GetAll(model.TargetBlog);
                    }
                }
                else
                {
                    model.Comments = Services.EntryComments.GetAll(model.TargetBlog);
                }
            }
            else
            {
                model.Comments = new List<Comment>();
            }

            return View(model);
        }

        #endregion
    }
}
