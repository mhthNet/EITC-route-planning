using System;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestCalculateRoute
    {
        [TestMethod]
        public void Calculate()
        {
            DbHelper.ClearAllCachedSectionsFromDb();
            Category category = DbHelper.GetCategoryByName("Normal");

            City from = new City("SLAVEKYSTEN");
            City to = new City("SIERRA LEONE");
            CalculatedRoute route =RouteCalculator.CalculateInternalRoute(category, 2, from.Name, to.Name,true);
            Assert.AreEqual(new Decimal(48), route.Price);
            Assert.AreEqual(40, route.Duration);
        }


        [TestMethod]
        public void CalculateExtern()
        {
            DbHelper.ClearAllCachedSectionsFromDb();
            Category category = DbHelper.GetCategoryByName("Normal");

            City from = new City("CAIRO");
            City to = new City("ST HELENA");
            CalculatedRoute route = RouteCalculator.Calculate(category, 3, from, to, true);
            Assert.AreEqual(new Decimal(130), route.Price);
            Assert.AreEqual(96, route.Duration);
        }
    }
}
