using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using EITC_route_planning.Models;
using EITC_route_planning.Services;

namespace EITC_route_planning.BusinessLogic
{
    public class RouteCalculator
    {
        //public static CalculatedRoute Calcuate(Category category, float weight, City from, City to)
        //{
        //    List<CachedSection> cachedSections = DbCachedSectionLoader.Load(category);

        //    List<CalculatedRoute> approximatedcalculatedRoutes = ShortestPath.Calculate(cachedSections);

        //    List<Section> sections = mapToSections(approximatedcalculatedRoutes);

        //    List<CachedSection> upToDateSections = FetchSections.FetchCachedSections(sections, weight, category);

        //    List<CalculatedRoute> exactCalculatedRoutes = ShortestPath.Calculate(upToDateSections);

        //    return exactCalculatedRoutes[0];
        //}

        static void CalculateInternalRoute(float weight, Category category, bool fastest, string origin, string destination)
        {
            var cachedSections = FetchSections.FetchInternCachedSections(weight, category);
            var cities = DbHelper.GetAllCities().Select(x => x.Name).ToList();
            var route = ShortestPath.calculateKRoutes(origin, destination, cities, cachedSections, fastest, 1);
            // Calculate total price and just do start/finish in response. Convert it to Json
            throw new NotImplementedException();
        }

        private static List<SectionRequest> mapToSectionRequests(List<CalculatedRoute> caldulatedRoutes)
        {
            throw new NotImplementedException();
        }
    }
}