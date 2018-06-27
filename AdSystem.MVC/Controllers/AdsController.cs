using AdSystem.Models;
using AdSystem.MVC.Library;
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
        public ActionResult Index()
        {
            AdDbContext ctx = new AdDbContext();
            var model = ctx.Ads.ToList();
            return View(model);
        }
        public ActionResult Details(int id)
        {
            AdDbContext ctx = new AdDbContext();
            var ad = ctx.Ads.Find(id);
            return View(ad);
        }
        public ActionResult Create()
        {
            AdDbContext ctx = new AdDbContext();
            ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title");
            ViewBag.AdType = new SelectList(LookupHelper.GetAdTypesLookup(), "Value", "Text");
            return View();
        }

        [HttpPost]
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
            AdDbContext ctx = new AdDbContext();

            if (ModelState.IsValid)
            {
                var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                var fullPath = Path.Combine(Server.MapPath("~/images/thumbnails"), fileName);
                viewModel.ThumbnailFile.SaveAs(fullPath);
                var thumbnailClientPath = $"/images/thumbnails/{fileName}";

                Ad ad;
                if (viewModel.AdType == "sale")
                {
                    ad = new SaleAd()
                    {
                        Age = viewModel.Age,
                        Area = viewModel.Area,
                        CategoryId = viewModel.CategoryId,
                        Title = viewModel.Title,
                        UnitCount = viewModel.UnitCount,
                        PricePerUnit = viewModel.PricePerUnit,
                        ThumbnailPhotoPath = thumbnailClientPath
                    };
                }
                else {
                    ad = new RentAd()
                    {
                        Age = viewModel.Age,
                        Area = viewModel.Area,
                        CategoryId = viewModel.CategoryId,
                        Title = viewModel.Title,
                        UnitCount = viewModel.UnitCount,
                        ThumbnailPhotoPath = thumbnailClientPath,
                        Diposite = viewModel.Diposite ?? 0,
                        Rent = viewModel.Rent ?? 0
                    };
                }

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

                ctx.Ads.Add(ad);
                ctx.SaveChanges();

                TempData["Message"] = "ثبت با موفقیت انجام شد.";
                return RedirectToAction("Index", "Ads");
            }
            TempData["Message"] = "خطایی رخ داده";
            TempData["MessageClass"] = "danger";
            ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title");
            ViewBag.AdType = new SelectList(LookupHelper.GetAdTypesLookup(), "Value", "Text");
            return View(viewModel);
        }

        [NonAction]
        private static void AddMedia(HttpPostedFileBase mediaFile, Ad ad)
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

        public ActionResult GetMedia(int id)
        {
            AdDbContext ctx = new AdDbContext();
            var media = ctx.Media.Find(id);

            return File(media.FileContent, media.MimeType, media.OriginalFileName);
        }

    }
}