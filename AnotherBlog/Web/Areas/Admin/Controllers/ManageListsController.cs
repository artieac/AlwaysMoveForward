using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;
using AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Models;
using AlwaysMoveForward.AnotherBlog.Web.Code.Utilities;
using AlwaysMoveForward.AnotherBlog.Web.Controllers;
using AlwaysMoveForward.AnotherBlog.Web.Code.Filters;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Controllers
{
    [CustomAuthorization(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator)]
    public class ManageListsController : AdminBaseController
    {
        public ActionResult Index(string id)
        {
            BlogListModel model = new BlogListModel();
            model.Common = this.InitializeCommonModel(id);

            if (model.Common.TargetBlog != null)
            {
                model.BlogLists = Services.BlogListService.GetByBlog(model.Common.TargetBlog);
            }

            return this.View(model);
        }    

        public JsonResult Edit(string blogSubFolder, int listId, string listName, bool showOrdered)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.BlogService.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = null;

            if (targetBlog != null)
            {
                if (string.IsNullOrEmpty(listName))
                {
                    ViewData.ModelState.AddModelError("listName", "Please enter a name");
                }

                if (ViewData.ModelState.IsValid == true)
                {
                    using (this.Services.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            currentList = Services.BlogListService.Save(targetBlog, listId, listName, showOrdered);
                            this.Services.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            LogManager.GetLogger().Error(e);
                            this.Services.UnitOfWork.EndTransaction(false);
                        }
                    }
                }
            }

            if (currentList != null)
            {
                model.BlogListId = currentList.Id;
            }

            return this.Json(model);
        }

        public JsonResult EditListItem(string blogSubFolder, int blogListId, int editListItemId, string editListItemName, string editListItemRelatedLink, int editListItemDisplayOrder)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.BlogService.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = this.Services.BlogListService.GetById(blogListId);

            if (currentList != null)
            {
                if (string.IsNullOrEmpty(editListItemName))
                {
                    ViewData.ModelState.AddModelError("itemName", "Please enter a name for the item.");
                }

                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        currentList = this.Services.BlogListService.UpdateItem(currentList, editListItemId, editListItemName, editListItemRelatedLink, editListItemDisplayOrder);
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            model.BlogListId = currentList.Id;
            model.BlogListItemId = editListItemId;

            return this.Json(model);
        }

        public ActionResult Delete(string blogSubFolder, int listId)
        {
            BlogListModel model = new BlogListModel();
            model.Common = this.InitializeCommonModel(blogSubFolder);

            if (model.Common.TargetBlog != null)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.BlogListService.Delete(Services.BlogListService.GetById(listId));
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return this.RedirectToAction("Index", new { blogSubFolder = blogSubFolder });
        }

        public ActionResult DeleteListItem(string id, int listItemId, int listId)
        {
            BlogListModel model = new BlogListModel();
            model.Common = this.InitializeCommonModel(id);

            if (model.Common.TargetBlog != null)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        BlogList targetList = this.Services.BlogListService.GetById(listId);

                        if (targetList !=null)
                        {
                            targetList.RemoveListItem(listItemId);
                        }

                        this.Services.BlogListService.Save(targetList);
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return this.RedirectToAction("Index", new { blogSubFolder = id });
        }

        public JsonResult GetAll(string blogSubFolder)
        {
            IList<BlogList> retVal = new List<BlogList>();
            Blog targetBlog = this.Services.BlogService.GetBySubFolder(blogSubFolder);

            retVal = this.Services.BlogListService.GetByBlog(targetBlog);
            return this.Json(retVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(string id, string name, bool showOrdered)
        {
            IList<BlogList> retVal = new List<BlogList>();
            Blog targetBlog = this.Services.BlogService.GetBySubFolder(id);

            if (targetBlog != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    ViewData.ModelState.AddModelError("newListName", "Please enter a name");
                }

                if (ViewData.ModelState.IsValid == true)
                {
                    using (this.Services.UnitOfWork.BeginTransaction())
                    {
                        try
                        {
                            BlogList newList = Services.BlogListService.Save(targetBlog, -1, name, showOrdered);
                            this.Services.UnitOfWork.EndTransaction(true);
                        }
                        catch (Exception e)
                        {
                            LogManager.GetLogger().Error(e);
                            this.Services.UnitOfWork.EndTransaction(false);
                        }
                    }
                }                
            }

            return this.GetAll(id);
        }
    }
}
