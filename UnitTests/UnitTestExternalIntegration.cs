using System;
using System.Collections.Generic;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestExternal
    {
        [TestMethod]
        public void TestMethod1()
        {

            List<SectionRequest> testdata = new List<SectionRequest>
            {
                new SectionRequest(
                    new City(),
                    new City(), 
                    2,
                    new Category("Weapons", (float) 2.3),
                    ExternalIntegration.Oceanic
                )
            };

            var provider = new Provider("TestOurOwn", "http://wa-eitdk.azurewebsites.net/route/");
            var result = ExternalIntegration.LoadAllSectionsFromProvider(testdata, provider);
            Assert.AreEqual(1, 1);
        }

        [TestMethod]
        public void TestOceanic()
        {

            List<SectionRequest> testdata = new List<SectionRequest>
            {
                new SectionRequest(
                    new City("Cairo"),
                    new City("Cairo"),
                    2,
                    new Category("Weapons", (float) 2.3),
                    ExternalIntegration.Oceanic
                )
            };

            var result = ExternalIntegration.LoadAllSectionsFromProvider(testdata, ExternalIntegration.Oceanic);

            Assert.AreEqual(result[0].Price, 100);
            Assert.AreEqual(result[0].Duration, 8);
            Assert.AreEqual(result[0].From.Name, "Congo");
            Assert.AreEqual(result[0].To.Name, "Cairo");
        }

        [TestMethod]
        public void TestTelstar()
        {
            List<SectionRequest> testdata = new List<SectionRequest>
            {
                new SectionRequest(
                    new City("test"),
                    new City("test2"),
                    2,
                    new Category("Weapons", (float) 2.3),
                    ExternalIntegration.Telstar
                )
            };

            var result = ExternalIntegration.LoadAllSectionsFromProvider(testdata, ExternalIntegration.Telstar);


            Assert.AreEqual(result[0].Price, 10);
            Assert.AreEqual(result[0].Duration, 20);

        }
    }
}
