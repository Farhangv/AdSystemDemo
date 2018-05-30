using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [NonAction]
        public ActionResult AboutUs()
        {
            return Content("About Us!");
        }
    }
}