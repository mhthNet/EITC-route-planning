using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using EITC_route_planning.Services;

namespace EITC_route_planning.Models
{
    public class City
    {
        public City(string name)
        {
            name = validate(name);
            Name = name;
        }

        private static string validate(string name)
        {
            name = name.ToUpper();
            if (DbHelper.GetAllCities().All(c => c.Name != name))
                throw new InvalidDataException("city name " + name + " does not exists");
            return name;
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