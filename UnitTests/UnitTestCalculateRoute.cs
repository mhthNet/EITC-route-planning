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
            Category category = DbHelper.GetCategoryByName("Weapons");

            City from = new City("SLAVEKYSTEN");
            City to = new City("SIERRA LEONE");
            CalculatedRoute route =RouteCalculator.CalculateInternalRoute(2, category, true, from.Name, to.Name);
            Assert.AreEqual(new Decimal(48), route.Price);
            Assert.AreEqual(96, route.Duration);
        }
    }
}
