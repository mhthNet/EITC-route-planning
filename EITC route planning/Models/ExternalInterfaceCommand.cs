using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class ExternalInterfaceCommand
    {
        public decimal Price { get; set; }
        public float Duration { get; set; }

        public Boolean valid { get; set; }

        public String fromName { get; set; }
        public String toName { get; set; }

    }
}