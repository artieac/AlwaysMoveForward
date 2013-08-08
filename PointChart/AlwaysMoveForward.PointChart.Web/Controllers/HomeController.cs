using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.DataLayer.Entities;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        [RequestAuthorizationFilter]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
       
            if (this.CurrentPrincipal.IsAuthenticated == true)
            {
                model.PointEarners = this.Services.PointEarner.GetAll(this.CurrentPrincipal.CurrentUser);
            }
            else
            {
                model.PointEarners = new List<PointEarner>();
            }

            return View(model);
        }
    }
}
