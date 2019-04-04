using System;
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
        public static Provider Telstar = new Provider(
            "Telstar Logistics",
            "Telstar.com/api/"
        );

        public static Provider Oceanic = new Provider(
            "Oceanic",
            "Oceanic.com/api/"
        );


        public static List<Provider> Providers = new List<Provider>()
            {
                Oceanic,
                Telstar
            };

        public static List<CachedSection> LoadAllSectionsFromAllProviders(List<SectionRequest> sectionRequests)
        {
            List<CachedSection> result = new List<CachedSection>();
            foreach (Provider provider in Providers)
            {
                var requestsForProvider = sectionRequests.FindAll(x => x.Provider == provider);
                result.AddRange(LoadAllSectionsFromProvider(requestsForProvider, provider));
            }
            return result;
        }

        public static List<CachedSection> LoadAllSectionsFromProvider(List<SectionRequest> sectionRequests, Provider provider)
        {
            return sectionRequests.Select(s => LoadSectionFromExternal(provider, s)).ToList();
        }
        private static CachedSection LoadSectionFromExternal(Provider externalConnection, SectionRequest sectionRequest)
        {
            Dictionary<String, String> parameters = new Dictionary<string, string>()
            {
                {"fromName",sectionRequest.From.Name},
                {"toName",sectionRequest.To.Name},
                {"category", sectionRequest.Category.Name },
                {"weight", sectionRequest.Weight.ToString() }
            };

            var client = new RestClient(externalConnection.Endpoint);

            var request = new RestRequest("routes", Method.GET);
            foreach (KeyValuePair<string, string> entry in parameters)
            {
                request.AddParameter(entry.Key, entry.Value);
            }

            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
            IRestResponse<CachedSection> cachedSection = client.Execute<CachedSection>(request);

            var data = cachedSection.Data;
            return new CachedSection(
                sectionRequest.From, 
                sectionRequest.To,
                data.Price,
                data.Duration,
                sectionRequest.Weight,
                sectionRequest.Category,
                data.Length
            );
        }
    }
}