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
        private static string name;
        private static City cityTo;
        private static int length;
        private static TransportationType transportType;
        private static string nameType;
        private static float xLocation;
        private static float yLocation;
        private static string type;
        private static float speed;
        private static float weightLimit;

        public static List<Section> GetAllSectionsFromDb()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.from_Name LIKE Cities.name", conn);
                
                List<Section> sections = new List<Section>();
                Section section = new Section(cityFrom, cityTo, length, transportType);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        cityFrom = new City(name, xLocation, yLocation);
                        name = reader[1].ToString();
                        xLocation = float.Parse(reader[9].ToString());
                        yLocation = float.Parse(reader[10].ToString());
                        length = (int) reader[5];
                        
                    }
                    reader.Close();
                }

                SqlCommand command2 = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.to_name LIKE Cities.name", conn);
                using (SqlDataReader reader2 = command2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        cityTo = new City(name, xLocation, yLocation);
                        name = reader2[2].ToString();
                        xLocation = float.Parse(reader2[9].ToString());
                        yLocation = float.Parse(reader2[10].ToString());
                        
                    }
                    reader2.Close();
                }

                SqlCommand command3 = new SqlCommand("SELECT * FROM TransportationType", conn);
                using (SqlDataReader reader3 = command3.ExecuteReader())
                {
                    while (reader3.Read())
                    {
                        transportType = new TransportationType(type, speed, weightLimit);
                        type = reader3[1].ToString();
                        speed = float.Parse(reader3[2].ToString());
                        weightLimit = float.Parse(reader3[3].ToString());
                        
                    }
                    reader3.Close();
                }       
                        
                sections.Add(section);
                    
                
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
                        City city = new City(nameType, xLocation, yLocation);
                        nameType = reader[1].ToString();
                        xLocation = float.Parse(reader[2].ToString());
                        yLocation = float.Parse(reader[3].ToString());
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

        public static List<TransportationType> getAllTransportationTypes()
        {

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TransportationType", conn);

                List<TransportationType> transportationTypes = new List<TransportationType>();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TransportationType transportationType = new TransportationType(type, speed, weightLimit);
                        type = reader[1].ToString();
                        speed = float.Parse(reader[2].ToString());
                        weightLimit = float.Parse(reader[3].ToString());
                        transportationTypes.Add(transportationType);
                    }
                }

                return transportationTypes;
            }
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

        public static List<WeightGroup> getAllWeightGroups()
        {
            throw new NotImplementedException();
        }
    }
}