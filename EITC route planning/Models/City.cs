﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class City
    {
        public City(){ }

        public City(string name, float xlocation, float ylocation)
        {
            Name = name;
            XLocation = xlocation;
            YLocation = ylocation;
        }
        public String Name { get; set; }
        public float XLocation { get; set; }
        public float YLocation { get; set; }

    }
}