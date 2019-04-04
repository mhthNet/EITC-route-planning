using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbHelper

    {
        private static City cityFrom;
        private static City cityTo;
        private static int length;
        private static TransportationType transportType;

        public static List<Section> GetAllSectionsFromDb()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.from_Name LIKE Cities.name", conn);
                SqlCommand command2 = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.to_name LIKE Cities.name", conn);

                SqlDataReader reader2 = command2.ExecuteReader();
                List<Section> sections = new List<Section>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Section section = new Section(cityFrom, cityTo, length, transportType);
                        cityFrom = (reader[1].ToString(), (float) reader[9], (float)reader[10]);
                        /*city.Location = new Point((int)reader[2], (int)reader[3]);
                        cities.Add(city);*/
                    }
                }

                return sections;
            }
        }

        public static List<City> GetAllCities()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Cities", conn);

                List<City> cities = new List<City>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        City city = new City();
                        city.Name = reader[1].ToString();
                        city.Location = new Point((int) reader[2], (int) reader[3]);
                        cities.Add(city);
                    }
                }

                return cities;
            }
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
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Category", conn);

                List<Category> categories = new List<Category>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.Name = reader[1].ToString();
                        category.PriceFactor = float.Parse(reader[2].ToString()); 
                        categories.Add(category);
                    }
                }
                
                return categories;
            }
        }
    }
}