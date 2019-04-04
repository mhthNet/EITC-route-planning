using System;
using System.Collections.Generic;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class TestLoadSegemtsToCached
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<CachedSection> cached = FetchSections.FetchInternCachedSections(2, new Category("new", 1));
            DbHelper.SaveCachedSections(cached);

            var readout = DbHelper.GetAllCachedSectionsFromDb();
            
            Assert.AreEqual(readout, cached);
        }
    }
}
