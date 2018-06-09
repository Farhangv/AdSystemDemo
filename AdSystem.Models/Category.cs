using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }
    }
}