using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbCachedSectionLoader
    {
        public static List<CachedSection> Load(Category category)
        {
            List<CachedSection> cachedSection = DbHelper.GetAllCachedSectionsFromDb();
            return cachedSection.Where(x=> x.Category?.Name == category.Name).ToList();
        }
    }
}