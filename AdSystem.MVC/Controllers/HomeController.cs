using AdSystem.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        private AdDbContext ctx = new AdDbContext();
        public ActionResult Index()
        {
            var model = ctx.Ads.ToList();
            return View(model);
        }

        [NonAction]
        public ActionResult AboutUs()
        {
            return Content("About Us!");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}