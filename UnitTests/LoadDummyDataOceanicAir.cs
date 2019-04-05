using System;
using System.Collections.Generic;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class LoadDummyDataOceanicAir
    {
        [TestMethod]
        public void LoadOceanic()
        {
            List<CachedSection> cach = new List<CachedSection>()
            {
                MockCachedSection1(),
                MockCachedSection2()
            };
            DbHelper.SaveCachedSections(cach);

        }


        public City MockCity2()
        {
            return new City("SLAVEKYSTEN");
        }

        public CachedSection MockCachedSection1()
        {
            return new CachedSection(
                new City("SLAVEKYSTEN"),
                new City("CAIRO"),
                30,
                4,
                1,
                MockCategory(),
                ExternalIntegration.Oceanic.Name);
        }

        public CachedSection MockCachedSection2()
        {
            return new CachedSection(
                new City("GULDKYSTEN"),
                new City("TANGER"),
                30,
                4,
                1,
                MockCategory(),
                ExternalIntegration.Oceanic.Name);
        }

        public Category MockCategory()
        {
            return new Category("Normal", 1);
        }
    }
}
