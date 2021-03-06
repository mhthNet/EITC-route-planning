﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class SectionRequest
    {
        public City From { get; set; }
        public City To { get; set; }
        public float Weight { get; set; }
        public Category Category { get; set; }

        public Provider Provider { get; set; }

        public SectionRequest(City from, City to, float weight, Category category, Provider provider)
        {
            From = from;
            To = to;
            Weight = weight;
            Category = category;
            Provider = provider;
        }
    }
    
}