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
            ExternalIntegration.LoadAllSectionsFromAllExternals(new List<SectionRequest>(),
                new EternalIntegrationConnections("", ""));
            Assert.AreEqual(1, 1);
        }
    }
}
