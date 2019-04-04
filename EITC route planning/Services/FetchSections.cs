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
        public static List<CachedSection> FetchCachedSections(
            List<Section> sections, float weight, Category category)
        {

            // get own 
            List<Section> ownSections = DbHelper.GetAllSectionsFromDb();

            List<CachedSection> ownAsCachedSections = ownSections.Select(CalculateCachedSegments).ToList();


            // get others from integration

            return new List<CachedSection>();
        }

        private static CachedSection CalculateCachedSegments(Section section)
        {
            return new CachedSection();
        }
    }
}