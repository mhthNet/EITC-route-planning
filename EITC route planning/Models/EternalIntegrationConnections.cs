﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EITC_route_planning.Services;

namespace EITC_route_planning.Models
{
    public class EternalIntegrationConnections
    {
        public string Name { get; set; }

        public string Endpoint { get; set; }

        public EternalIntegrationConnections(string name, string endpoint)
        {
            this.Name = name;
            this.Endpoint = endpoint;
        }
    }
}