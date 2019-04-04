using EITC_route_planning.Models;
using System;
using System.Collections.Generic;


namespace EITC_route_planning.Services
{
    public class DbRouteUpdater
    {
        public static void Update()
        {
            float weight = 1;
            foreach (Category category in DbHelper.GetAllCategoriesFromDb())
            {
                var sectionsRequests = BuildSectionRequests(weight, category);

                List<CachedSection> newCachedSections = FetchSections.FetchExternCachedSections(sectionsRequests);

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
                    if (city != city2)
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
    }
}