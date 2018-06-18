using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdSystem.Models
{
    public class SaleAd:Ad
    {
        [Display(Name = "قیمت هر متر مربع")]
        public int? PricePerUnit { get; set; }

    }
}
