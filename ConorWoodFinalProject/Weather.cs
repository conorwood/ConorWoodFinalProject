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

namespace ConorWoodFinalProject
{
    internal class Weather
    {
        private RootObject? rootObject;
        protected string? cityName;
        protected string? weatherCondition;
        protected string? tempF;
        protected HttpClient client;
        protected string key;
        protected BitmapImage bitmap;
        private ForecastWeather forecast;

        protected string zipCode;
        protected string region;
        protected string feelslike_temp;
        protected string uv;
        protected string wind;
        protected int is_day;

        public Weather() 
        {
            rootObject = new RootObject();
            client = new();
            key = "b06f9ec5e918401889a152444230604";
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
        

        protected virtual BitmapImage LoadBitmap(String assetsRelativePath, double decodeWidth)
        {
            BitmapImage theBitmap = new BitmapImage();
            theBitmap.BeginInit();
            String basePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"assets\");
            String path = System.IO.Path.Combine(basePath, assetsRelativePath);
            theBitmap.UriSource = new Uri(path, UriKind.Absolute);
            theBitmap.DecodePixelWidth = (int)decodeWidth;
            theBitmap.EndInit();

            return theBitmap;
        }

        protected BitmapImage LoadBitmapFromURl(String url) 
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("https:" + url, UriKind.Absolute);
            bitmapImage.EndInit();

            return bitmapImage;
        }

        public virtual async Task getWeatherInfo(string zipCode)
        {
            
            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/current.json?key={this.key}&q={zipCode}");

            rootObject = JsonSerializer.Deserialize<RootObject?>(json);

            this.cityName = rootObject?.location?.name;
            this.weatherCondition = rootObject?.current?.condition?.text; // for now
            this.tempF = rootObject?.current?.temp_f.ToString();
            this.region = rootObject?.location?.region;
            this.feelslike_temp = rootObject?.current?.feelslike_f.ToString();
            this.uv = rootObject?.current?.uv.ToString();
            this.wind = rootObject?.current.wind_mph.ToString();
            this.is_day = rootObject.current.is_day;

            //this.bitmap = LoadBitmap("sun.png", 50.0);
            this.bitmap = LoadBitmapFromURl(rootObject.current.condition.icon);

        }



    }
}
