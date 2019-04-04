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
    }
}
