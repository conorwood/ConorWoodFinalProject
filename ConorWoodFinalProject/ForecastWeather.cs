﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ConorWoodFinalProject
{
    [Serializable]
    public class ForecastWeather : Weather 
    {
        private ForeCastRoot? forecastRoot;
        private string date;
        private List<ForecastDay> days;

        [NonSerialized]
        List<BitmapImage> icons;

        public ForecastWeather() : base()
        {
            forecastRoot = new ForeCastRoot();
            days = new List<ForecastDay>();
            icons = new List<BitmapImage>();
        }


        public List<ForecastDay> Days
        {
            get 
            {
                return this.days;
            }
        }

        public List<BitmapImage> Icons
        {
            get
            {
                return this.icons;
            }
        }

        

        public override async Task getWeatherInfo(string zipCode)
        {
            this.client = new();
            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/forecast.json?key={this.key}&q={zipCode}&days=3");

            forecastRoot = JsonSerializer.Deserialize<ForeCastRoot?>(json);


            this.cityName = forecastRoot.location.name;
            this.days = forecastRoot.forecast.forecastday;

            this.bitmap = LoadBitmapFromURl(forecastRoot.forecast.forecastday[0].day.condition.icon);

            foreach (var v in forecastRoot.forecast.forecastday)
            {
                icons.Add(LoadBitmapFromURl(v.day.condition.icon));
            }



            
        }


        public string formattedDate(ForecastDay forecastDay)
        {
            string input = forecastDay.date;
            DateTime date = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return date.ToString("M/d");
        }
    }
}
