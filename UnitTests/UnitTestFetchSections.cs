using System;
using System.Collections.Generic;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestFetchSections
    {
        [TestMethod]
        public void TestPriceCalc()
        {
            Category category = DbHelper.GetCategoryByName("Weapons");

            List<CachedSection> cachedSections = FetchSections.FetchInternCachedSections( 2, category);

            Assert.AreEqual((Decimal)9.6, cachedSections[0].Price);
        }
    }
}
