using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class WeightGroup
    {
        public float MaxWeight { get; set; }
        public Decimal Price { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}