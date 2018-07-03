using AdSystem.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdSystem.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private AdDbContext ctx = new AdDbContext();
        // GET: Categories
        public ActionResult Index(int id)
        {
            var ads = ctx.Ads.Where(a => a.CategoryId == id).ToList();
            return View(ads);
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