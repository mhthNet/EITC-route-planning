using EITC_route_planning.Models;
using EITC_route_planning.Services;
using System;
using System.Collections.Generic;
using System.Linq;


public class DbRouteUpdater
{
    public void Update()
    {
        // fetch

        //save to db

    }

    private List<Section> CityCombinations()
    {

        List<City> cities = DbHelper.GetAllCities();
        List<Section> allCityCombo = new List<Section>();
        foreach (var city in cities)
        {
            foreach (var city2 in cities)
            {
                if (city != city2)
                {
                    allCityCombo.Add(new Section(city, city2, 0, null));
                }


            }

        }

        return allCityCombo;
    }
}