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
            obj.Run();
            Assert.AreEqual(1, 1);
        }
    }
}
