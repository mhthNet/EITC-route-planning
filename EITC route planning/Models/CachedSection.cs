using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class CachedSection : CalculatedRoute
    {
        public decimal Price { get; set; }
        public float Duration { get; set; }
        public float Weight { get; set; }
        public Category Category { get; set; }
    }
}