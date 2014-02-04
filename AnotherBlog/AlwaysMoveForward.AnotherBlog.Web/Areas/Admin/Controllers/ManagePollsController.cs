using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.IO;

using AlwaysMoveForward.Common.Utilities;
using AlwaysMoveForward.Common.DomainModel.Poll;
using AlwaysMoveForward.AnotherBlog.Common.DomainModel;
using AlwaysMoveForward.AnotherBlog.BusinessLayer.Service;
using AlwaysMoveForward.AnotherBlog.Web.Models.BlogModels;
using AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Models;
using AlwaysMoveForward.AnotherBlog.Web.Code.Utilities;
using AlwaysMoveForward.AnotherBlog.Web.Code.Filters;

namespace AlwaysMoveForward.AnotherBlog.Web.Areas.Admin.Controllers
{
    [RequestAuthenticationFilter]
    public class ManagePollsController : AdminBaseController
    {
        private const int PollPageSize = 25;

        [AdminAuthorizationFilter(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator + "," + RoleType.Blogger, IsBlogSpecific = false)]
        public ActionResult Index(int? page)
        {
            int currentPageIndex = 0;

            if (page.HasValue == true)
            {
                currentPageIndex = page.Value - 1;
            }

            ManagePollsModel model = new ManagePollsModel();
            model.Common = this.InitializeCommonModel();
            model.Polls = Pagination.ToPagedList(this.Services.PollService.GetAll(), currentPageIndex, PollPageSize);
            return View(model);
        }

        [AdminAuthorizationFilter(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator + "," + RoleType.Blogger, IsBlogSpecific = false)]
        public JsonResult GetAll(int? page)
        {
            int currentPageIndex = 0;

            if (page.HasValue == true)
            {
                currentPageIndex = page.Value - 1;
            }

            IPagedList<PollQuestion> retVal = Pagination.ToPagedList(this.Services.PollService.GetAll(), currentPageIndex, PollPageSize);
            return Json(retVal, JsonRequestBehavior.AllowGet);
         }

        [AdminAuthorizationFilter(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator + "," + RoleType.Blogger, IsBlogSpecific = false)]
        public JsonResult GetById(int pollQuestionId)
        {
            PollQuestion retVal = this.Services.PollService.GetById(pollQuestionId);
            return Json(retVal, JsonRequestBehavior.AllowGet);
        }

        [AdminAuthorizationFilter(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator + "," + RoleType.Blogger, IsBlogSpecific = false)]
        public JsonResult AddPoll(String title, String question)
        {
            IList<PollQuestion> retVal = new List<PollQuestion>();

            if (title == "")
            {
                ViewData.ModelState.AddModelError("title", "Please enter a title");
            }

            if (question == "")
            {
                ViewData.ModelState.AddModelError("question", "Please enter a question");
            }

            if (ViewData.ModelState.IsValid == true)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        PollQuestion newPoll = Services.PollService.AddPollQuestion(question, title);
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return Json(this.Services.PollService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [AdminAuthorizationFilter(RequiredRoles = RoleType.SiteAdministrator + "," + RoleType.Administrator + "," + RoleType.Blogger, IsBlogSpecific = false)]
        public JsonResult AddPollOption(int pollQuestionId, String optionText)
        {
            PollQuestion retVal = null;

            if (optionText == "")
            {
                ViewData.ModelState.AddModelError("optionText", "Please enter a text for the option");
            }

            if (ViewData.ModelState.IsValid == true)
            {
                using (this.Services.UnitOfWork.BeginTransaction())
                {
                    try
                    {
                        retVal = Services.PollService.AddPollOption(pollQuestionId, optionText);
                        this.Services.UnitOfWork.EndTransaction(true);
                    }
                    catch (Exception e)
                    {
                        LogManager.GetLogger().Error(e);
                        this.Services.UnitOfWork.EndTransaction(false);
                    }
                }
            }

            return Json(retVal, JsonRequestBehavior.AllowGet);
        }
    }
}