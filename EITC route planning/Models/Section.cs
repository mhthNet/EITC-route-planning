using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class Section
    {
        public City From { get; set; }
        public City To { get; set; }
        public int Length { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}