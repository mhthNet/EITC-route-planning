﻿using System;
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