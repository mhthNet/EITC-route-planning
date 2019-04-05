using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using EITC_route_planning.Controllers;
using EITC_route_planning.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestCreateCity
    {
        [TestMethod]
        public void TestMethod1()
        {
            var obj = new City("hvalbugten");
            Assert.AreEqual(obj.Name, "HVALBUGTEN");
        }

        [TestMethod]
        public void TestMethod2()
        {
            var obj = new City("HVALBUGTEN");
            Assert.AreEqual(obj.Name, "HVALBUGTEN");
        }
    }
}
