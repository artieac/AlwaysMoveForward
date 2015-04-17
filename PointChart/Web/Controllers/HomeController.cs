using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AlwaysMoveForward.PointChart.Common.DomainModel;
using AlwaysMoveForward.PointChart.Web.Code.Filters;
using AlwaysMoveForward.PointChart.Web.Models;

namespace AlwaysMoveForward.PointChart.Web.Controllers
{
    public class HomeController : BaseController
    {
        // GET: /Home/
        [CookieAuthenticationParser]
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            model.OwnedCharts = this.Services.Charts.GetByCreator(this.CurrentPrincipal.CurrentUser);
            model.AssignedCharts = this.Services.Charts.GetByPointEarner(this.CurrentPrincipal.CurrentUser);

            return this.View(model);
        }

        [CookieAuthenticationParser]
        public ActionResult PointEarners()
        {
            return this.View();
        }
    }
}
