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
        public void searchRoute()
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

        public ActionResult Index()
        {
            var categories = DbHelper.GetAllCategoriesFromDb();
            var model = new Shippment();
            model.Categories = GetCategoryListItems(categories);

            return View(model);
        }

        private IEnumerable<SelectListItem> GetCategoryListItems(IEnumerable<Category> elements)
        {
            // Create an empty list to hold result of the operation
            var selectList = new List<SelectListItem>();

            // For each string in the 'elements' variable, create a new SelectListItem object
            // that has both its Value and Text properties set to a particular value.
            // This will result in MVC rendering each item as:
            //     <option value="State Name">State Name</option>
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    //Value = element,
                    Text = element.Name
                });
            }

            return selectList;
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