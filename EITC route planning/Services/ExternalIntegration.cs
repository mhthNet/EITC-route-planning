﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using EITC_route_planning.Models;
using RestSharp;

namespace EITC_route_planning.Services
{
    public class ExternalIntegration
    {
        static List<EternalIntegrationConnections> connections = new List<EternalIntegrationConnections>()
            {
                new EternalIntegrationConnections(
                    "Telstar Logistics",
                    "Telstar.com/api/"
                    ),
                new EternalIntegrationConnections(
                "Oceanic",
                "Oceanic.com/api/"
                )
            };

        public List<CachedSection> LoadAllSectionsFromAllExternals(List<SectionRequest> sectionRequests, EternalIntegrationConnections conn)
        {
            return sectionRequests.Select(s => LoadSectionFromExternal(conn, s)).ToList();
        } 
            
        private static CachedSection LoadSectionFromExternal(EternalIntegrationConnections externalConnection, SectionRequest sectionRequest)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>()
            {
                {"fromName",sectionRequest.From.Name},
                {"toName",sectionRequest.To.Name},
                {"category", sectionRequest.Category.Name },
                {"weight", sectionRequest.Weight.ToString() }
            };

            return ApiGetSectionRequest(externalConnection.Endpoint, parameters);
        }

        private static CachedSection ApiGetSectionRequest(string endpoint, Dictionary<String, String> parameters)
        {
            var client = new RestClient(endpoint);

            var request = new RestRequest("routes/get", Method.GET);
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                request.AddParameter(entry.Key, entry.Value);
            }

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
     
            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<CachedSection> cachedSection = client.Execute<CachedSection>(request);

            return new CachedSection();
        }
    }
}