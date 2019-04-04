using System;
using System.Collections.Generic;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;
using EITC_route_planning.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic
{
    [TestClass]
    public class ShortestPathCalcTests
    {
        [TestMethod]
        public void Calculate2ShortestPaths()
        {
            // Arrange
            // Create nodes
            var nodes = new List<City>();
            var cityA = new City("A");
            var cityB = new City("B");
            var cityC = new City("C");
            var cityD = new City("D");
            var cityE = new City("E");
            nodes.Add(cityA);
            nodes.Add(cityB);
            nodes.Add(cityC);
            nodes.Add(cityD);
            nodes.Add(cityE);

            // Create edges
            var edges = new List<CachedSection>();
            edges.Add(new CachedSection(cityA, cityB, 11, 11, "EIT"));
            edges.Add(new CachedSection(cityC, cityB, 33, 33, "EIT"));
            edges.Add(new CachedSection(cityE, cityC, 10, 10, "EIT"));
            edges.Add(new CachedSection(cityD, cityE, 5, 5, "EIT"));
            edges.Add(new CachedSection(cityD, cityB, 1, 101, "EIT"));
            edges.Add(new CachedSection(cityD, cityA, 102, 2, "EIT"));

            // Act
            var calc = new ShortestPath();
            calc.calculateKRoutes(cityD, cityB, nodes, edges, true, 3);

            // Assert
        }

        [TestMethod]
        public void Calculate2CheapestPaths()
        {
            // Arrange
            // Create nodes
            var nodes = new List<City>();
            var cityA = new City("A");
            var cityB = new City("B");
            var cityC = new City("C");
            var cityD = new City("D");
            var cityE = new City("E");
            nodes.Add(cityA);
            nodes.Add(cityB);
            nodes.Add(cityC);
            nodes.Add(cityD);
            nodes.Add(cityE);

            // Create edges
            var edges = new List<CachedSection>();
            edges.Add(new CachedSection(cityA, cityB, 11, 11, "EAT"));
            edges.Add(new CachedSection(cityB, cityC, 33, 33, "EAT"));
            edges.Add(new CachedSection(cityC, cityE, 10, 10, "EAT"));
            edges.Add(new CachedSection(cityE, cityD, 5, 5, "EAT"));
            edges.Add(new CachedSection(cityD, cityB, 1, 101, "EAT"));
            edges.Add(new CachedSection(cityA, cityD, 102, 2, "EAT"));

            // Act
            var calc = new ShortestPath();
            calc.calculateKRoutes(cityD, cityB, nodes, edges, true, 2);

            // Assert
        }

        [TestMethod]
        public void OriginDestinationNotConnected()
        {
            // Arrange
            var nodes = new List<City>();
            var cityA = new City("A");
            var cityB = new City("B");
            nodes.Add(cityA);
            nodes.Add(cityB);
            var edges = new List<CachedSection>();

            // Act
            var calc = new ShortestPath();
            calc.calculateKRoutes(cityA, cityB, nodes, edges, true, 2);

            // Assert

        }


    }
}
