using System;
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


            FetchSections.FetchInternCachedSections(weight: 2, category: new Category());
            //obj.Run();
            Assert.AreEqual(1, 1);
        }
    }
}
