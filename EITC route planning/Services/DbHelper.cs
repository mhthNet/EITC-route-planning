﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbHelper

    {
        public static List<Section> GetAllSectionsFromDb()
        {
            throw new NotImplementedException();
        }

        public static List<CachedSection> GetAllCachedSectionsFromDb()
        {
            throw new NotImplementedException();
        }

        public static void SaveCachedSections(List<CachedSection>cachedSections)
        {
            throw new NotImplementedException();
        }

        public static List<Category> GetAllCategoriesFromDb()
        {
            //TODO: maybe use key value pairs to also include the category ID?
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Category", conn);

                List<Category> categories = new List<Category>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    Console.WriteLine("FirstColumn\tSecond Column\t\tThird Column\t\tForth Column\t");
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.Name = reader[1].ToString();
                        category.PriceFactor = 1.0f; //TODO: fix
                        categories.Add(category);
                    }
                }
                
                return categories;
            }
        }
    }
}