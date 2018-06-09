using AdSystem.Models;
using AdSystem.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdSystem.MVC.Controllers
{
    public class AdsController : Controller
    {
        public ActionResult ShowCreateForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SubmitCreateForm()
        {
            SaleAd sa = new SaleAd();
            sa.Age = int.Parse(Request.Form["Age"]);
            sa.PricePerUnit = int.Parse(Request.Form["PricePerUnit"]);
            sa.Title = Request.Form["Title"];
            sa.Area = int.Parse(Request.Form["Area"]);

            AdDbContext ctx = new AdDbContext();
            ctx.SaleAds.Add(sa);
            ctx.SaveChanges();

            return Content("ثبت با موفقیت انجام شد");
        }


        [ChildActionOnly]
        public ActionResult AdBox()
        {
            var ad = new Ad() { Id = 1, Title = "فروش آپارتمان ۱۰۰ متری", Area = 100, UnitCount = 10, Age = 1 };
            return PartialView("_AdBox", ad);
        }
    }
}