using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EITC_route_planning.Models;
using EITC_route_planning.Services;

namespace EITC_route_planning.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult homePageModel()
        {
            var categories = DbHelper.GetAllCategoriesFromDb();
            var model = new Shippment();
            model.categories = categories;

            return View(model);
        }

        public ActionResult RouteOverview()
        {
            return View();
        }

        public void createShippment()
        {
            if (HttpContext.Request.RequestType == "POST")
            {
                var weight = Request.Form["id"];
                var packageType = Request.Form["type"];
                var fromCity = Request.Form["from"];
                var toCity = Request.Form["to"];
            }

        }

        public void verifyInputs()
        {

        }

        public void searchRoute()
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}