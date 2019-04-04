﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using EITC_route_planning.Models;

namespace EITC_route_planning.Services
{
    public class DbHelper

    {
        private static String type;
        private static float speed;
        private static float weightLimit;
        private static TransportationType transportType;
        private static City cityFrom;
        private static City cityTo;
        private static int length;
        public static List<Section> GetAllSectionsFromDb()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.from_Name LIKE Cities.name", conn);
                
                List<Section> sections = new List<Section>();
                List<City> citiesFrom = new List<City>();
                List<City> citiesTo = new List<City>();
                List<Int32> lengths = new List<Int32>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        
                        String name = reader[1].ToString();

                        float xLocation = float.Parse(reader[9].ToString());
                        float yLocation = float.Parse(reader[10].ToString());
                        length = (int) reader[5];
                        System.Diagnostics.Debug.WriteLine("QQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQQ");
                        System.Diagnostics.Debug.WriteLine(name);
                        System.Diagnostics.Debug.WriteLine(xLocation);
                        System.Diagnostics.Debug.WriteLine(yLocation);
                        System.Diagnostics.Debug.WriteLine(length);
                        cityFrom = new City(name, xLocation, yLocation);
                        citiesFrom.Add(cityFrom);
                        lengths.Add(length);
                    }
                    reader.Close();
                }

                SqlCommand command2 = new SqlCommand("SELECT *, Cities.X, Cities.Y FROM Section INNER JOIN Cities ON Section.to_name LIKE Cities.name", conn);
                using (SqlDataReader reader2 = command2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        
                        String name = reader2[2].ToString();
                        float xLocation = float.Parse(reader2[9].ToString());
                        float yLocation = float.Parse(reader2[10].ToString());
                        cityTo = new City(name, xLocation, yLocation);
                        citiesTo.Add(cityTo);
                    }
                    reader2.Close();
                }

                SqlCommand command3 = new SqlCommand("SELECT * FROM TransportationType", conn);
                using (SqlDataReader reader3 = command3.ExecuteReader())
                {
                    while (reader3.Read())
                    {
                        
                        String type = reader3[1].ToString();
                        float speed = float.Parse(reader3[2].ToString());
                        float weightLimit = float.Parse(reader3[3].ToString());
                        transportType = new TransportationType(type, speed, weightLimit);
                    }
                    reader3.Close();
                }
                System.Diagnostics.Debug.WriteLine(citiesFrom.Count);
                System.Diagnostics.Debug.WriteLine(citiesTo.Count);
                System.Diagnostics.Debug.WriteLine(lengths.Count);

                for (int i = 0; i < citiesFrom.Count; i++)
                {
                    Section section = new Section(citiesFrom[i], citiesTo[i], lengths[i], transportType);
                    sections.Add(section);
                }
                

                System.Diagnostics.Debug.WriteLine("TESTING!!!!!!!!!!!!!!!!!!");
                System.Diagnostics.Debug.WriteLine(sections[0].From.Name);
                

                return sections;
            }
        }

        public static City GetCityByName(string name)
        {
            List<City> cities = GetAllCities();
            City cityWithId = null;
            foreach (var city in cities)
            {
                if (city.Name == name)
                {
                    cityWithId = city;
                    break;
                }
            }
            return cityWithId;
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
                        String nameType = reader[1].ToString();
                        float xLocation = float.Parse(reader[2].ToString());
                        float yLocation = float.Parse(reader[3].ToString());
                        City city = new City(nameType, xLocation, yLocation);
                        cities.Add(city);
                    }
                }

                return cities;
            }
        }

        public static List<CachedSection> GetAllCachedSectionsFromDb()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();

                SqlCommand commandSections = new SqlCommand("SELECT * FROM CachedSection");
                List<CachedSection> cachedSections = new List<CachedSection>();

                using (SqlDataReader reader = commandSections.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int cachedSectionId = (int)reader[0];
                        decimal price = decimal.Parse(reader[1].ToString());
                        float duration = float.Parse(reader[2].ToString());

                        string cityFromName = reader[3].ToString();
                        string cityToName = reader[4].ToString();

                        City from = GetCityByName(cityFromName);
                        City to = GetCityByName(cityToName);

                        string provider = reader[5].ToString();
                        float weight = float.Parse(reader[6].ToString());

                        string categoryName = reader[7].ToString();
                        Category category = GetCategoryByName(categoryName);

                        CachedSection cachedSection = new CachedSection(from, to, price, duration, weight, category, length);
                        cachedSection.Provider = provider;
                        cachedSection.Id = cachedSectionId;

                        cachedSections.Add(cachedSection);
                    }
                }
                return cachedSections;
            }
        }

        public static void SaveCachedSections(List<CachedSection> cachedSections)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=dbs-eitdk.database.windows.net;Database=db-eitdk;User Id=admin-eitdk;Password=Eastindia4thewin";
                conn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO dbo.CachedSection(id, price, duration, fromCity, toCity, provider, weight, category_name) VALUES(@ID, @Price, @Duration, @FromCity, @ToCity, @Provider, @Weight, @Category)", conn);

                List<City> cities = new List<City>();
                
                command.Parameters.Add("@ID", SqlDbType.Int);
                command.Parameters.Add("@Price", SqlDbType.Text);
                command.Parameters.Add("@Duration", SqlDbType.Float);
                command.Parameters.Add("@FromCity", SqlDbType.Text);
                command.Parameters.Add("@ToCity", SqlDbType.Text);
                command.Parameters.Add("@Provider", SqlDbType.Text);
                command.Parameters.Add("@Weight", SqlDbType.Float);
                command.Parameters.Add("@Cateogry", SqlDbType.Text);

                for (int i = 0; i < cachedSections.Count; i++)
                {
                    command.Parameters["@ID"].Value = i + 1;
                    command.Parameters["@Price"].Value = cachedSections[i].Price;
                    command.Parameters["@Duration"].Value = cachedSections[i].Duration;
                    command.Parameters["@FromCity"].Value = cachedSections[i].From.Name;
                    command.Parameters["@ToCity"].Value = cachedSections[i].To.Name;
                    command.Parameters["@Provider"].Value = cachedSections[i].Provider;
                    command.Parameters["@Weight"].Value = cachedSections[i].Weight;
                    command.Parameters["@Category"].Value = cachedSections[i].Category.Name;
                    
                    command.ExecuteNonQuery();
                }
                
            }
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
                        String type = reader[1].ToString();
                        float speed = float.Parse(reader[2].ToString());
                        float weightLimit = float.Parse(reader[3].ToString());
                        TransportationType transportationType = new TransportationType(type, speed, weightLimit);
                        transportationTypes.Add(transportationType);
                    }
                }

                return transportationTypes;
            }
        }

        public static Category GetCategoryByName(string name)
        {
            List<Category> categories = GetAllCategoriesFromDb();
            Category categoryWithId = null;
            foreach (var category in categories)
            {
                if (category.Name == name)
                {
                    categoryWithId = category;
                    break;
                }
            }
            return categoryWithId;
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
            return new List<WeightGroup>()
            {
                new WeightGroup(1, 40),
                new WeightGroup(10, 50),
                new WeightGroup(50, 60),
                new WeightGroup(100, 70)
            };
        }
    }
}