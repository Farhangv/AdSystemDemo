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
        [ChildActionOnly]
        public ActionResult AdBox()
        {
            var ad = new Ad() { Id = 1, Title = "فروش آپارتمان ۱۰۰ متری", Area = 100, UnitCount = 10, Age = 1 };
            return PartialView("_AdBox", ad);
        }
    }
}