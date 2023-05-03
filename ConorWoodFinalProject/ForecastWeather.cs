using System;
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
    /// <summary>
    /// Inherits from Weather class. Contains additional information specific to forecast days, such as dates 
    /// </summary>
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

        
        /// <summary>
        /// Uses HTTP Client to make an API call to forecast endpoint. 
        /// Parses JSON object and populates class members
        /// </summary>
        /// <param name="location"> Location to retrieve weather data for </param>
        /// <returns> Task to await in calling function </returns>
        public override async Task getWeatherInfo(string location)
        {
            this.client = new();
            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/forecast.json?key={this.key}&q={location}&days=3");

            forecastRoot = JsonSerializer.Deserialize<ForeCastRoot?>(json);


            this.cityName = forecastRoot.location.name;
            this.days = forecastRoot.forecast.forecastday;

            this.bitmap = LoadBitmapFromURl(forecastRoot.forecast.forecastday[0].day.condition.icon);

            foreach (var v in forecastRoot.forecast.forecastday)
            {
                icons.Add(LoadBitmapFromURl(v.day.condition.icon));
            }



            
        }

        /// <summary>
        /// Format date returned by JSON object from yyyy-MM-dd into M/d format for display 
        /// </summary>
        /// <param name="forecastDay"> Day to format date </param>
        /// <returns> String with correctly formatted date </returns>
        /// 
        public string formattedDate(ForecastDay forecastDay)
        {
            string input = forecastDay.date;
            DateTime date = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return date.ToString("M/d");
        }
    }
}
