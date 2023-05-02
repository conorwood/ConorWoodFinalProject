using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ConorWoodFinalProject
{

    [Serializable]
    public class RootObject
    {
        public Location? location { get; set; }
        public Current? current { get; set; }
    }
    [Serializable]
    public class Location
    {
        public Location() { }
        public string? name { get; set; }
        public string? region { get; set; }
        public string? country { get; set; }

        public double lat { get; set; }
        public double lon { get; set; }

        public string? tz_id { get; set; }
        public long localtime_epoch { get; set; }
        public string? localtime { get; set; }

    }
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
    [Serializable]
    public class Condition
    {
        public Condition() { }

        public string text { get; set; }
        public string icon { get; set; }

        public int code { get; set; }
    }
    [Serializable]
    public class ForeCastRoot
    {
        public Location? location { get; set; }
        public Current? current { get; set; }

        public Forecast? forecast { get; set; }
    }
    [Serializable]
    public class Forecast
    {
        public Forecast() { }

        public List<ForecastDay> forecastday { get; set; }
    }
    [Serializable]
    public class ForecastDay
    {
        public ForecastDay() { }

        public string date { get; set; }

        public Day day { get; set; }

    }
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
    [Serializable]
    public class Astro
    {
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string moonrise { get; set; }
        public string moonset { get; set; }
        public string moon_phase { get; set; }
        public string moon_illumination { get; set; }
        public int is_moon_up { get; set; }
        public int is_sun_up { get; set; }

    }
}
