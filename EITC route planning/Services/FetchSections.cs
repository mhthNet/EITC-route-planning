using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using EITC_route_planning.Models;
using Microsoft.Ajax.Utilities;

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
            ).Where(x => x != null).ToList();
        }

        private static CachedSection CalculateInternalCachedSegment(Section section, float weight, Category category)
        {
            try
            {
                return new CachedSection(
                    price: CalculatePrice(section, weight, category),
                    duration: CalculateDuration(section),
                    weight: weight,
                    category: category,
                    from: section.From,
                    to: section.To,
                    provider: ExternalIntegration.EastIndia.Name
                );
            }
            catch (InvalidDataException e)
            {
                Console.Write(e.Message);
                return null;
            }
            
        }

        private static float CalculateDuration(Section section)
        {
            float speed = section.TransportationType.Speed;
            return speed * section.Length;
        }

        public static decimal CalculatePrice(Section section, float weight, Category category)
        {
            List<WeightGroup> weightGroups = DbHelper.GetAllWeightGroupsFromDb();
            try
            {
                List<WeightGroup> weightGroups2 = weightGroups
                    .OrderBy(i => i.MaxWeight).ToList();

                WeightGroup weightGroup = weightGroups2.First(it => it.MaxWeight >= weight);
                return weightGroup.Price * (decimal) category.PriceFactor * section.Length;
            }
            catch
            {
                throw new InvalidDataException("The weight does not fit into any weight group");
            }
            
        }
    }
}