using AdSystem.Models;
using AdSystem.MVC.Models;
using AdSystem.MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
<<<<<<< HEAD
        public ActionResult Create(AdsCreateViewModel viewModel)
        {
            var extension = "";
            if (viewModel.ThumbnailFile != null)
            {
                extension = Path.GetExtension(viewModel.ThumbnailFile.FileName).ToLower();


                if (viewModel.ThumbnailFile.ContentLength / 1024 >= 150)
                {
                    ModelState.AddModelError("ThumbnailFile", "سایز فایل باید کمتر از ۱۵۰ کیلو بایت باشد");
                    ModelState.AddModelError("", "این خطای فرم نمونه است");
                }
                if (
                        !(extension == ".jpg" || extension == ".jpeg" ||
                         extension == ".png" || extension == ".gif")
                    )
                {
                    ModelState.AddModelError("ThumbnailFile", "فرمت تصویر مورد قبول نیست");
                }
            }
            if (ModelState.IsValid)
            {
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var fullPath = Path.Combine(Server.MapPath("~/images/thumbnails"), fileName);
                viewModel.ThumbnailFile.SaveAs(fullPath);
                var thumbnailClientPath = $"/images/thumbnails/{fileName}";

                SaleAd ad = new SaleAd()
                {
                    Age = viewModel.Age,
                    Area = viewModel.Area,
                    CategoryId = viewModel.CategoryId,
                    Title = viewModel.Title,
                    UnitCount = viewModel.UnitCount,
                    PricePerUnit = viewModel.PricePerUnit,
                    ThumbnailPhotoPath = thumbnailClientPath
                };

                if (viewModel.MediaFile1 != null)
                {
                    AddMedia(viewModel.MediaFile1, ad);
                }
                if (viewModel.MediaFile2 != null)
                {
                    AddMedia(viewModel.MediaFile2, ad);
                }
                if (viewModel.MediaFile3 != null)
                {
                    AddMedia(viewModel.MediaFile3, ad);
                }

                AdDbContext ctx = new AdDbContext();
                ctx.Ads.Add(ad);
                ctx.SaveChanges();

                return Content("ثبت با موفقیت انجام شد", "text/plain");
            }
            else
            {
                //return Content("خطای اعتبار سنجی", "text/plain");
                AdDbContext ctx = new AdDbContext();
                ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title");

                return View(viewModel);
            }
=======
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
>>>>>>> parent of 269c7cc... Session36-970321
        }

        [NonAction]
        private static void AddMedia(HttpPostedFileBase mediaFile, SaleAd ad)
        {
            var mediaExt = Path.GetExtension(mediaFile.FileName);
            MemoryStream ms = new MemoryStream();
            mediaFile.InputStream.CopyTo(ms);
            byte[] content = ms.ToArray();

            Media media = new Media()
            {
                Extension = mediaExt,
                FileSize = mediaFile.ContentLength,
                MimeType = mediaFile.ContentType,
                OriginalFileName = mediaFile.FileName,
                Title = string.Empty,
                FileContent = content
            };
            if (ad.Media == null)
            {
                ad.Media = new List<Media>();
            }
            ad.Media.Add(media);
        }

        [ChildActionOnly]
        public ActionResult AdBox()
        {
            var ad = new Ad() { Id = 1, Title = "فروش آپارتمان ۱۰۰ متری", Area = 100, UnitCount = 10, Age = 1 };
            return PartialView("_AdBox", ad);
        }


        //[HttpPost]
        //public ActionResult Create(SaleAd ad, HttpPostedFileBase thumbnailFile)
        //{
        //    //HttpPostedFileBase thumbnail =  Request.Files["ThumbnailFile"];
        //    var extension = Path.GetExtension(thumbnailFile.FileName).ToLower();

        //    if (thumbnailFile.ContentLength / 1024 <= 150 &&
        //        (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif"))
        //    {

        //        AdDbContext ctx = new AdDbContext();

        //        var fileName = $"{Guid.NewGuid().ToString()}{extension}";
        //        //var fullPath = $"{Server.MapPath("~/images/thumbnails")}/{fileName}";
        //        var fullPath = Path.Combine(Server.MapPath("~/images/thumbnails"), fileName);
        //        thumbnailFile.SaveAs(fullPath);
        //        ad.ThumbnailPhotoPath = $"/images/thumbnails/{fileName}";
        //        ctx.SaleAds.Add(ad);
        //        ctx.SaveChanges();

        //        return Content("ثبت با موفقیت انجام شد", "text/plain");
        //    }
        //    else
        //    {
        //        return Content("فایل ارسالی مناسب نیست", "text/plain");
        //    }
        //}

    }
}