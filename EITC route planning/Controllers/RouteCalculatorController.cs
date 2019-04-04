using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;

namespace EITC_route_planning.Controllers
{
    public class RouteCalculatorController : Controller
    {
        // GET: RouteCalculator
        public void Run()
        {
            var result = RouteCalculator.Calcuate(new Category(), 1, new City(), new City());
        }
    }
}