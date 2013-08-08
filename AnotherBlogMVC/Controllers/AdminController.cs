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

            IList<BlogUser> userBlogs = Services.BlogUsers.GetUserBlogs(this.CurrentPrincipal.CurrentUser.UserId);

            for (int i = 0; i < userBlogs.Count; i++)
            {
                targetModel.UserBlogs.Add((Blog)(userBlogs[i].Blog));
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

            targetModel.SortColumn = "";
            targetModel.SortAscending = true;
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
                    using (this.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            Services.SiteInfo.Save(siteName, siteUrl, siteAbout, siteContact, defaultTheme, siteAnalyticsId);
                            this.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            this.Logger.Error(e.Message);
                            this.UnitOfWork.EndTransaction(false);
                        }
                    }
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
                            this.UnitOfWork.BeginTransaction();
                            model.TargetBlog = Services.Blogs.Save(model.TargetBlog.BlogId, model.TargetBlog.Name, model.TargetBlog.SubFolder, description, about, blogWelcome, blogTheme);
                            this.UnitOfWork.EndTransaction(true);
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
            IList<BlogUser> userBlogs = Services.BlogUsers.GetUserBlogs(this.CurrentPrincipal.CurrentUser.UserId);

            for (int i = 0; i < userBlogs.Count; i++)
            {
                model.UserBlogs.Add((Blog)(userBlogs[i].Blog));
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
                    using (this.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            model.TargetBlog = Services.Blogs.Save(targetBlogId, blogName, targetSubFolder, blogDescription, blogAbout, blogWelcome, blogTheme);
                            this.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            model.TargetBlog = Services.Blogs.Create();
                            model.TargetBlog.Name = blogName;
                            model.TargetBlog.About = blogAbout;
                            model.TargetBlog.Description = blogDescription;
                            model.TargetBlog.SubFolder = targetSubFolder;
                            model.TargetBlog.WelcomeMessage = blogWelcome;

                            this.Logger.Error(e.Message);
                            this.UnitOfWork.EndTransaction(false);
                        }
                    }
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

                this.UnitOfWork.EndTransaction(true);
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

        #endregion

        #region Blog List Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ManageBlogLists(string blogSubFolder)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());

            if (model.TargetBlog != null)
            {
                model.BlogLists = Services.BlogLists.GetByBlog(model.TargetBlog);
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult DeleteBlogList(string blogSubFolder, int listId)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());

            if (model.TargetBlog != null)
            {
                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.BlogLists.Delete(Services.BlogLists.GetById(listId));
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return RedirectToAction("ManageBlogLists", new { blogSubFolder = blogSubFolder });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ShowBlogList(string blogSubFolder, int blogListId)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());
            model.CurrentList = Services.BlogLists.GetById(blogListId);
            model.CurrentListItems = Services.BlogLists.GetListItems(model.CurrentList);

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult AddBlogList(string blogSubFolder, String newListName, Boolean newListShowOrdered)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());

            if (model.TargetBlog != null)
            {
                if (newListName == "")
                {
                    ViewData.ModelState.AddModelError("newListName", "Please enter a name");
                }

                if (ViewData.ModelState.IsValid == true)
                {
                    using (this.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            model.CurrentList = Services.BlogLists.Save(model.TargetBlog, -1, newListName, newListShowOrdered);
                            this.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            this.Logger.Error(e.Message);
                            this.UnitOfWork.EndTransaction(false);
                        }
                    }
                }
            }

            return RedirectToAction("ManageBlogLists", new { blogSubFolder = blogSubFolder });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public JsonResult EditBlogList(string blogSubFolder, int listId, String listName, Boolean showOrdered)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.Blogs.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = null;

            if (targetBlog != null)
            {
                if (listName == "")
                {
                    ViewData.ModelState.AddModelError("listName", "Please enter a name");
                }

                if (ViewData.ModelState.IsValid == true)
                {
                    using (this.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            currentList = Services.BlogLists.Save(targetBlog, listId, listName, showOrdered);
                            this.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            this.Logger.Error(e.Message);
                            this.UnitOfWork.EndTransaction(false);
                        }
                    }
                }
            }

            if (currentList != null)
            {
                model.BlogListId = currentList.Id;
            }

            return Json(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult AddBlogListItem(string blogSubFolder, int blogListId, string newListItemName, string newListItemRelatedLink, int? newListItemDisplayOrder)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());

            model.CurrentList = this.Services.BlogLists.GetById(blogListId);

            if(model.CurrentList!=null)
            {
                if(newListItemName=="")
                {
                    ViewData.ModelState.AddModelError("newListItemName", "Please enter a name for the item.");
                }

                int displayOrderValue = 0;

                if (newListItemDisplayOrder.HasValue)
                {
                    displayOrderValue = newListItemDisplayOrder.Value;
                }

                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        this.Services.BlogLists.SaveBlogListItem(model.TargetBlog, model.CurrentList, -1, newListItemName, newListItemRelatedLink, displayOrderValue);
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return RedirectToAction("ShowBlogList", new { blogSubFolder = blogSubFolder, blogListId = blogListId });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public JsonResult EditBlogListItem(string blogSubFolder, int blogListId, int editListItemId, string editListItemName, string editListItemRelatedLink, int editListItemDisplayOrder)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.Blogs.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = this.Services.BlogLists.GetById(blogListId);
            BlogListItem savedItem = null;
            
            if (currentList != null)
            {
                if (editListItemName == "")
                {
                    ViewData.ModelState.AddModelError("itemName", "Please enter a name for the item.");
                }

                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        savedItem = this.Services.BlogLists.SaveBlogListItem(targetBlog, currentList, editListItemId, editListItemName, editListItemRelatedLink, editListItemDisplayOrder);
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            if (savedItem != null)
            {
                model.BlogListId = savedItem.BlogList.Id;
                model.BlogListItemId = savedItem.Id;
            }

            return Json(model);
        }
        
        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult DeleteBlogListItem(string blogSubFolder, int listItemId, int blogListId)
        {
            BlogListModel model = (BlogListModel)this.InitializeModel(blogSubFolder, new BlogListModel());

            if (model.TargetBlog != null)
            {
                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.BlogLists.DeleteListItem(Services.BlogLists.GetListItemById(listItemId));
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return RedirectToAction("ManageBlogLists", new { blogSubFolder = blogSubFolder });
        }

        #endregion

        #region Blog Post Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult ManageBlogPosts(string blogSubFolder, string filterType, string filterValue, int? page, string sortColumn, Boolean? sortAscending)
        {
            BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

            if (sortAscending.HasValue)
            {
                model.SortAscending = sortAscending.Value;
            }
            else
            {
                model.SortAscending = false;
            }

            if (sortColumn != null)
            {
                model.SortColumn = sortColumn;
            }
            else
            {
                model.SortColumn = "DateCreated";
            }

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
                    model.EntryList = Pagination.ToPagedList(Services.BlogEntries.GetAllByBlog(model.TargetBlog, false, -1, model.SortColumn, model.SortAscending), currentPageIndex, Constants.PageSize);
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

                        using (this.UnitOfWork.BeginTransaction())
                        {
                            try
                            {
                                model.PostTags = Services.Tags.AddTags(model.TargetBlog, tagInput.Split(','));
                                model.BlogPost = Services.BlogEntries.Save(model.TargetBlog, title, entryText, blogEntryId, isEntryPublished, true);
                                Services.Tags.AssociateTags(model.BlogPost, model.PostTags);
                                this.UnitOfWork.EndTransaction(true);
                            }
                            catch (Exception e)
                            {
                                this.Logger.Error(e.Message);
                                this.UnitOfWork.EndTransaction(false);
                            }
                        }
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
                        model.PostTags = Services.Tags.GetByBlogEntryId(model.BlogPost.EntryId);
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

                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        model.PostTags = Services.Tags.AddTags(model.TargetBlog, ajaxTagInput.Split(','));
                        model.BlogPost = Services.BlogEntries.Save(model.TargetBlog, ajaxTitle, ajaxEntryText, blogEntryId, isEntryPublished, true);
                        Services.BlogEntryTags.AssociateTags(model.BlogPost, model.PostTags);
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            AjaxSaveModel retVal = new AjaxSaveModel();
            retVal.EntryId = model.BlogPost.EntryId;
            retVal.BlogSubFolder = model.TargetBlog.SubFolder;

            return Json(retVal);
        }

        #endregion

        #region User Management

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ManageUsers(int? page)
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
                        using (this.UnitOfWork.BeginTransaction())
                        {
                            try
                            {
                                model.CurrentUser = Services.Users.Save(userName, password, email, targetUserId, isSiteAdmin.Value, approvedCommenter.Value, isActive.Value, userAbout, displayName);
                                this.UnitOfWork.EndTransaction(true);
                            }
                            catch (Exception e)
                            {
                                this.Logger.Error(e.Message);
                                this.UnitOfWork.EndTransaction(false);
                            }
                        }
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
        public ActionResult DeleteUser(string userId)
        {
            using (this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    int targetUserId = Int32.Parse(userId);
                    Services.Users.Delete(targetUserId);
                    this.UnitOfWork.EndTransaction(true);
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message);
                    this.UnitOfWork.EndTransaction(false);
                }
            }

            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());
            model.Roles = Services.Roles.GetAll();
            model.Blogs = Services.Blogs.GetAll();

            return RedirectToAction("ManageUsers");
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult ManageUserBlogs(string userId)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());

            int targetUser = int.Parse(userId);

            model.Blogs = Services.Blogs.GetAll();
            model.CurrentUser = Services.Users.GetById(targetUser);

            if (model.CurrentUser != null)
            {
                model.CurrentUser.UserBlogs = Services.BlogUsers.GetUserBlogs(model.CurrentUser.UserId);
            }

            return View(model);
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult AddUserBlog(string blogSubFolder, string userId, string targetBlog, string blogRole)
        {
            SiteAdminModel model = (SiteAdminModel)this.InitializeModel("", new SiteAdminModel());

            int targetUser = int.Parse(userId);
            int blogId = int.Parse(targetBlog);
            int roleId = int.Parse(blogRole);

            using (this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    Services.BlogUsers.Save(targetUser, blogId, roleId);
                    this.UnitOfWork.EndTransaction(true);
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message);
                    this.UnitOfWork.EndTransaction(false);
                }
            }

            model.Blogs = Services.Blogs.GetAll();
            model.CurrentUser = Services.Users.GetById(targetUser);
            model.CurrentUser.UserBlogs = Services.BlogUsers.GetUserBlogs(model.CurrentUser.UserId);

            return View("ManageUserBlogs", new { userId = userId });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator)]
        public ActionResult DeleteUserRole(string blogSubFolder, int? blogId, int? userId)
        {
            if (blogId != null && userId != null)
            {
                int targetBlog = Convert.ToInt32(blogId);
                int targetUser = Convert.ToInt32(userId);

                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.BlogUsers.DeleteUserBlog(targetBlog, targetUser);
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
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
                using (this.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.EntryComments.SetStatus(model.TargetBlog, id, Comment.CommentStatus.Approved);
                        this.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        this.Logger.Error(e.Message);
                        this.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return RedirectToAction("BlogManageComments", new { blogSubFolder = blogSubFolder });
        }

        [CustomAuthorization(RequiredRoles = Role.SiteAdministrator + "," + Role.Administrator + "," + Role.Blogger)]
        public ActionResult BlogDeleteComment(string blogSubFolder, int id)
        {
            using (this.UnitOfWork.BeginTransaction())
            {
                try
                {
                    BlogAdminModel model = (BlogAdminModel)this.InitializeModel(blogSubFolder, new BlogAdminModel());

                    if (model.TargetBlog != null)
                    {
                        Services.EntryComments.SetStatus(model.TargetBlog, id, Comment.CommentStatus.Deleted);
                        this.UnitOfWork.EndTransaction(true);
                    }
                }
                catch (Exception e)
                {
                    this.Logger.Error(e.Message);
                    this.UnitOfWork.EndTransaction(false);
                }
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
