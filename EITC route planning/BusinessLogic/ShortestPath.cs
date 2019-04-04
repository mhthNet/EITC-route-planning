using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.RankedShortestPath;
using QuickGraph.FST;
using QuickGraph.Serialization;

namespace EITC_route_planning.BusinessLogic
{
    public static class ShortestPath
    {
        public static void Calculate()
        {
            // not implemented
        }
        public static List<CalculatedRoute> calculateKRoutes(string origin, string destination, List<string> cities, List<CachedSection> cachedSections, bool fastest, int k)
        {
            var graphWeightTuple = CreateBidirectionalGraph(cities, cachedSections, fastest);

            var pathAlgorithm = new HoffmanPavleyRankedShortestPathAlgorithm<string, TaggedEdge<string>>(graphWeightTuple.Item1, e => graphWeightTuple.Item2[e]);
            pathAlgorithm.ShortestPathCount = k;
            pathAlgorithm.Compute(origin, destination);

            var result = new List<CalculatedRoute>();
            foreach (IEnumerable<TaggedEdge<string>> path in pathAlgorithm.ComputedShortestPaths)
            {
                var route = new CalculatedRoute();
                route.Route = new List<CachedSection>();
                decimal totalPrice = 0;
                float totalDuration = 0;
                foreach (TaggedEdge<string> section in path)
                {
                    var cachedSection = new CachedSection();
                    cachedSection.Price = section.Price;
                    totalPrice += section.Price;
                    cachedSection.Duration = section.Duration;
                    totalDuration += section.Duration;

                    cachedSection.Provider = section.Provider;
                    cachedSection.Category = section.Category;
                    cachedSection.Weight = section.Weight;
                    cachedSection.From = new City(section.Source);
                    cachedSection.To = new City(section.Target);

                    route.Route.Add(cachedSection);
                }
                route.Price = totalPrice;
                route.Duration = totalDuration;

                result.Add(route);
            }
            return result;
        }

        private static Tuple<BidirectionalGraph<string, TaggedEdge<string>>,Dictionary<TaggedEdge<string>, double>>
            CreateBidirectionalGraph(List<string> cities, List<CachedSection> cachedSections, bool fastest)
        {
            BidirectionalGraph<string, TaggedEdge<string>> graph =
                new BidirectionalGraph<string, TaggedEdge<string>>(true);
            // Add nodes to map
            foreach (string city in cities)
            {
                graph.AddVertex(city);
            }
            // Add edges to map and create weight dictionary
            var weights = new Dictionary<TaggedEdge<string>, double>();
            foreach (CachedSection section in cachedSections)
            {
                var edge = new TaggedEdge<string>(section, section.From.Name, section.To.Name);
                graph.AddVerticesAndEdge(edge);
                weights[edge] = fastest ? section.Duration : (double) section.Price;
                var oppositeEdge = new TaggedEdge<string>(section, section.To.Name, section.From.Name);
                graph.AddVerticesAndEdge(oppositeEdge);
                weights[oppositeEdge] = fastest ? section.Duration : (double)section.Price;
            }
            return new Tuple<
                    BidirectionalGraph<string, TaggedEdge<string>>, 
                    Dictionary<TaggedEdge<string>, double>
                >(graph, weights);
        }
    }

    public class TaggedEdge<TVertex> : Edge<TVertex>
    {
        public decimal Price { get; set; }
        public float Duration { get; set; }
        public string Provider { get; set; }
        public float Weight { get; set; }
        public Category Category { get; set; }

        public TaggedEdge(CachedSection section, TVertex source, TVertex target)
            : base(source, target)
        {
            Provider = section.Provider;
            Duration = section.Duration;
            Price = section.Price;
            Weight = section.Weight;
            Category = section.Category;
        }
    }
}