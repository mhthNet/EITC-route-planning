using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EITC_route_planning.Models
{
    public class CachedSection : Section
    {
        public Decimal Price { get; set; }
        public float Duration { get; set; }
        public float Weight { get; set; }
        public Category Category { get; set; }

        public CachedSection() { }

        public CachedSection(City from, City to, Decimal price, float duration, float weight, Category category)
        {
            Price = price;
            Duration = duration;
            Weight = weight;
            Category = category;
            From = from;
            To = to;
        }
    }

    
}