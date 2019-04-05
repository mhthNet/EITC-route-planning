using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EITC_route_planning.BusinessLogic;
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
            for (int i = 0; i < TempData.Keys.Count - 2; i++)
            {
                if (TempData.ContainsKey("price" + i))
                    ViewData["price" + i] = TempData["price" + i];
                if (TempData.ContainsKey("duration" + i))
                    ViewData["duration" + i] = TempData["duration" + i];
                if (TempData.ContainsKey("from" + i))
                    ViewData["from" + i] = TempData["from" + i];
                if (TempData.ContainsKey("To" + i))
                    ViewData["To" + i] = TempData["To" + i];
            }
            if (TempData.ContainsKey("price"))
                ViewData["price"] = TempData["price"];
            if (TempData.ContainsKey("duration"))
                ViewData["duration"] = TempData["duration"];
            return View();
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

                CalculatedRoute calculatedRoute = null;
                CalculatedRoute calculatedRouteCheap = null;
                try
                {
                    calculatedRoute = RouteCalculator.Calculate(model.Category, model.Weight,
                        DbHelper.GetCityByName(model.CityFrom), DbHelper.GetCityByName(model.CityTo), true);

                    calculatedRouteCheap = RouteCalculator.Calculate(model.Category, model.Weight,
                        DbHelper.GetCityByName(model.CityFrom), DbHelper.GetCityByName(model.CityTo), false);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                shippment.searchedSections = calculatedRoute;
                shippment.searchedSectionsCheap = calculatedRouteCheap;

                shippment.Categories = GetCategoryListItems(categories);
                shippment.CitiesFrom = GetCityListItems(cities);
                shippment.CitiesTo = GetCityListItems(cities);
                for (int i = 0; i < shippment.searchedSections.Route.Count; i++)
                {
                    TempData["price"+i] = shippment.searchedSections.Route[i].Price.ToString();
                    TempData["duration"+i] = shippment.searchedSections.Route[i].Duration.ToString();
                    TempData["from"+i] = shippment.searchedSections.Route[i].From.Name;
                    TempData["to"+i] = shippment.searchedSections.Route[i].To.Name;
                }
                TempData["price"] = shippment.searchedSections.Price;
                TempData["duration"] = shippment.searchedSections.Duration;

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