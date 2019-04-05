using System;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDbRouteUpdater
    {
        [TestMethod]
        public void TestUpdate()
        {
            DbRouteUpdater.UpdateExternal();
            var res = DbCachedSectionLoader.Load(new Category("Weapons", 2));

            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        public void TestUpdateInternal()
        {
            DbHelper.ClearAllCachedSectionsFromDb();
            DbRouteUpdater.UpdateInternal();
            var res = DbCachedSectionLoader.Load(new Category("Weapons", 2));

            Assert.AreEqual(res[0].Category.Name, "Weapons");
            Assert.AreEqual(res[0].Provider, ExternalIntegration.EastIndia.Name);
            Assert.AreEqual(res[0].Price, new Decimal(9.6));
        }
    }
}
