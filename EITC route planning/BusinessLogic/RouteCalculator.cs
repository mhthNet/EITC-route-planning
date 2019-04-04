using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using EITC_route_planning.Models;
using EITC_route_planning.Services;
using QuickGraph;

namespace EITC_route_planning.BusinessLogic
{
    public class RouteCalculator
    {
        private static int kEstimates = 5;
        public static CalculatedRoute Calculate(Category category, float weight, City from, City to, Boolean fastest=true)
        {

            var cityNames = DbHelper.GetAllCities().Select(x => x.Name).ToList();

            List<CachedSection> cachedSections = DbCachedSectionLoader.Load(category);

            if (cachedSections.Count == 0)
            {
                DbRouteUpdater.Update();
                cachedSections = DbCachedSectionLoader.Load(category);
            }

            List<CalculatedRoute> approximatedcalculatedRoutes =
                ShortestPath.calculateKRoutes(from.Name, to.Name, cityNames, cachedSections, fastest, kEstimates);
            List<SectionRequest> sections = CalculatedRouteToSectionRequests(approximatedcalculatedRoutes);

            List<CachedSection> upToDateSections = FetchSections.FetchInternCachedSections(weight, category);
            upToDateSections.AddRange(FetchSections.FetchExternCachedSections(sections));

            List<CalculatedRoute> exactCalculatedRoutes = ShortestPath.calculateKRoutes(from.Name, to.Name, cityNames, upToDateSections, fastest, 1);
            if (exactCalculatedRoutes.Count == 0)
            {
                throw new NoPathFoundException();
            }
            return exactCalculatedRoutes[0];
        }

        public static CalculatedRoute CalculateInternalRoute(float weight, Category category, bool fastest, string origin, string destination)
        {
            var cachedSections = FetchSections.FetchInternCachedSections(weight, category);
            var cities = DbHelper.GetAllCities().Select(x => x.Name).ToList();
            var routes = ShortestPath.calculateKRoutes(origin, destination, cities, cachedSections, fastest, 1);
            // Calculate total price and just do start/finish in response. Convert it to Json

            if (routes.Count == 0)
            {
                throw new NoPathFoundException();
            }
            
            return routes[0];
        }

        private static List<SectionRequest> CalculatedRouteToSectionRequests(List<CalculatedRoute> calculatedRoutes)
        {
            List<SectionRequest> converted = new List<SectionRequest>();
            foreach (CalculatedRoute calculatedRoute in calculatedRoutes)
            {
                foreach (Provider provider in ExternalIntegration.Providers)
                {
                    converted.AddRange(
                        calculatedRoute.Route.Select(x => new SectionRequest(
                            x.From,
                            x.To,
                            x.Weight,
                            x.Category,
                            ExternalIntegration.Providers.Find(y => y.Name == x.Provider)
                        ))
                    );
                }
            }
            return converted;
        }
    }
}