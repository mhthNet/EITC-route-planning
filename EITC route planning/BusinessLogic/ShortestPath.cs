using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;
using QuickGraph;
using QuickGraph.Algorithms.RankedShortestPath;

namespace EITC_route_planning.BusinessLogic
{
    public class ShortestPath
    {

        public List<CalculatedRoute> calculateKRoutes(City origin, City destination, List<City> cities, List<CachedSection> cachedSections, bool fastest, int k)
        {
            var graph = CreateBidirectionalGraph(cities, cachedSections, fastest);


            HoffmanPavleyRankedShortestPathAlgorithm<City, WeightedTaggedUndirectedEdge<City, string>> hoffmanAlgorithm =
                new HoffmanPavleyRankedShortestPathAlgorithm<City, WeightedTaggedUndirectedEdge<City, string>>(
                    graph,
                    x => x.Weight
                );

            hoffmanAlgorithm.ShortestPathCount = k;
            hoffmanAlgorithm.SetRootVertex(origin);
            hoffmanAlgorithm.Compute(origin, destination);

            foreach (IEnumerable<TaggedUndirectedEdge<City, string>> path in hoffmanAlgorithm.ComputedShortestPaths)
            {
                // Not implemented
            }
            throw new NotImplementedException();
        }

        private static BidirectionalGraph<City, WeightedTaggedUndirectedEdge<City, string>> CreateBidirectionalGraph(List<City> cities, List<CachedSection> cachedSections, bool fastest)
        {
            BidirectionalGraph<City, WeightedTaggedUndirectedEdge<City, string>> graph =
                new BidirectionalGraph<City, WeightedTaggedUndirectedEdge<City, string>>(true);
            // Add nodes to map
            foreach (City city in cities)
            {
                graph.AddVertex(city);
            }
            // Add weighted edges to map
            foreach (CachedSection section in cachedSections)
            {
                var edge = new WeightedTaggedUndirectedEdge<City, string>(section.From, section.To, section.Provider, fastest ? section.Duration : (float)section.Price);
                graph.AddEdge(edge);
                //var oppositeEdge = new WeightedTaggedUndirectedEdge<City, string>(section.To, section.From, section.Provider, fastest ? section.Duration : (float)section.Price);
                //graph.AddEdge(oppositeEdge);
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