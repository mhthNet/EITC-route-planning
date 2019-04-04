using System;
using System.Collections.Generic;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;
using EITC_route_planning.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace BusinessLogic
{
    [TestClass]
    public class ShortestPathTests
    {


        [TestMethod]
        public void CalculateShortestPath()
        {
            // Arrange
            // Create nodes
            var nodes = new List<string>();
            var cityA = new City("A");
            var cityB = new City("B");
            var cityC = new City("C");
            var cityD = new City("D");
            var cityE = new City("E");
            nodes.Add(cityA.Name);
            nodes.Add(cityB.Name);
            nodes.Add(cityC.Name);
            nodes.Add(cityD.Name);
            nodes.Add(cityE.Name);

            // Create edges
            var edges = new List<CachedSection>();
            edges.Add(new CachedSection(cityA, cityB, 11, 11, "EIT"));
            edges.Add(new CachedSection(cityC, cityB, 33, 33, "EIT"));
            edges.Add(new CachedSection(cityD, cityE, 5, 5, "EIT"));
            edges.Add(new CachedSection(cityD, cityB, 1, 101, "EIT"));
            edges.Add(new CachedSection(cityE, cityC, 10, 10, "EIT"));
            edges.Add(new CachedSection(cityD, cityA, 102, 2, "EIT"));

            // Act
            var res = ShortestPath.calculateKRoutes(cityD.Name, cityB.Name, nodes, edges, true, 1)[0];
            
            // 
            Assert.AreEqual(13, res.Duration);
            Assert.AreEqual(113, res.Price);
            Assert.AreEqual("A",res.Route[0].To.Name);
        }

        [TestMethod]
        public void CalculateCheapestPath()
        {
            // Arrange
            // Create nodes
            var nodes = new List<string>();
            var cityA = new City("A");
            var cityB = new City("B");
            var cityC = new City("C");
            var cityD = new City("D");
            var cityE = new City("E");
            nodes.Add(cityA.Name);
            nodes.Add(cityB.Name);
            nodes.Add(cityC.Name);
            nodes.Add(cityD.Name);
            nodes.Add(cityE.Name);

            // Create edges
            var edges = new List<CachedSection>();
            edges.Add(new CachedSection(cityA, cityB, 11, 11, "EAT"));
            edges.Add(new CachedSection(cityB, cityC, 33, 33, "EAT"));
            edges.Add(new CachedSection(cityC, cityE, 10, 10, "EAT"));
            edges.Add(new CachedSection(cityE, cityD, 5, 5, "EAT"));
            edges.Add(new CachedSection(cityD, cityB, 1, 101, "EAT"));
            edges.Add(new CachedSection(cityA, cityD, 102, 2, "EAT"));

            // Act
            var res = ShortestPath.calculateKRoutes(cityD.Name, cityB.Name, nodes, edges, false, 1)[0];

            // Assert
            Assert.AreEqual(101, res.Duration);
            Assert.AreEqual(1, res.Price);
            Assert.AreEqual("B", res.Route[0].To.Name);
        }

        [TestMethod]
        public void Calculate3CheapestPaths()
        {
            // Arrange
            // Create nodes
            var nodes = new List<string>();
            var cityA = new City("A");
            var cityB = new City("B");
            var cityC = new City("C");
            nodes.Add(cityA.Name);
            nodes.Add(cityB.Name);
            nodes.Add(cityC.Name);

            // Create edges
            var edges = new List<CachedSection>();
            edges.Add(new CachedSection(cityA, cityB, 4, 0, "EAT"));
            edges.Add(new CachedSection(cityA, cityB, 7, 0, "EAT"));
            edges.Add(new CachedSection(cityB, cityC, 10, 0, "EAT"));
            edges.Add(new CachedSection(cityB, cityC, 15, 0, "EAT"));
            edges.Add(new CachedSection(cityA, cityC, 15, 0, "EAT"));

            // Act
            var res = ShortestPath.calculateKRoutes(cityA.Name, cityC.Name, nodes, edges, false, 3);

            // Assert
            Assert.AreEqual(14, res[0].Price);
            Assert.AreEqual("B", res[0].Route[0].To.Name);
            Assert.AreEqual(15, res[1].Price);
            Assert.AreEqual("C", res[1].Route[0].To.Name);
            Assert.AreEqual(17, res[2].Price);
            Assert.AreEqual("B", res[2].Route[0].To.Name);
            Assert.AreEqual(3,res.Count);
        }

        [TestMethod]
        public void OriginDestinationNotConnected()
        {
            // Arrange
            var nodes = new List<string>();
            var cityA = new City("A");
            var cityB = new City("B");
            nodes.Add(cityA.Name);
            nodes.Add(cityB.Name);
            var edges = new List<CachedSection>();

            // Act
            var res = ShortestPath.calculateKRoutes(cityA.Name, cityB.Name, nodes, edges, true, 1);

            // Assert
            Assert.AreEqual(0, res.Count);

        }


    }
}
