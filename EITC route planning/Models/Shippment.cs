using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace EITC_route_planning.Models
{
    public class Shippment
    {
        [Required]
        [Display(Name = "Weight")]
        public int Weight { get; set; }

        [Required]
        [Display(Name = "Type of package")]
        public string Category { get; set; }
        
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}