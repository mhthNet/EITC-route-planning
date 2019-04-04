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

            List<CachedSection> cachedSections = FetchSections.FetchInternCachedSections(weight: 2, category: new Category("", 1));

            Assert.AreEqual(cachedSections, 1);
        }
    }
}
