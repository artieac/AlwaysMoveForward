using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlwaysMoveForward.OAuth2.BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace AlwaysMoveForward.OAuth2.Web.Controllers
{
    public class HomeController : AMFControllerBase
    {
        public HomeController(ServiceManagerBuilder serviceManagerBuilder,
                              ILoggerFactory loggerFactory) 
                              : base(serviceManagerBuilder, loggerFactory.CreateLogger<HomeController>()) { }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
