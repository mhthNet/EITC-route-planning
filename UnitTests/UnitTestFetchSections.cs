using System;
using System.Collections.Generic;
using System.Linq;
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
            Category category = DbHelper.GetCategoryByName("Normal");

            List<CachedSection> cachedSections = FetchSections.FetchInternCachedSections( 2, category);

            Assert.AreEqual(new Decimal(45), cachedSections[0].Price);

            DbHelper.SaveCachedSections(cachedSections);
        }


        [TestMethod]
        public void TestCalculatePrice()
        {
            Category category = DbHelper.GetCategoryByName("Normal");

            List<float> weights = new List<float>()
            {
                0, 1, 4, 5, 6, 8, 11, 23, 50, 51, 99, 100
            };

            List<Decimal> expected = new List<Decimal>()
            {
                5, 5, 5, 5, 5, 5, 6, 6,   6,  8,   8,   8
            };
            var section = new Section(new City("CAIRO"), new City("SLAVEKYSTEN"), 2, new TransportationType("SHIP", 12, 200));

 
            List<Decimal> prices = weights.Select(weight => 
                FetchSections.CalculatePrice(
                    section, weight, category)
                ).ToList();

            for (int i=0; weights.Count < i; i++)

                Assert.AreEqual(expected[i], prices[i]);
        }

        [TestMethod]
        public void TestPriceCalcOutOfBoundWeightGroup()
        {
            Category category = DbHelper.GetCategoryByName("Weapons");

            float weight = 101;

            List<CachedSection> cachedSections = FetchSections.FetchInternCachedSections(weight, category);

            Assert.AreEqual(0, cachedSections.Count);
        }
    }
}
