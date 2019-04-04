using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EITC_route_planning.Models;

namespace EITC_route_planning.Models
{
public class RouteOverview
{
    [Required]
    [Display(Name = "Sections")]
    public List<Section> sections { get; set; }

}
}