using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
            from.Name = from.Name.ToUpper();
            to.Name = to.Name.ToUpper();
            var cityNames = DbHelper.GetAllCities().Select(x => x.Name).ToList();

            List<CachedSection> cachedSections = DbCachedSectionLoader.Load(category);
            
            if (cachedSections.Count == 0)
            {
                DbRouteUpdater.UpdateExternal();
                cachedSections = DbCachedSectionLoader.Load(category);
            }
            cachedSections = removeCityRuleViolations(cachedSections, category);

            List<CalculatedRoute> approximatedcalculatedRoutes =
                ShortestPath.calculateKRoutes(from.Name, to.Name, cityNames, cachedSections, fastest, kEstimates);
            if (approximatedcalculatedRoutes.Count == 0)
            {
                throw new NoPathFoundException();
            }
            List<SectionRequest> sections = CalculatedRouteToSectionRequests(approximatedcalculatedRoutes, weight);

            List<CachedSection> upToDateSections = FetchSections.FetchInternCachedSections(weight, category);
            upToDateSections.AddRange(FetchSections.FetchExternCachedSections(sections));

            List<CalculatedRoute> exactCalculatedRoutes = ShortestPath.calculateKRoutes(from.Name, to.Name, cityNames, upToDateSections, fastest, 1);
            if (exactCalculatedRoutes.Count == 0)
            {
                List<SectionRequest> sectionRequests = ExternalCachedSectionToSectionRequests(cachedSections, weight);
                List<CachedSection> allUpToDateSections = FetchSections.FetchInternCachedSections(weight, category);
                allUpToDateSections.AddRange(FetchSections.FetchExternCachedSections(sectionRequests));
                allUpToDateSections = removeCityRuleViolations(allUpToDateSections, category);
                var slowExactCalculatedRoutes =
                    ShortestPath.calculateKRoutes(from.Name, to.Name, cityNames, allUpToDateSections, fastest, 1);
                if (slowExactCalculatedRoutes.Count == 0)
                {
                    throw new NoPathFoundException();
                }
                return slowExactCalculatedRoutes[0];
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

        private static List<SectionRequest> CalculatedRouteToSectionRequests(List<CalculatedRoute> calculatedRoutes, float weight)
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
                            weight,
                            x.Category,
                            ExternalIntegration.Providers.Find(y => y.Name == x.Provider)
                        ))
                    );
                }
            }
            return converted;
        }

        private static List<SectionRequest> ExternalCachedSectionToSectionRequests(
            List<CachedSection> cachedSections, float weight)
        {
            List<SectionRequest> converted = new List<SectionRequest>();
            foreach (var cachedSection in cachedSections)
            {
                if (cachedSection.Provider != "EastIndia")
                {
                    foreach (Provider provider in ExternalIntegration.Providers)
                    {
                        converted.Add(new SectionRequest(
                            cachedSection.From,
                            cachedSection.To,
                            weight,
                            cachedSection.Category,
                            ExternalIntegration.Providers.Find(y => y.Name == cachedSection.Provider))
                        );
                    }
                }

            }
            return converted;
        }

        private static List<CachedSection> removeCityRuleViolations(List<CachedSection> cachedSections, Category category)
        {
            var restrictedCities = new List<string>();
            restrictedCities.Add("Hvalbugten");
            restrictedCities.Add("Kapstaden");
            var restrictedCategories = new List<string>();
            restrictedCategories.Add("Weapons");
            restrictedCategories.Add("Live Animals");
            if (restrictedCategories.Contains(category.Name))
            {
                return cachedSections.Where(x =>
                    restrictedCities.Contains(x.From.Name)
                    || restrictedCities.Contains(x.To.Name)).ToList();
            }
            return cachedSections;
        }
    }
}