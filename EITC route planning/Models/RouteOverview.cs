using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages.Html;

namespace EITC_route_planning.Models
{
public class RouteOverview
{
    [Required]
    [Display(Name = "Sections")]
    public List<Section> Sections { get; set; }

}
}