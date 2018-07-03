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
        public ActionResult Index(int? page)
        { 
            var model = ctx.Ads
                .OrderByDescending(p => p.Id)
                .Skip(((page ?? 1) - 1) * 6)
                .Take(6)
                .ToList();

            ViewBag.CurrentPage = page ?? 1;
            var totalRecords = ctx.Ads.Count();
            ViewBag.PageCount = totalRecords % 6 == 0 ? totalRecords / 6 : (totalRecords / 6) + 1;
            

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