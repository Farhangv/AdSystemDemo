using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdSystem.MVC.Library
{
    public class LookupHelper
    {
        public static List<AdTypeLookup> GetAdTypesLookup()
        {
            return new List<AdTypeLookup>()
            {
                new AdTypeLookup() { Value = "rent", Text = "اجاره" },
                new AdTypeLookup() { Value = "sale", Text = "فروش" }
            };
        }
    }

    public class AdTypeLookup
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
}