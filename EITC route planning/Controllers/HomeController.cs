using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EITC_route_planning.Models;

namespace EITC_route_planning.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult homePageModel()
        {
            var categories = GetAllCategories();
            var model = new Shippment();
            model.Categories = GetSelectedCategories(categories);

            return View(model);
        }
        private IEnumerable<string> GetAllCategories()
        {
            return new List<string>
            {
                "Heavy",
                "Not Heavy",
                "Dangerous",
                "Non Dangerous"
            };
        }

        private IEnumerable<SelectListItem> GetSelectedCategories(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            // This will result in MVC rendering each item as:
            //     <option value="Category Name">Category Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
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