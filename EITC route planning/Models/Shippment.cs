using System;
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
        public List<City> CitiesFrom { get; set; }
        public List<City> CitiesTo { get; set; }

    }
}