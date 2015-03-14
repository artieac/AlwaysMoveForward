using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AlwaysMoveForward.MainSite.Models;

namespace AlwaysMoveForward.MainSite.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            CarouselModel model = new CarouselModel();
            model.CarouselItems = new List<string>();

            string[] fileNames = Directory.GetFiles(Server.MapPath("/content/images/Carousel"));

            for (int i = 0; i < fileNames.Count(); i++ )
            {
                model.CarouselItems.Add("/content/images/Carousel/" + fileNames[i].Substring(fileNames[i].LastIndexOf("\\") + 1));
            }

            return View(model);
        }
    }
}
