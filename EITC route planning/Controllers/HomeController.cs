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
        public ActionResult RouteOverview()
        {
            var sections = DbHelper.GetAllSectionsFromDb();
            var model = new RouteOverview();
            model.Sections = sections;
            return View(model);
        }

        [HttpPost]
        public ActionResult SearchRoute(Shippment model)
        {
            var shippment = new Shippment();
            var Model = new Shippment();

            var categories = DbHelper.GetAllCategoriesFromDb();
            Model.Categories = GetCategoryListItems(categories);

            var cities = DbHelper.GetAllCities();
            Model.CitiesFrom = GetCityListItems(cities);
            Model.CitiesTo = GetCityListItems(cities);

            if (ModelState.IsValid)
            {
                shippment.Category = model.Category;
                shippment.Weight = model.Weight;
                shippment.CityFrom = model.CityFrom;
                shippment.CityTo = model.CityTo;
            }
            
            return View("index", Model);
        }

        public void verifyInputs()
        {

        }

        public ActionResult Index()
        {
            var categories = DbHelper.GetAllCategoriesFromDb();
            var model = new Shippment();
            model.Categories = GetCategoryListItems(categories);

            var cities = DbHelper.GetAllCities();
            model.CitiesFrom = GetCityListItems(cities);
            model.CitiesTo = GetCityListItems(cities);
            
            return View(model);
        }

        private IEnumerable<SelectListItem> GetCategoryListItems(IEnumerable<Category> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Text = element.Name
                });
            }

            return selectList;
        }

        private IEnumerable<SelectListItem> GetCityListItems(IEnumerable<City> elements)
        {
            var selectList = new List<SelectListItem>();
            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Text = element.Name
                });
            }

            return selectList;
        }


    }
}