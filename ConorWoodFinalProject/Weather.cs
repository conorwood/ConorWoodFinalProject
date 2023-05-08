using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Collections.ObjectModel;

namespace ConorWoodFinalProject
{
    /// <summary>
    /// Represents a current weather object. Contains method to retrieve appropriate data 
    /// </summary>
    [Serializable]
    public class Weather
    {
        private RootObject? rootObject;
        protected string? cityName;
        protected string? weatherCondition;
        protected string? tempF;

        [NonSerialized]
        protected HttpClient client;
        protected string key;

        [NonSerialized]
        protected BitmapImage bitmap;
        private ForecastWeather forecast;

        protected string zipCode;
        protected string region;
        protected string feelslike_temp;
        protected string uv;
        protected string wind;
        protected int is_day;

        private AstronomyInfo astronomy;

        public Weather() 
        {
            rootObject = new RootObject();
            key = "b06f9ec5e918401889a152444230604"; // API Key
            astronomy= new AstronomyInfo();
        }

        public int IsDay
        {
            get
            {
                return this.is_day;
            }
        }

        public override string ToString()
        {
            return CityName;
        }

        public string UV
        {
            get 
            {
                return this.uv;
            }
        }

        public string Wind
        {
            get
            {
                return this.wind;
            }
        }

        public string Region
        {
            get
            {
                return this.region;
            }

        }

        public string Feelslike_temp
        {
            get
            {
                return this.feelslike_temp;
            }
        }

        public AstronomyInfo Astronomy
        {
            get
            {
                return this.astronomy;
            }
        }

        public ForecastWeather Forecast
        {
            get
            {
                return this.forecast;
            }

            set
            {
                this.forecast = value;
            }
        }

        public string ZipCode
        {
            get 
            {
                return this.zipCode;
            }

            set
            {
                this.zipCode = value;
            }
        }
        public string CityName
        {
            get
            {
                return this.cityName;
            }
        }

        public string WeatherCondition
        {
            get
            {
                return this.weatherCondition;
            }
        }

        public string TempF
        {
            get 
            {
                return this.tempF;
            }
        }

        public BitmapImage Bitmap
        {
            get 
            {
                return this.bitmap;
            }
        }


        /// <summary>
        /// Loads a bitmap image from a given URL
        /// </summary>
        /// <param name="url"> URL to load image from </param>
        /// <returns> Loaded BitmapImage </returns>
        protected BitmapImage LoadBitmapFromURl(String url) 
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("https:" + url, UriKind.Absolute);
            bitmapImage.EndInit();

            return bitmapImage;
        }


        /// <summary>
        /// Uses HTTP Client to make an API call to current endpoint. 
        /// Parses JSON object and populates class members
        /// </summary>
        /// <param name="location"> Location to retrieve weather data for </param>
        /// <returns> Task to await in calling function </returns>
        public virtual async Task getWeatherInfo(string location)
        {
            client = new();
            // call endpoint and parse
            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/current.json?key={this.key}&q={location}");
            rootObject = JsonSerializer.Deserialize<RootObject?>(json);

            // set the object properties
            this.cityName = rootObject?.location?.name;
            this.weatherCondition = rootObject?.current?.condition?.text;
            this.tempF = rootObject?.current?.temp_f.ToString();
            this.region = rootObject?.location?.region;
            this.feelslike_temp = rootObject?.current?.feelslike_f.ToString();
            this.uv = rootObject?.current?.uv.ToString();
            this.wind = rootObject?.current.wind_mph.ToString();
            this.is_day = rootObject.current.is_day;

            this.bitmap = LoadBitmapFromURl(rootObject.current.condition.icon);

        }



    }
}
