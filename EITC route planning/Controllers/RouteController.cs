using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;
using EITC_route_planning.Services;

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
        public IHttpActionResult Get(string originName, string destinationName, string parcelType, int weight, int filter)
        {
            if (!DbHelper.GetAllCities().Select(x => x.Name).Contains(originName) ||
                !DbHelper.GetAllCities().Select(x => x.Name).Contains(destinationName))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "One of the specified cities was not found"));
            }

            if (!DbHelper.GetAllCategoriesFromDb().Select(x => x.Name).Contains(parcelType))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The specified parcel type was not found"));
            }

            if (!(filter == 0 || filter == 1))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The specified filter was not found"));
            }

            if (weight > 100)
            {
                return Json(new {});
            }
            var result = RouteCalculator.CalculateInternalRoute(weight, new Category(parcelType), filter == 1, originName, destinationName);
            
            return Json(new { valid = "true", duration = result.Duration.ToString(), price = result.Price.ToString(), fromName = originName, toName = destinationName });
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