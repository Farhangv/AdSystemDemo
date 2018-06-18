using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdSystem.Models
{
    public class Ad
    {
        [Display(Name = "شناسه")]
        public int Id { get; set; }
        [MaxLength(100)]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "مساحت")]
        public int? Area { get; set; }
        [Display(Name = "سن بنا")]
        public int? Age { get; set; }
        [Display(Name = "تعداد واحد در طبقه")]
        public int? UnitCount { get; set; }
        [Display(Name = "امکانات")]
        public virtual ICollection<Feature> Features { get; set; }
        [MaxLength(150)]
        [Display(Name = "عکس")]
        public string ThumbnailPhotoPath { get; set; }
        [Display(Name = "رسانه ها")]
        public virtual ICollection<Media> Media { get; set; }
        [Display(Name = "دسته بندی")]
        public virtual Category Category { get; set; }
        [Display(Name = "دسته بندی")]
        public int? CategoryId { get; set; }

        [NotMapped]
        [Display(Name = "قیمت کل")]
        public virtual string TotalPriceText { get { return "۰"; } }

    }
}