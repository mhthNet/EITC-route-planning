﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class TransportationType

   
    {
        public TransportationType(string type, float speed, float weightLimit )
        {
            Type = type;
            Speed = speed;
            WeightLimit = weightLimit;
        }
        public string Type { get; set; }

        public float Speed { get; set; }

        public float WeightLimit { get; set; }
    }
}