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
        public ActionResult Index(string blogSubFolder)
        {
            BlogListModel model = new BlogListModel();
            model.Common = this.InitializeCommonModel(blogSubFolder);

            if (model.Common.TargetBlog != null)
            {
                model.BlogLists = Services.BlogListService.GetByBlog(model.Common.TargetBlog);
            }

            return View(model);
        }

        public ActionResult DeleteList(string blogSubFolder, int listId)
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

            return RedirectToAction("Index", new { blogSubFolder = blogSubFolder });
        }

        public JsonResult EditList(string blogSubFolder, int listId, String listName, Boolean showOrdered)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.BlogService.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = null;

            if (targetBlog != null)
            {
                if (listName == "")
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

            return Json(model);
        }

        public JsonResult EditListItem(string blogSubFolder, int blogListId, int editListItemId, string editListItemName, string editListItemRelatedLink, int editListItemDisplayOrder)
        {
            AjaxBlogListModel model = new AjaxBlogListModel();
            model.BlogSubFolder = blogSubFolder;

            Blog targetBlog = this.Services.BlogService.GetBySubFolder(model.BlogSubFolder);
            BlogList currentList = this.Services.BlogListService.GetById(blogListId);

            if (currentList != null)
            {
                if (editListItemName == "")
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

            return Json(model);
        }

        public ActionResult DeleteListItem(string blogSubFolder, int listItemId, int blogListId)
        {
            BlogListModel model = new BlogListModel();
            model.Common = this.InitializeCommonModel(blogSubFolder);

            if (model.Common.TargetBlog != null)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        Services.BlogListService.DeleteItem(blogListId, listItemId);
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return RedirectToAction("Index", new { blogSubFolder = blogSubFolder });
        }

        public JsonResult GetAllListsByBlog(String blogSubFolder)
        {
            IList<BlogList> retVal = new List<BlogList>();
            Blog targetBlog = this.Services.BlogService.GetBySubFolder(blogSubFolder);

            retVal = this.Services.BlogListService.GetByBlog(targetBlog);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddList(string blogSubFolder, String name, Boolean showOrdered)
        {
            IList<BlogList> retVal = new List<BlogList>();
            Blog targetBlog = this.Services.BlogService.GetBySubFolder(blogSubFolder);

            if (targetBlog != null)
            {
                if (name == "")
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

            return this.GetAllListsByBlog(blogSubFolder);
        }

        public JsonResult GetBlogListItems(int listId)
        {
            BlogList retVal = this.Services.BlogListService.GetById(listId);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}
