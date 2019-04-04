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

                List<List<Section>> searchedSections = new List<List<Section>>();
                List<Section> fastSections = new List<Section>();
                //TODO: use real data, not dummy data
                fastSections.Add(new Section(new City("city1", 1.0f, 2.0f), new City("city2", 3.0f, 4.0f), 5, new TransportationType("SHIP", 12, 100)));
                fastSections.Add(new Section(new City("city3", 1.0f, 2.0f), new City("city4", 3.0f, 4.0f), 5, new TransportationType("SHIP", 12, 100)));
                searchedSections.Add(fastSections);

                shippment.searchedSections = searchedSections;

                shippment.Categories = GetCategoryListItems(categories);
                shippment.CitiesFrom = GetCityListItems(cities);
                shippment.CitiesTo = GetCityListItems(cities);

                return View("index", shippment);
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