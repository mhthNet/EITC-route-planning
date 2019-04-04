using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbRouteUpdater
    {
        public void Update()
        {
            // fetch
            List<CachedSection> cachedSections = FetchSections.FetchCachedSections(new List<Section>(), 1, new Category());
            //save to db
            DbHelper.SaveCachedSections(cachedSections);
        }
    }
}