using EITC_route_planning.Models;
using System;
using System.Collections.Generic;


namespace EITC_route_planning.Services
{
    public class DbRouteUpdater
    {
        public void Update()
        {
            float weight = 1;
            Category category = new Category("Default", 1);
            var sectionsRequests = BuildSectionRequests(out weight, out category);

            List<CachedSection> newCachedSections = FetchSections.FetchExternCachedSections(sectionsRequests);

            //save to db
            DbHelper.SaveCachedSections(newCachedSections);
        }

        private List<SectionRequest> BuildSectionRequests(out float weight, out Category category)
        {
            List<SectionRequest> sectionsRequests = new List<SectionRequest>();
            weight = 1;
            category = new Category("Default", (float) 1.0);

            foreach (Provider provider in ExternalIntegration.Providers)
            {
                sectionsRequests.AddRange(CityCombinations(weight, category, provider));
            }
            return sectionsRequests;
        }

        private List<SectionRequest> CityCombinations(float weight, Category category, Provider provider)
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