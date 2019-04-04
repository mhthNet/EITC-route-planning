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
            DbRouteUpdater.Update();
            var res = DbCachedSectionLoader.Load(new Category("Weapons", 2));

            Assert.AreEqual(res, 1);
        }
    }
}
