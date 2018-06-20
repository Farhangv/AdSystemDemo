using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdSystem.MVC.ViewModels
{
    public class AdsEditViewModel
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [MaxLength(100, ErrorMessage = "عنوان باید کمتر از ۱۰۰ کاراکتر باشد")]
        [MinLength(5, ErrorMessage = "عنوان باید حداقل ۵ کاراکتر باشد")]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "مساحت")]
        [Required(ErrorMessage = "این فیلد اجباری است.")]
        public int? Area { get; set; }
        [Display(Name = "سن بنا")]
        [Required(ErrorMessage = "این فیلد اجباری است.")]
        public int? Age { get; set; }
        [Display(Name = "تعداد واحد در طبقه")]
        public int? UnitCount { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "این فیلد اجباری است.")]
        public int? CategoryId { get; set; }
        [Display(Name = "قیمت هر متر مربع")]
        public int? PricePerUnit { get; set; }
        [Display(Name = "تصویر بند انگشتی")]
        public HttpPostedFileBase ThumbnailFile { get; set; }
        [Display(Name = "فایل ۱ آگهی")]
        public HttpPostedFileBase MediaFile1 { get; set; }
        [Display(Name = "فایل ۲ آگهی")]
        public HttpPostedFileBase MediaFile2 { get; set; }
        [Display(Name = "فایل ۳ آگهی")]
        public HttpPostedFileBase MediaFile3 { get; set; }

        [Display(Name = "نوع آگهی")]
        public string AdType { get; set; }

        [Display(Name = "رهن")]
        public int? Diposite { get; set; }
        [Display(Name = "اجاره")]
        public int? Rent { get; set; }


        public string ThumbnailPhotoPath { get; set; }

    }
}