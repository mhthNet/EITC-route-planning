using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class FetchSections
    {
        public static List<CachedSection> FetchExternCachedSections(List<SectionRequest> sectionRequests)
        {
            return ExternalIntegration.LoadAllSectionsFromAllProviders(sectionRequests);
        }
        public static List<CachedSection> FetchInternCachedSections(float weight, Category category)
        {
            List<Section> ownSections = DbHelper.GetAllSectionsFromDb();

            return ownSections.Select(
                x => CalculateInternalCachedSegment(x, weight, category)
            ).ToList();
        }

        private static CachedSection CalculateInternalCachedSegment(Section section, float weight, Category category)
        { 
            return new CachedSection(
                price: CalculatePrice(section, weight, category),
                duration:CalculateDuration(section),
                weight: weight,
                category: category,
                from: section.From,
                to: section.To,
                provider: ExternalIntegration.EastIndia.Name
                );
        }

        private static float CalculateDuration(Section section)
        {
            float speed = section.TransportationType.Speed;
            return speed * section.Length;
        }

        private static decimal CalculatePrice(Section section, float weight, Category category)
        {
            List<WeightGroup> weightGroups = DbHelper.getAllWeightGroups();

            WeightGroup weightGroup = weightGroups
                .OrderByDescending(it => it.MaxWeight)
                .First(it => it.MaxWeight > weight);
            return weightGroup.Price * (decimal)category.PriceFactor;
        }
    }
}