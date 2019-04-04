using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace EITC_route_planning.Models
{
    public class CachedSection : Section
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public float Duration { get; set; }
        public float Weight { get; set; }
        public Category Category { get; set; }
        public string Provider { get; set; }

        public CachedSection() { }

        public CachedSection(decimal price, float duration, float weight, Category category, string provider)
        {
            Price = price;
            Duration = duration;
            Weight = weight;
            Category = category;
            Provider = provider;
        }

        public CachedSection(City cityFrom, City cityTo, decimal price, float duration, string provider)
        {
            Price = price;
            Duration = duration;
            Provider = provider;
            From = cityFrom;
            To = cityTo;
        }

        public CachedSection(int id, City from, City to, Decimal price, float duration, float weight, Category category, Provider provider)
        {
            Id = id;
            Price = price;
            Duration = duration;
            Weight = weight;
            Category = category;
            From = from;
            To = to;
            Provider = provider.Name;
        }
    }

    
}