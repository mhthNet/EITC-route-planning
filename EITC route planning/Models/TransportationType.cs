using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class TransportationType
    {
        public string Name { get; set; }
        public string Type { get; set; }

        public int Speed { get; set; }

        public float WeightLimit { get; set; }
    }
}