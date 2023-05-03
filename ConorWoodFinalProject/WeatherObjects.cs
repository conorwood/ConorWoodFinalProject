using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConorWoodFinalProject
{
    /// <summary>
    /// All classes are Objects representing JSON objects returned by the Weather API that this project uses. 
    /// <summary/>
    /// 

    /// <summary>
    /// RootObject is the top-level of the JSON response 
    /// </summary>
    [Serializable]
    public class RootObject
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
    }
    /// <summary>
    /// Represents Location JSON object 
    /// </summary>
    /// <param name="name"> Name of the city </param>
    /// <param name="region"> Name of the region (state) </param>
    /// 
    [Serializable]
    public class Location
    {
        public Location() { }
        public string? name { get; set; }
        public string? region { get; set; }
        //public string? country { get; set; }

        //public double lat { get; set; }
        //public double lon { get; set; }

        //public string? tz_id { get; set; }
        //public long localtime_epoch { get; set; }
        //public string? localtime { get; set; }

    }

    /// <summary>
    /// Represents the Current JSON object 
    /// </summary>
    /// <param name="temp_f">The current temperature, in farenheit </param>
    /// <param name="is_day">integer, 1 means it is daytime and 0 means it is nighttime </param>
    /// <param name="uv"> The current UV index </param>
    /// <param name="wind_mph"> The current wind speed, in MPH </param>
    /// <param name="condition"> Condition object contained within Current object </param>
    /// 
    [Serializable]
    public class Current
    {
        public Current() { }
        public double temp_f { get; set; }
        public double feelslike_f { get; set; }

        public int is_day { get; set; }

        public double uv { get; set; }

        public double wind_mph { get; set; }

        public Condition? condition { get; set; }
    }

    /// <summary>
    /// Represents Condition JSON Object
    /// </summary>
    /// <param name="text"> Text of what the condition is (sunny, rainy, cloudy, etc.) </param>
    /// <param name="icon"> URL of image representing the condition </param>
    [Serializable]
    public class Condition
    {
        public Condition() { }

        public string text { get; set; }
        public string icon { get; set; }

    }

    /// <summary>
    /// Top-Level response of Forecast endpoint
    /// </summary>
    /// <param name="location"> Response Location object </param>
    /// <param name="current"> Reponse Current object </param>
    /// <param name="forecast"> Response forecast object </param>
    [Serializable]
    public class ForeCastRoot
    {
        public Location? location { get; set; }
        public Current? current { get; set; }

        public Forecast? forecast { get; set; }
    }

    /// <summary>
    /// Forecast Object from JSON response 
    /// </summary>
    /// <param name="forecastday"> List of days containing forecast information </param>
    [Serializable]
    public class Forecast
    {
        public Forecast() { }

        public List<ForecastDay> forecastday { get; set; }
    }
    /// <summary>
    /// ForecastDay JSON Object
    /// </summary>
    /// <param name="date"> Date of forecast day </param>
    /// <param name="day"> Day object </param>
    [Serializable]
    public class ForecastDay
    {
        public ForecastDay() { }

        public string date { get; set; }

        public Day day { get; set; }

    }

    /// <summary>
    /// Day JSON object
    /// </summary>
    /// <param name="maxtemp_f"> High temperature in farenheit </param>
    /// <param name="mintemp_f"> Low temperature in farenheit </param>
    /// <param name="condition"> Condition object </param>
    [Serializable]
    public class Day
    {
        public Day() { }

        public double maxtemp_f { get; set; }

        public double mintemp_f { get; set; }

        public Condition condition { get; set; }
    }

    [Serializable]
    class AstronomyResponse
    {
        public AstronomyResponse() { }

        public Location location { get; set; }

        public Astronomy astronomy { get; set; }
    }

    [Serializable]
    public class Astronomy
    {
        public Astronomy() { }

        public Astro astro { get; set; }

    }

    /// <summary>
    /// Astronomy JSON object containing astronomy information 
    /// </summary>
    /// <param name="sunrise"> Time that the sun rises </param>
    /// <param name="sunset"> Time that the sun sets </param>
    /// <param name="moonrise"> Time that the moon rises </param>
    /// <param name="moonset"> Time that the moon sets </param>
    /// <param name="moon_phase"> Current phase that the moon is in </param>
    [Serializable]
    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        //public string moon_illumination { get; set; }
        //public int is_moon_up { get; set; }
        //public int is_sun_up { get; set; }

    }
}
