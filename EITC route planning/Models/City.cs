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

        public City() { }

        public City(string name, float xLocation, float yLocation)
        {
            Name = name;
            XLocation = xLocation;
            YLocation = yLocation;
        }
        public String Name { get; set; }
        public float XLocation { get; set; }
        public float YLocation { get; set; }
    }
}