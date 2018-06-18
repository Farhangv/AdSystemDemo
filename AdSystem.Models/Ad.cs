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
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int? Area { get; set; }
        public int? Age { get; set; }
        public int? UnitCount { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        [MaxLength(150)]
        public string ThumbnailPhotoPath { get; set; }
        public virtual ICollection<Media> Media { get; set; }
        public virtual Category Category { get; set; }
        public int? CategoryId { get; set; }

        [NotMapped]
        public virtual string TotalPriceText { get { return "۰"; } }

    }
}