using System;
using System.Collections.Generic;
using System.Web.Mvc.Routing.Constraints;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using FSharpx.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTestDbHelper
    {
        [TestMethod]
        public void TestGetSections()
        {
            List<Section> sections = DbHelper.GetAllSectionsFromDb();

            List<Section> expected = new List<Section>()
            {
                new Section(
                    new City("ST HELENA"),
                    new City("KAPSTADEN"),
                    9,
                    new TransportationType("SHIP", 9, 900)
                    ),
                new Section(new City("KAPSTADEN"),
                    new City("HVALBUGTEN"),
                    3,
                    new TransportationType("SHIP", 9, 900))

            };

            Assert.AreEqual(sections[0].From.Name, expected[0].From.Name);
            Assert.AreEqual(sections[0].To.Name, expected[0].To.Name);
            Assert.AreEqual(sections[0].Length, expected[0].Length);
            Assert.AreEqual(sections[0].TransportationType.Type, expected[0].TransportationType.Type);
            Assert.AreEqual(sections[1].From.Name, expected[1].From.Name);
            Assert.AreEqual(sections[1].To.Name, expected[1].To.Name);
            Assert.AreEqual(sections[1].Length, expected[1].Length);
            Assert.AreEqual(sections[1].TransportationType.Type, expected[1].TransportationType.Type);
        }
    }
}
