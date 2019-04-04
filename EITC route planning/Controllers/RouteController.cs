﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;

namespace EITC_route_planning.Controllers
{
    public class RouteController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5 
        public IHttpActionResult Get(string fromName, string toName, string parcelType, int weight, int filter)
        {

            //var result = RouteCalculator.Calcuate(new Category(), 1, new City(), new City());
            //"valid": "true",    "duration": 7,    "price": 25,    "fromName": "Slavekysten",    "toName": "Saharah
            return Json(new { valid = "true", duration = "7", price = "25", fromName = "Slavekysten", toName = "Saharah" }); 
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}