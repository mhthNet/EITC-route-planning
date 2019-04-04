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
            DbHelper.ClearAllCachedSectionsFromDb();
            List<Section> ownSections = DbHelper.GetAllSectionsFromDb();
            List<CachedSection> cached = FetchSections.FetchInternCachedSections(100, new Category("Weapons", (float) 1.2));
            DbHelper.SaveCachedSections(cached);
            var readout = DbHelper.GetAllCachedSectionsFromDb();
            
            
            Assert.AreEqual(readout[0].Category.Name, cached[0].Category.Name);
            Assert.AreEqual(readout[0].Category.PriceFactor, cached[0].Category.PriceFactor);
            Assert.AreEqual(readout[0].Duration, cached[0].Duration);
            Assert.AreEqual(readout[0].Id, cached[0].Id);
            Assert.AreEqual(readout[0].Price, cached[0].Price);
            Assert.AreEqual(readout[0].Provider, cached[0].Provider);
            Assert.AreEqual(readout[0].Weight, cached[0].Weight);
            Assert.AreEqual(readout[0].From.Name, cached[0].From.Name);
            Assert.AreEqual(readout[0].From.XLocation, cached[0].From.XLocation);
            Assert.AreEqual(readout[0].From.YLocation, cached[0].From.YLocation);
            Assert.AreEqual(readout[0].To.Name, cached[0].To.Name);
            Assert.AreEqual(readout[0].To.XLocation, cached[0].To.XLocation);
            Assert.AreEqual(readout[0].To.YLocation, cached[0].To.YLocation);

            Assert.AreEqual(readout[1].Category.Name, cached[1].Category.Name);
            Assert.AreEqual(readout[1].Category.PriceFactor, cached[1].Category.PriceFactor);
            Assert.AreEqual(readout[1].Duration, cached[1].Duration);
            Assert.AreEqual(readout[1].Id, cached[1].Id);
            Assert.AreEqual(readout[1].Price, cached[1].Price);
            Assert.AreEqual(readout[1].Provider, cached[1].Provider);
            Assert.AreEqual(readout[1].Weight, cached[1].Weight);
            Assert.AreEqual(readout[1].From.Name, cached[1].From.Name);
            Assert.AreEqual(readout[1].From.XLocation, cached[1].From.XLocation);
            Assert.AreEqual(readout[1].From.YLocation, cached[1].From.YLocation);
            Assert.AreEqual(readout[1].To.Name, cached[1].To.Name);
            Assert.AreEqual(readout[1].To.XLocation, cached[1].To.XLocation);
            Assert.AreEqual(readout[1].To.YLocation, cached[1].To.YLocation);
        }
    }
}
