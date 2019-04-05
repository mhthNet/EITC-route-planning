using EITC_route_planning.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace EITC_route_planning.Services
{
    public class DbRouteUpdater
    {
        public static void UpdateExternal()
        {
            float weight = 1;
            foreach (Category category in DbHelper.GetAllCategoriesFromDb())
            {
                var sectionsRequests = BuildSectionRequests(weight, category);

                List<CachedSection> newCachedSections = FetchSections.FetchExternCachedSections(sectionsRequests);

                DbHelper.SaveCachedSections(newCachedSections);
            }
    
        }

        public static void UpdateInternal()
        {
            float weight = 1;
            foreach (Category category in DbHelper.GetAllCategoriesFromDb())
            {

                List<CachedSection> newCachedSections = FetchSections.FetchInternCachedSections(weight, category);

                DbHelper.SaveCachedSections(newCachedSections);
            }

        }

        private static List<SectionRequest> BuildSectionRequests(float weight, Category category)
        {
            List<SectionRequest> sectionsRequests = new List<SectionRequest>();
            weight = 1;
            category = new Category("Default", (float) 1.0);

            foreach (Provider provider in ExternalIntegration.Providers)
            {
                List<SectionRequest> req = CityCombinations(weight, category, provider);
                sectionsRequests.AddRange(req);
            }
            return sectionsRequests;
        }

        private static List<SectionRequest> CityCombinations(float weight, Category category, Provider provider)
        {

            List<City> cities = DbHelper.GetAllCities();
            List<SectionRequest> allCityCombo = new List<SectionRequest>();
            foreach (var city in cities)
            {
                foreach (var city2 in cities)
                {
                    if (city != city2 && CombinationExistsIn(city,city2, allCityCombo))
                    {
                        allCityCombo.Add(
                            new SectionRequest(
                                city,
                                city2,
                                weight,
                                category,
                                provider
                            )
                        );
                    }
                }
            }
            return allCityCombo;
        }

        private static bool CombinationExistsIn(City city, City city2, List<SectionRequest> allCityCombo)
        {
            return allCityCombo.Any(x => (x.From == city && x.To == city2) || (x.From == city2 && x.To == city));
        }
    }
}