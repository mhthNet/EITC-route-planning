using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;



namespace EITC_route_planning.Services
{
    public class MockExternalCachedSections
    {
        private static int percentage = 3;
        public static List<CachedSection> LoadAllSectionsFromAllProviders(List<SectionRequest> sectionRequests)
        {

            Random random = new Random();
            return sectionRequests.Select(x => new CachedSection(
                    x.From,
                    x.To,
                    (decimal)random.Next(5, 30),
                    (float)random.Next(3, 15),
                    x.Weight,
                    x.Category,
                    x.Provider?.Name
                )
            ).Where(it => random.Next(1, 100)<= 3).ToList();
        }
    }
}