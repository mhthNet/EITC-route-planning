using System;
using EITC_route_planning.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new RouteCalculatorController();
            int result = obj.run();
            Assert.AreEqual(result, 1);
        }
    }
}
