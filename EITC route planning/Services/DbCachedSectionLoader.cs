using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbCachedSectionLoader
    {
        public List<CachedSection> Load(Category category)
        {
            return new List<CachedSection>();
        }
    }
}