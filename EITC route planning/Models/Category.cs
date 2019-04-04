using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using EITC_route_planning.Services;

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

        private void validate(string name)
        {
            var Category = DbHelper.GetCategoryByName(name);
            if (Category == null)
            {
                throw new InvalidDataException("invalid Category name");
            }
        }
    }


}