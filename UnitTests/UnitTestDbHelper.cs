using System;
using System.Collections.Generic;
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

            Assert.AreEqual(sections, 1);
        }
    }
}
