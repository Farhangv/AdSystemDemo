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
        
        public ActionResult Create()
        {
            AdDbContext ctx = new AdDbContext();
            ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title");
            return View();
        }

        [HttpPost]
        //public ActionResult Create(FormCollection form)
        //public ActionResult Create(int age, int pricePerUnit, string title, int area)
        public ActionResult Create(SaleAd ad)
        {
            //SaleAd sa = new SaleAd();

            //sa.Age = int.Parse(Request.Form["Age"]);
            //sa.PricePerUnit = int.Parse(Request.Form["PricePerUnit"]);
            //sa.Title = Request.Form["Title"];
            //sa.Area = int.Parse(Request.Form["Area"]);


            //sa.Age = int.Parse(form["Age"]);
            //sa.PricePerUnit = int.Parse(form["PricePerUnit"]);
            //sa.Title = form["Title"];
            //sa.Area = int.Parse(form["Area"]);

            //sa.Age = age;
            //sa.Title = title;
            //sa.PricePerUnit = pricePerUnit;
            //sa.Area = area;

            AdDbContext ctx = new AdDbContext();
            //ctx.SaleAds.Add(sa);
            ctx.SaleAds.Add(ad);
            ctx.SaveChanges();

            return Content("ثبت با موفقیت انجام شد", "text/plain");
        }


        [ChildActionOnly]
        public ActionResult AdBox()
        {
            var ad = new Ad() { Id = 1, Title = "فروش آپارتمان ۱۰۰ متری", Area = 100, UnitCount = 10, Age = 1 };
            return PartialView("_AdBox", ad);
        }
    }
}