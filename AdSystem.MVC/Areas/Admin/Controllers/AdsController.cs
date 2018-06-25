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

namespace AdSystem.MVC.Areas.Admin.Controllers
{
    public class AdsController : Controller
    {
        public ActionResult Index()
        {
            AdDbContext ctx = new AdDbContext();
            return View(ctx.Ads.ToList());
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


        public ActionResult Edit(int id)
        {
            AdDbContext ctx = new AdDbContext();
            var model = ctx.Ads.Find(id);
            ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title", model.CategoryId);
            ViewBag.AdType = new SelectList(LookupHelper.GetAdTypesLookup(), "Value", "Text", 
                    model is SaleAd ? "sale" : "rent"
                );
            AdsEditViewModel viewModel = new AdsEditViewModel()
            {
                Id = model.Id,
                Title = model.Title,
                CategoryId = model.CategoryId,
                Age = model.Age,
                Area = model.Area,
                AdType = model is SaleAd ? "sale" : "rent",
                UnitCount = model.UnitCount,
                ThumbnailPhotoPath = model.ThumbnailPhotoPath,
                
            };

            if (model is SaleAd)
            {
                viewModel.PricePerUnit = ((SaleAd)model).PricePerUnit;
            }
            else
            {
                viewModel.Diposite = ((RentAd)model).Diposite;
                viewModel.Rent = ((RentAd)model).Rent;
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(AdsEditViewModel viewModel)
        {
            AdDbContext ctx = new AdDbContext();
            var model = ctx.Ads.Find(viewModel.Id);
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
                model.Title = viewModel.Title;
                model.Age = viewModel.Age;
                model.Area = viewModel.Area;
                model.CategoryId = viewModel.CategoryId;

                if (viewModel.ThumbnailFile != null)
                {
                    var fileName = $"{Guid.NewGuid().ToString()}{extension}";
                    var fullPath = Path.Combine(Server.MapPath("~/images/thumbnails"), fileName);
                    viewModel.ThumbnailFile.SaveAs(fullPath);
                    var thumbnailClientPath = $"/images/thumbnails/{fileName}";

                    model.ThumbnailPhotoPath = thumbnailClientPath;
                }
                
                if (viewModel.MediaFile1 != null)
                {
                    AddMedia(viewModel.MediaFile1, model);
                }
                if (viewModel.MediaFile2 != null)
                {
                    AddMedia(viewModel.MediaFile2, model);
                }
                if (viewModel.MediaFile3 != null)
                {
                    AddMedia(viewModel.MediaFile3, model);
                }

                ctx.SaveChanges();

                TempData["Message"] = "ویرایش با موفقیت انجام شد";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(ctx.Categories.ToList(), "Id", "Title", viewModel.CategoryId);
            ViewBag.AdType = new SelectList(LookupHelper.GetAdTypesLookup(), "Value", "Text",
                    viewModel.AdType
                );
            TempData["Message"] = "ویرایش با خطا مواجه شد";
            TempData["MessageClass"] = "danger";
            return View(viewModel);


        }

        public ActionResult Delete(int id)
        {
            AdDbContext ctx = new AdDbContext();
            return View(ctx.Ads.Find(id));
        }
        [ActionName("Delete")]
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            AdDbContext ctx = new AdDbContext();
            var ad = ctx.Ads.Find(id);

            TempData["Message"] = $"{ad.Title} با موفقیت حذف شد";
            ctx.Ads.Remove(ad);
            ctx.SaveChanges();

            return RedirectToAction("Index");
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

    }
}