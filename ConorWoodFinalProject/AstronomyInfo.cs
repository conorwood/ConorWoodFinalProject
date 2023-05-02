using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConorWoodFinalProject
{
    [Serializable]
    public class AstronomyInfo
    {

        private string key;
        private AstronomyResponse astronomyResponse;
        [NonSerialized]
        HttpClient client;
        private string sunrise;
        private string sunset;
        private string moon_phase;

        public string Sunrise
        {
            get
            {
                return sunrise;
            }
        }

        public string Sunset
        {
            get
            {
                return sunset;
            }
        }

        public string MoonPhase
        {
            get
            {
                return moon_phase;
            }
        }

        public AstronomyInfo() 
        {
            astronomyResponse = new AstronomyResponse();
            client = new();
            key = "b06f9ec5e918401889a152444230604";
        }

        public async Task getAstroInfo(string location)
        {
            
            string? dt = getCurrentDate();

            HttpClient client = new();

            var json = await client.GetStringAsync($"http://api.weatherapi.com/v1/astronomy.json?key={this.key}&q={location}&dt={dt}");
            astronomyResponse = JsonSerializer.Deserialize<AstronomyResponse?>(json);

            Astro astro = astronomyResponse.astronomy.astro;

            this.sunrise = astro.sunrise;
            this.sunset = astro.sunset;
            this.moon_phase = astro.moon_phase;

        }

        private static string getCurrentDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.ToString("yyyy-MM-dd");
        }
    }

}
