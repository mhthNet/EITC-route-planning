using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EITC_route_planning.Models
{
    public class Category
    {
        public Category(){ }

        public Category(string name)
        {
            Name = name;
        }


        public string Name { get; set; }
        public float PriceFactor { get; set; }

        public Category(string name, float priceFactor)
        {
            Name = name;
            PriceFactor = priceFactor;
        }
    }


}