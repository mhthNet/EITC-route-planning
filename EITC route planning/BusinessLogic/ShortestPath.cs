using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.RankedShortestPath;

namespace EITC_route_planning.BusinessLogic
{
    public class ShortestPath
    {
        public void Calculate()
        {
            // not implemented
        }
        public List<CalculatedRoute> calculateKRoutes(string origin, string destination, List<string> cities, List<CachedSection> cachedSections, bool fastest, int k)
        {
            var graph = CreateBidirectionalGraph(cities, cachedSections, fastest);


            //HoffmanPavleyRankedShortestPathAlgorithm<string, WeightedTaggedUndirectedEdge<string, string>> hoffmanAlgorithm =
            //    new HoffmanPavleyRankedShortestPathAlgorithm<string, WeightedTaggedUndirectedEdge<string, string>>(
            //        graph,
            //        x => x.Weight
            //    );

            //hoffmanAlgorithm.ShortestPathCount = k;
            //hoffmanAlgorithm.SetRootVertex(origin);
            //hoffmanAlgorithm.Compute(origin, destination);
            var res = graph.RankedShortestPathHoffmanPavley(x => x.Weight, origin, destination, k);
            
            //foreach (IEnumerable<TaggedUndirectedEdge<City, string>> path in hoffmanAlgorithm.ComputedShortestPaths)
            //{
            //    // Not implemented
            //}
            throw new NotImplementedException();
        }

        private static BidirectionalGraph<string, WeightedTaggedUndirectedEdge<string, string>> CreateBidirectionalGraph(List<string> cities, List<CachedSection> cachedSections, bool fastest)
        {
            BidirectionalGraph<string, WeightedTaggedUndirectedEdge<string, string>> graph =
                new BidirectionalGraph<string, WeightedTaggedUndirectedEdge<string, string>>(true);
            // Add nodes to map
            foreach (string city in cities)
            {
                graph.AddVertex(city);
            }
            // Add weighted edges to map
            foreach (CachedSection section in cachedSections)
            {
                var edge = new WeightedTaggedUndirectedEdge<string, string>(section.From.Name, section.To.Name, section.Provider, fastest ? section.Duration : (float)section.Price);
                graph.AddEdge(edge);
                var oppositeEdge = new WeightedTaggedUndirectedEdge<string, string>(section.To.Name, section.From.Name, section.Provider, fastest ? section.Duration : (float)section.Price);
                graph.AddEdge(oppositeEdge);
            }
            return graph;
        }
    }

    public class WeightedTaggedUndirectedEdge<TVertex, TTag> : TaggedUndirectedEdge<TVertex, TTag>
    {
        public double Weight { get; set; }

        public WeightedTaggedUndirectedEdge(TVertex source, TVertex target, TTag tag, double weight)
            : base(source, target, tag)
        {
            Weight = weight;
        }
    }
}