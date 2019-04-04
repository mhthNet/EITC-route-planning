using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class CalculatedRoute
    {
        public Decimal Price { get; set; }
        public float Duration { get; set; }
        public List<CachedSection> Route { get; set; }
    }
}