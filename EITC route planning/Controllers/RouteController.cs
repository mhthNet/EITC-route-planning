using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using EITC_route_planning.BusinessLogic;
using EITC_route_planning.Models;
using EITC_route_planning.Services;

namespace EITC_route_planning.Controllers
{
    public class RouteController : ApiController
    {

        // GET api/<controller>/5 
        public IHttpActionResult Get(string fromName, string toName, string parcelType, int weight, int filter)
        {
            fromName = fromName.ToUpper();
            toName = toName.ToUpper();
            if (!DbHelper.GetAllCities().Select(x => x.Name).Contains(fromName) ||
                !DbHelper.GetAllCities().Select(x => x.Name).Contains(toName))
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
            var result = RouteCalculator.CalculateInternalRoute(weight, new Category(parcelType), filter == 1, fromName, toName);
            
            return Json(new { valid = "true", duration = result.Duration.ToString(), price = result.Price.ToString(), fromName = fromName, toName = toName });
        }

    }
}