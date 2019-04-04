using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class City
    {
        public City(string name)
        {
            Name = name;
        }
        public String Name { get; set; }
        public Point Location { get; set; }
    }
}