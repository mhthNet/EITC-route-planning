﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EITC_route_planning.Models
{
    public class Shippment
    {
        public int Weight { get; set; }
        public Category Category { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> CitiesFrom { get; set; }
        public IEnumerable<SelectListItem> CitiesTo { get; set; }

    }
}