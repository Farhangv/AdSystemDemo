using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdSystem.MVC.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Area { get; set; }
        public int Age { get; set; }
        public int UnitCount { get; set; }
        //TODO: Add Facilities

        public string ThumbnailPhotoPath { get; set; }
        //TODO: Add Media 

        public virtual string TotalPriceText { get { return "۰"; } }
    }
}