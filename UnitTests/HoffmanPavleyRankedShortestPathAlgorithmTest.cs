﻿

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using QuickGraph.Serialization;

using QuickGraph.Algorithms.RankedShortestPath;

using System.IO;

using QuickGraph.Algorithms;

using QuickGraph.Collections;



namespace QuickGraph.Tests.Algorithms.RankedShortestPath

{

    [TestClass]

    public class HoffmanPavleyRankedShortestPathAlgorithmTest

    {
        [TestMethod]

        public void HoffmanPavleyRankedShortestPathNetwork()

        {

            // create network graph

            var g = new BidirectionalGraph<int, Edge<int>>();

            var weights = new Dictionary<Edge<int>, double>();

            var data = new int[] {

                1,4,3, //
                4,1,3,

                1,2,1,
                2,1,1,

                2,3,3,
                3,2,3,

                4,5,1,
                5,4,1,

                1,5,2,
                5,1,2,

                2,5,2,
                5,2,3,

                2,6,5,
                6,2,5,

                2,8,2,
                8,2,2,

                6,9,2,
                9,6,2,

                6,8,4,
                8,6,4,

                5,8,2,
                8,5,2,

                5,7,2,
                7,5,2,

                4,7,3,
                7,4,3,

                7,8,4,
                8,7,4,

                9,8,5

            };

            int i = 0;

            for (; i + 2 < data.Length; i += 3)

            {

                Edge<int> edge = new Edge<int>(data[i + 0], data[i + 1]);

                g.AddVerticesAndEdge(edge);

                weights[edge] = data[i + 2];

            }

            Assert.AreEqual(data.Length, i);



            this.HoffmanPavleyRankedShortestPath(g, weights, 9, 1, 10);

        }



        public IEnumerable<IEnumerable<TEdge>> HoffmanPavleyRankedShortestPath<TVertex, TEdge>(IBidirectionalGraph<TVertex, TEdge> g, 
            Dictionary<TEdge, double> edgeWeights,
            TVertex rootVertex,
            TVertex goalVertex,
            int pathCount)
            where TEdge : IEdge<TVertex>

        {

            //GraphConsoleSerializer.DisplayGraph((IEdgeListGraph<TVertex, TEdge>)g);



            var target = new HoffmanPavleyRankedShortestPathAlgorithm<TVertex, TEdge>(g, e => edgeWeights[e]);

            target.ShortestPathCount = pathCount;

            target.Compute(rootVertex, goalVertex);



            double lastWeight = double.MinValue;

            foreach (var path in target.ComputedShortestPaths)

            {

                Console.WriteLine("path: {0}", Enumerable.Sum(path, e => edgeWeights[e]));

                double weight = Enumerable.Sum(path, e => edgeWeights[e]);

                Assert.IsTrue(lastWeight <= weight, "{0} <= {1}", lastWeight, weight);

                Assert.AreEqual(rootVertex, Enumerable.First(path).Source);

                Assert.AreEqual(goalVertex, Enumerable.Last(path).Target);

                Assert.IsTrue(EdgeExtensions.IsPathWithoutCycles<TVertex, TEdge>(path));



                lastWeight = weight;

            }



            return target.ComputedShortestPaths;

        }



        [TestMethod]

        [WorkItem(12288)]

        [Ignore]

        [Description("binary data outdated")]

        public void Repro12288()

        {

            AdjacencyGraph<int, Edge<int>> g;

            using (var stream = this.GetType().Assembly.GetManifestResourceStream(

                "QuickGraph.Tests.Algorithms.RankedShortestPath.AdjacencyGraph.bin"))

                g = stream.DeserializeFromBinary<int, Edge<int>, AdjacencyGraph<int, Edge<int>>>();



            var g1 = g.ToBidirectionalGraph();



            int Source = 1;

            int Target = 2;



            int pathCount = 5;

            foreach (IEnumerable<Edge<int>> path in g1.RankedShortestPathHoffmanPavley(

                e => 5, Source, Target, pathCount))

            { }

        }

    }

}